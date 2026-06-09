using UnityEngine;
using Experimental.Core.Other;
using Experimental.Core.GUIHelpers;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class APitGeoModUI
{
    public static void MakePitGeoUI()
    {
        if (PitGeoGUIOpen)
        {
            PitGeoWindow.height = PitGeoDropOpen ? 520 : 460;
            PitGeoWindow = GUILayout.Window(PitGeoWindowID, PitGeoWindow, UIM, PitGeoWindowName, WindowStyle);
        }
    }

    public static void UIM(int windid)
    {
        Mod();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    public static void Mod()
    {
        GUILayout.BeginVertical();
        PitGeoTabsIntager = GUILayout.Toolbar(PitGeoTabsIntager, PitGeoTabs, Buttonss);
        switch (PitGeoTabsIntager)
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
        GUILayout.Label("Enable PitGeo");
        PitModOnn = GUILayout.Toggle(PitModOnn, "Enable");
        GUILayout.Space(2f);
        GUILayout.Label("Options:");
        GUILayout.Label("SlipWall");
        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(SlipOptions == SlipWallPitOptions.None, "None"))
        {
            SlipOptions = SlipWallPitOptions.None;
        }
        if (GUILayout.Toggle(SlipOptions == SlipWallPitOptions.UpperSlipWall, "Upper Wall"))
        {
            SlipOptions = SlipWallPitOptions.UpperSlipWall;
        }
        if (GUILayout.Toggle(SlipOptions == SlipWallPitOptions.LowerSlipWall, "Lower Wall"))
        {
            SlipOptions = SlipWallPitOptions.LowerSlipWall;
        }
        if (GUILayout.Toggle(SlipOptions == SlipWallPitOptions.BothSlipWalls, "Both"))
        {
            SlipOptions = SlipWallPitOptions.BothSlipWalls;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("Pit Ground");
        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(GroundOptions == PitGroundOptions.None, "None"))
        {
            GroundOptions = PitGroundOptions.None;
        }
        if (GUILayout.Toggle(GroundOptions == PitGroundOptions.PitGroundTop, "Top Ground"))
        {
            GroundOptions = PitGroundOptions.PitGroundTop;
        }
        if (GUILayout.Toggle(GroundOptions == PitGroundOptions.PitGroundBottom, "Bottom Ground"))
        {
            GroundOptions = PitGroundOptions.PitGroundBottom;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("Speed Settings:");
        GUILayout.Label("Slip Wall:");
        WallSlipMult = GUILayout.HorizontalSlider(WallSlipMult, 1f, 25f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"WMult1: {WallSlipMult:F2}");
        WallSlipMultOther = GUILayout.HorizontalSlider(WallSlipMultOther, 1f, 30f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"WMult2: {WallSlipMultOther:F2}");
        GUILayout.Label("Pit Ground:");
        GroundMult = GUILayout.HorizontalSlider(GroundMult, 1f, 25f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"GMult1: {GroundMult:F2}");
        GroundMultOther = GUILayout.HorizontalSlider(GroundMultOther, 1f, 30f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"GMult2: {GroundMultOther:F2}");
        GUILayout.Label($"Current Inputs: {InputSelectors.PitGInputNames[InputSelectors.PitGSelectedIndex]}");
        GUILayout.Label("Go into Setting to change this");
    }

    public static void Settings()
    {
        GUILayout.Label("Set Inputs:");
        int OldIndex = InputSelectors.PitGSelectedIndex;
        InputSelectors.PitGSelectedIndex = MenuHelper.Dropdown(
            "PitGeo_Inpit",
            InputSelectors.PitGInputNames,
            InputSelectors.PitGSelectedIndex,
            GUILayout.Width(200)
        );
        GUILayout.Label("Current Inputs:");
        GUILayout.Label($"{InputSelectors.PitGInputNames[InputSelectors.PitGSelectedIndex]}");
    }
}