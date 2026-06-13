using UnityEngine;

namespace Experimental.Core.MainManagers;

public static class GSoundGUIManager
{
    public static readonly List<SoundTab> Tabs = new();

    public static int SelectedTab;

    public static SoundCard? CurrentCard;

    public static int CurrentIndex;
    public static SoundManager.PlaybackMode PlaybackMode;

    public class SoundCard
    {
        public string? Name;
        public string? FileURL;
        public string? FileName;
        public Texture2D? Cover;

        public float Volume = 1f;

        public async Task PlayURL()
        {
            if (string.IsNullOrEmpty(FileURL)) return;
            AudioClip? clip = await SoundManager.LoadURL(FileURL);
            if (clip == null) return;
            SoundManager.PlayClip(clip, PlaybackMode, Volume);
        }

        public async Task PlayFile()
        {
            if (string.IsNullOrEmpty(FileName)) return;
            AudioClip? Clip = await SoundManager.LoadFile(FileName);
            if (Clip == null) return;
            SoundManager.PlayClip(Clip, PlaybackMode, Volume);
        }
    }

    public class SoundTab
    {
        public string Name = "";
        public List<SoundCard> Cards = new();
    }

    public static async Task PlayCard(SoundTab Tab, int I)
    {
        if (I < 0 || I >= Tab.Cards.Count) return;
        CurrentIndex = I;
        CurrentCard = Tab.Cards[I];
        await CurrentCard.PlayFile();
    }

    public static async Task Next()
    {
        SoundTab tab = Tabs[SelectedTab];
        int intded = CurrentIndex + 1;
        if (intded >= tab.Cards.Count) intded = 0;
        await PlayCard(tab, intded);
    }

    public static async Task Previous()
    {
        SoundTab T = Tabs[SelectedTab];
        int prev = CurrentIndex - 1;
        if (prev < 0) prev = T.Cards.Count - 1;
        await PlayCard(T, prev);
    }

    public static void Pause()
    {
        SoundManager.Pause();
    }

    public static void Resume()
    {
        SoundManager.Resume();
    }

    public static void Stop()
    {
        SoundManager.StopAll();
    }
}