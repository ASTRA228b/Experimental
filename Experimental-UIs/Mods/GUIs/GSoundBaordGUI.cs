using UnityEngine;
using Experimental.Core.MainManagers;
using static Experimental.Core.GUIHelpers.GlobalStyles;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.GUIs;

public static class GSoundBaordGUI
{
    public static void MakeGSoundBoardGUI()
    {
        if (GSoundsOpen)
        {
            GSoundBoardWindow = GUILayout.Window(GSoundBoardsID, GSoundBoardWindow, UIM, GSoundsWindowName, WindowStyle);
        }
    }

    public static void UIM(int id)
    {
        Mod();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            GSoundsOpen = !GSoundsOpen;
        }
        GUI.DragWindow();
    }

    public static void Mod()
    {
        GUILayout.BeginVertical();
        GsoundsTabInt =
            GUILayout.Toolbar(GsoundsTabInt, GSoundsTabs, Buttonss);

        switch (GsoundsTabInt)
        {
            case 0:
                MainMod();
                break;

            case 1:
                Settings();
                break;
        }

        GUILayout.EndVertical();
    }

    public static void MainMod()
    {
        if (GSoundGUIManager.Tabs.Count <= 0)
        {
            GUILayout.Label("No Sound Tabs Loaded");
            return;
        }
        GSoundGUIManager.SoundTab tab = GSoundGUIManager.Tabs[GSoundGUIManager.SelectedTab];
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.Width(250));
        SoundBoardScroll =GUILayout.BeginScrollView(SoundBoardScroll);
        for (int i = 0; i < tab.Cards.Count; i++)
        {
            if (GUILayout.Button(tab.Cards[i].Name,Buttonss))
            {
                _ = GSoundGUIManager.PlayCard(tab, i);
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label($"Current: {GSoundGUIManager.CurrentCard?.Name ?? "None"}");
        GUILayout.Label($"{SoundManager.CurrentTime:F1}s / {SoundManager.CurrentLength:F1}s");
        GUILayout.HorizontalSlider(SoundManager.CurrentProgress, 0f, 1f, SliderStyle, SliderThumbStyle);
        GUILayout.Space(5f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<", Buttonss))
        {
            _ = GSoundGUIManager.Previous();
        }
        if (GUILayout.Button(SoundManager.IsPaused ? "Play" : "Pause", Buttonss))
        {
            if (SoundManager.IsPaused)
                GSoundGUIManager.Resume();
            else
                GSoundGUIManager.Pause();
        }

        if (GUILayout.Button(">>", Buttonss))
        {
            _ = GSoundGUIManager.Next();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    public static void Settings()
    {
        GUILayout.Label("Master Volume");
        SoundManager.MasterVolume = GUILayout.HorizontalSlider( SoundManager.MasterVolume, 0f, 5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Volume: {SoundManager.MasterVolume:F2}");
        GUILayout.Space(5f);
        GUILayout.Label("Local Volume");
        SoundManager.LocalVolume = GUILayout.HorizontalSlider(SoundManager.LocalVolume, 0f, 5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Volume: {SoundManager.LocalVolume:F2}");
        GUILayout.Space(5f);
        GUILayout.Label("Microphone Volume");
        SoundManager.MicrophoneVolume = GUILayout.HorizontalSlider(SoundManager.MicrophoneVolume, 0f, 5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Volume: {SoundManager.MicrophoneVolume:F2}");
        GUILayout.Label("Sound Options");
        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(GSoundGUIManager.PlaybackMode == SoundManager.PlaybackMode.Local, "Local"))
        {
            GSoundGUIManager.PlaybackMode = SoundManager.PlaybackMode.Local;
        }
        if (GUILayout.Toggle(GSoundGUIManager.PlaybackMode == SoundManager.PlaybackMode.Microphone, "Microphone"))
        {
            GSoundGUIManager.PlaybackMode = SoundManager.PlaybackMode.Microphone;
        }
        if (GUILayout.Toggle(GSoundGUIManager.PlaybackMode == SoundManager.PlaybackMode.Both, "Both"))
        {
            GSoundGUIManager.PlaybackMode = SoundManager.PlaybackMode.Both;
        }
        GUILayout.EndHorizontal();
    }
}