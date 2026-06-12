using BepInEx;
using Photon.Voice.Unity;
using Experimental.Stuff;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Experimental.Core.MainManagers;

public static class SoundManager
{
    public enum PlaybackMode
    {
        Local,
        Microphone,
        Both
    }

    public static float MasterVolume = 1f;
    public static float LocalVolume = 1f;
    public static float MicrophoneVolume = 1f;

    private static AudioSource? LocalScr;
    private static readonly Dictionary<string, AudioClip> Cache = new();

    public static AudioClip? CurrentClip { get; private set; }

    public static bool IsPlaying => LocalScr != null && LocalScr.isPlaying;
    public static bool IsPaused { get; private set; }

    public static bool IsStopped => LocalScr == null || (!LocalScr.isPlaying && !IsPaused);
    public static float CurrentTime => LocalScr?.time ?? 0f;

    public static float CurrentLength => CurrentClip?.length ?? 0f;

    public static void Pause()
    {
        if (LocalScr == null)
            return;

        LocalScr.Pause();
        IsPaused = true;
    }

    public static void Resume()
    {
        if (LocalScr == null)
            return;

        LocalScr.UnPause();
        IsPaused = false;
    }


    #region GUIExtras
    public static float CurrentProgress
    {
        get
        {
            if (CurrentLength <= 0)
                return 0f;

            return CurrentTime / CurrentLength;
        }
    }
    public static void TogglePause()
    {
        if (IsPaused)
            Resume();
        else
            Pause();
    }
    #endregion

    public static void Init()
    {
        if (LocalScr != null)
            return;

        GameObject Sound = new(Constantss.ExperimentalSoundManager + ".SoundOBJ");
        GameObject.DontDestroyOnLoad(Sound);
        LocalScr = Sound.AddComponent<AudioSource>();
        LocalScr.playOnAwake = false;
        LocalScr.loop = false;
        LocalScr.spatialBlend = 0f;
    }

    public static async Task<AudioClip?> LoadFile(string Path)
    {
        if (Cache.TryGetValue(Path, out var result)) return result;

        AudioType type = GetAudioType(Path);

        using UnityWebRequest rcs = UnityWebRequestMultimedia.GetAudioClip("file://" + Path, type);
        var Oper = rcs.SendWebRequest();
        while (!Oper.isDone)
            await Task.Yield();

        if (rcs.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[{Constantss.ExperimentalSoundManager}]: Failed To Load Audio: {rcs.error}");
            return null;
        }
        AudioClip clip = DownloadHandlerAudioClip.GetContent(rcs);
        Cache[Path] = clip;
        return clip;

    }

    public static async Task<AudioClip?> LoadURL(string url)
    {
        if (Cache.TryGetValue(url, out var result)) return result;

        AudioType type = GetAudioType(url);
        using UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(url, type);
        var Oper = req.SendWebRequest();
        while (!Oper.isDone) await Task.Yield();
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[{Constantss.ExperimentalSoundManager}]: Failed To Load URL Audio: {req.error}");
            return null;
        }
        AudioClip clip = DownloadHandlerAudioClip.GetContent(req);
        Cache[url] = clip;
        return clip;
    }

    public static void PlayClip(AudioClip Clip, PlaybackMode mode = PlaybackMode.Local, float Vol = 1f, bool loop = false)
    {
        if (Clip == null) return;
        switch (mode)
        {
            case PlaybackMode.Local:
                PlayLocal(Clip, Vol, loop);
                break;

            case PlaybackMode.Microphone:
                PlayMic(Clip, Vol, loop);
                break;

            case PlaybackMode.Both:
                PlayLocal(Clip, Vol, loop);
                PlayMic(Clip, Vol, loop);
                break;
        }
        CurrentClip = Clip;
        IsPaused = false;
    }

    public static void PlayLocal(AudioClip clip, float vol = 1f, bool loop = false)
    {
        LocalScr!.volume = vol * LocalVolume * MasterVolume;
        LocalScr.clip = clip;
        LocalScr.loop = loop;
        LocalScr.Play();
    }

    public static void PlayMic(AudioClip clip, float volume = 1f, bool loop = false)
    {
        Recorder rec = GorillaTagger.Instance.myRecorder;
        if(rec == null) return;
        rec.SourceType = Recorder.InputSourceType.AudioClip;
        rec.AudioClip = CreateVolumeAdjustedClip(clip, volume * MicrophoneVolume * MasterVolume);
        rec.LoopAudioClip = loop;
        rec.RestartRecording(true);
    }

    public static void StopMic()
    {
        Recorder R = GorillaTagger.Instance.myRecorder;
        if (R == null) return;
        R.SourceType = Recorder.InputSourceType.Microphone;
        R.AudioClip = null;
        R.RestartRecording(true);
    }

    public static void StopLocal()
    {
        if (LocalScr == null)
            return;

        LocalScr.Stop();

        IsPaused = false;
    }

    public static void StopAll()
    {
        StopMic();
        StopLocal();
    }

    private static AudioClip CreateVolumeAdjustedClip(AudioClip clip, float vol)
    {
        float[] dat = new float[clip.samples * clip.channels];
        clip.GetData(dat, 0);
        for (int h =0; h < dat.Length; h++)
            dat[h] *= vol;

        AudioClip C = AudioClip.Create(clip.name + "_Volume", clip.samples, clip.channels, clip.frequency, false);
        C.SetData(dat, 0);
        return C;
    }

    private static AudioType GetAudioType(string path)
    {
        string ext = Path.GetExtension(path).ToLower();
        return ext switch
        {
            ".mp3" => AudioType.MPEG,
            ".wav" => AudioType.WAV,
            ".ogg" => AudioType.OGGVORBIS,
            ".aiff" => AudioType.AIFF,
            _ => AudioType.UNKNOWN
        };
    }
}