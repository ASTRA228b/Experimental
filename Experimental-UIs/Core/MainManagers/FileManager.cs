using BepInEx;
using Experimental.Stuff;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Experimental.Core.MainManagers;

public class FileManager : MonoBehaviour
{
    public static string RootFolder => Path.Combine(Paths.GameRootPath, "Experimental");
    public static string SoundFolder => Path.Combine(RootFolder, "Sounds");
    private const string SoundUrl = "https://iimenu-lts-serverdata.vercel.app/nt4.mp3";

    private void Start()
    {
        SetupFoldes();
        string SoundPath = Path.Combine(SoundFolder, "nt4.mp3");
        if (!File.Exists(SoundPath))
            StartCoroutine(DownloadSounds(SoundPath));
    }

    private void SetupFoldes()
    {
        if (!Directory.Exists(SoundFolder))
            Directory.CreateDirectory(SoundFolder);

        if (!Directory.Exists(RootFolder))
            Directory.CreateDirectory(RootFolder);
    }

    private IEnumerator DownloadSounds(string SavePath)
    {
        using UnityWebRequest req = UnityWebRequest.Get(SoundUrl);
        yield return req.SendWebRequest();
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[{Constantss.GUID}] Failed to download sound: {req.error}");
            yield break;
        }
        File.WriteAllBytes(SavePath, req.downloadHandler.data);
        Debug.Log($"[{Constantss.GUID}] Downloaded nt4.mp3");
    }

    public static string[] GetSoundFiles()
    {
        if (!Directory.Exists(SoundFolder))
            return new string[0];

        return Directory.GetFiles(SoundFolder)
            .Where(x =>
                x.EndsWith(".mp3") ||
                x.EndsWith(".wav") ||
                x.EndsWith(".ogg"))
            .ToArray();
    }
}