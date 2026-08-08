using UnityEngine;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

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
            if (timeSettings != TimeSettingss.Morning)
            {
                timeSettings = TimeSettingss.Morning;
                SystemSwitch();
            }
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.TenAM, "10AM"))
        {
            if (timeSettings != TimeSettingss.TenAM)
            {
                timeSettings = TimeSettingss.TenAM;
                SystemSwitch();
            }
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Day, "Day"))
        {
            if (timeSettings != TimeSettingss.Day)
            {
                timeSettings = TimeSettingss.Day;
                SystemSwitch();
            }
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Evning, "Evening"))
        {
            if (timeSettings != TimeSettingss.Evning)
            {
                timeSettings = TimeSettingss.Evning;
                SystemSwitch();
            }
        }
        if (GUILayout.Toggle(timeSettings == TimeSettingss.Night, "Night"))
        {
            if (timeSettings != TimeSettingss.Night)
            {
                timeSettings = TimeSettingss.Night;
                SystemSwitch();
            }
        }
        GUILayout.Space(5f);
        GUILayout.Label("Weather");
        if (GUILayout.Button("Start Rain", Buttonss))
            StartRain();

        if (GUILayout.Button("Stop Rain", Buttonss))
            StopRain();
    }

    public static void StartRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int Yes = 1; Yes < manager.weatherCycle.Length; Yes++)
        {
            manager.weatherCycle[Yes] = (BetterDayNightManager.WeatherType)1;
        }
    }
    public static void StopRain()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null || manager.weatherCycle == null)
            return;
        for (int No = 1; No < manager.weatherCycle.Length; No++)
        {
            manager.weatherCycle[No] = (BetterDayNightManager.WeatherType)0;
        }
    }
    public static void SystemSwitch()
    {
        var manager = BetterDayNightManager.instance;
        if (manager == null)
            return;

        switch (timeSettings)
        {
            case TimeSettingss.Morning:
                manager.SetTimeOfDay(1, true);
                break;

            case TimeSettingss.TenAM:
                manager.SetTimeOfDay(3, true);
                break;

            case TimeSettingss.Day:
                manager.SetTimeOfDay(4, true);
                break;

            case TimeSettingss.Evning:
                manager.SetTimeOfDay(6, true);
                break;

            case TimeSettingss.Night:
                manager.SetTimeOfDay(0, true);
                break;
        }
    }

}