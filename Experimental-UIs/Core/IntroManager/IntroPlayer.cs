using Experimental.Stuff;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace Experimental.Core.IntroManager;

public class IntroPlayer : MonoBehaviour
{
    private AudioSource? ExperimentalIntroAudio;

    private void Start()
    {
        ExperimentalIntroAudio = gameObject.AddComponent<AudioSource>();
        ExperimentalIntroAudio.playOnAwake = false;
        ExperimentalIntroAudio.loop = false;
        StartCoroutine(LoadAndPlay());
    }

    private IEnumerator LoadAndPlay()
    {
        string FileDataPath = GetFileFromAssembly();
        if (string.IsNullOrWhiteSpace(FileDataPath))
        {
            Debug.LogError($"[{Constantss.GUID}]: Failed To Extract File");
            yield break;
        }
        using UnityWebRequest IntroReq = UnityWebRequestMultimedia.GetAudioClip("file://" + FileDataPath, AudioType.MPEG);
        yield return IntroReq.SendWebRequest();
        if (IntroReq.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(IntroReq.error);
            yield break;
        }
        AudioClip IClip = DownloadHandlerAudioClip.GetContent(IntroReq);
        ExperimentalIntroAudio!.clip = IClip;
        Debug.Log($"[{Constantss.GUID}]: Playing StartUp Sound");
        ExperimentalIntroAudio?.Play();
        yield return new WaitForSeconds(IClip.length);
        Debug.Log($"[{Constantss.GUID}]: StartUp Stopped");
        ExperimentalIntroAudio!.Stop();
    }

    private string GetFileFromAssembly()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream ASMStream = assembly.GetManifestResourceStream("Experimental.RCS.ExperimentalStartSound.mp3");
        if (ASMStream == null)
        {
            Debug.LogError($"[{Constantss.GUID}]: Intro Audio File Was Null");
            return "";
        }
        string DataPath = Path.Combine(Application.persistentDataPath, "nt4.mp3");
        using FileStream NewFile = new(DataPath, FileMode.Create, FileAccess.Write);
        ASMStream.CopyTo(NewFile);
        return DataPath;
    }

}