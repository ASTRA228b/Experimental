using Experimental.Core.GUIHelpers;
using Experimental.Core.Preset;
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

    public static void SaveFile(string FileName, string Data)
    {
        File.WriteAllText(Path.Combine(RootFolder, FileName), Data);
    }
    public static string LoadFile(string FileName)
    {
        string path = Path.Combine(RootFolder, FileName);

        if (!File.Exists(path))
            return string.Empty;

        return File.ReadAllText(path);
    }

    public static void SaveGUISettings()
    {
        GlobalStyles.StyleColors colors = GlobalStyles.PullColors();
        GUISettingsData data = new()
        {
            WindowColor = colors.Window,
            ButtonColor = colors.Buttons,
            SliderTrackColor = colors.SliderTrack,
            SliderThumbColor = colors.SliderThumb
        };
        SaveFile("GUISettings.json", JsonUtility.ToJson(data, true));
    }

    public static void LoadGUISettings()
    {
        string json = LoadFile("GUISettings.json");
        if (string.IsNullOrEmpty(json))
        {
            SaveGUISettings();
            return;
        }
        GUISettingsData data = JsonUtility.FromJson<GUISettingsData>(json);
        GlobalStyles.SetColors(data.WindowColor, data.ButtonColor, data.SliderTrackColor, data.SliderThumbColor);
    }

    public static void LoadPreset() // Save & load everything like i said i was going to do
    {
        string Json = LoadFile("EPreset.json");
        if (string.IsNullOrEmpty(Json))
        {
            SavePreset();
            return;
        }
        PresetData dat = JsonUtility.FromJson<PresetData>(Json);
        PresetHelpers.Apply(dat);
    }

    public static void SavePreset()
    {
        PresetData Data = PresetHelpers.Pull();
        SaveFile("EPreset.json", JsonUtility.ToJson(Data, true));
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