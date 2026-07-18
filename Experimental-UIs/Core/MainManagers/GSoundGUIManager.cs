using UnityEngine;

namespace Experimental.Core.MainManagers;

public static class GSoundGUIManager
{
    public static readonly List<SoundTab> Tabs = new();

    public static int SelectedTab;

    public static SoundCard? CurrentCard;

    public static int CurrentIndex;
    public static SoundManager.PlaybackMode PlaybackMode;
    public static bool ShuffleEnabled;
    public static bool AutoPlayNext = true;
    private static int lastRandomIndex = -1;
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
        if (!string.IsNullOrEmpty(CurrentCard.FileName))
        {
            await CurrentCard.PlayFile();
        }
        else if (!string.IsNullOrEmpty(CurrentCard.FileURL))
        {
            await CurrentCard.PlayURL();
        }
    }

    public static async Task Next()
    {
        if (Tabs.Count == 0)
            return;
        SoundTab tab = Tabs[SelectedTab];
        if (tab.Cards.Count == 0)
            return;
        int nextIndex;
        if (ShuffleEnabled && tab.Cards.Count > 1)
        {
            do
            {
                nextIndex = UnityEngine.Random.Range(0, tab.Cards.Count);
            }
            while (nextIndex == CurrentIndex);
            lastRandomIndex = nextIndex;
        }
        else
        {
            nextIndex = CurrentIndex + 1;
            if (nextIndex >= tab.Cards.Count)
                nextIndex = 0;
        }
        await PlayCard(tab, nextIndex);
    }

    public static async Task Previous()
    {
        if (Tabs.Count == 0)
            return;
        SoundTab tab = Tabs[SelectedTab];
        if (tab.Cards.Count == 0)
            return;
        int previousIndex = CurrentIndex - 1;
        if (previousIndex < 0)
            previousIndex = tab.Cards.Count - 1;
        await PlayCard(tab, previousIndex);
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
    public static void LoadSounds()
    {
        Tabs.Clear();

        SoundTab main = new()
        {
            Name = "Main"
        };

        foreach (string file in FileManager.GetSoundFiles())
        {
            main.Cards.Add(new SoundCard
            {
                Name = Path.GetFileNameWithoutExtension(file),
                FileName = file
            });
        }

        Tabs.Add(main);
    }
}