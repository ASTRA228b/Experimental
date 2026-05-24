using UnityEngine;
using Experimental.Core.GUIHelpers;
using Experimental.Mods.Settings;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;
using static BetterDayNightManager;

namespace Experimental.Mods.GUIs;

public static class GorillaTimeUI
{
    public static void MakeGTimeUI()
    {
        if (GTVOpen)
        {
            GTWindow = GUILayout.Window(GTVID, GTWindow, UIM, GTIMEName, WindowStyle);
        }
    }

    public static void RunTMod()
    {
        SystemSwitch();
    }

    public static void UIM(int TID)
    {
        GTimeController();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            GTVOpen = !GTVOpen;
        }
        GUI.DragWindow();
    }

    public static void GTimeController()
    {
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Morning, "Morning"))
        {
            timeSettings = TimeSettingss.Morning;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.TenAM, "10AM"))
        {
            timeSettings = TimeSettingss.TenAM;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Day, "Day"))
        {
            timeSettings = TimeSettingss.Day;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Evning, "Evening"))
        {
            timeSettings = TimeSettingss.Evning;
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Night, "Night"))
        {
            timeSettings = TimeSettingss.Night;
        }
        GUILayout.Space(5f);
        GUILayout.Label("Weather");
        if (GUILayout.Button("Start Rain", Buttonss))
        {
            StartRain();
        }
        if (GUILayout.Button("Stop Rain", Buttonss))
        {
            StopRain();
        }
    }

    public static void StartRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int Yes = 1; Yes < manager.weatherCycle.Length; Yes++)
        {
            manager.weatherCycle[Yes] = (WeatherType)1;
        }
    }
    public static void StopRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int No = 1; No < manager.weatherCycle.Length; No++)
        {
            manager.weatherCycle[No] = (WeatherType)0;
        }
    }
    public static void SystemSwitch()
    {
        switch (timeSettings)
        {
            case TimeSettingss.Morning:
                BetterDayNightManager.instance.SetTimeOfDay(1);
                break;
            case TimeSettingss.TenAM:
                BetterDayNightManager.instance.SetTimeOfDay(3);
                break;
            case TimeSettingss.Day:
                BetterDayNightManager.instance.SetTimeOfDay(4);
                break;
            case TimeSettingss.Evning:
                BetterDayNightManager.instance.SetTimeOfDay(6);
                break;
            case TimeSettingss.Night:
                BetterDayNightManager.instance.SetTimeOfDay(0);
                break;
        }
    }

}