using Experimental.Core.GUIHelpers;
using Fusion;
using UnityEngine; 
using static Experimental.Core.GUIHelpers.GlobalStyles;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.GUIs;

public static class ACamUtils
{
    public static void MakeCamUI()
    {
        if (CamUIOpen)
        {
            CamUiRect = GUILayout.Window(CamUIWId, CamUiRect, UIM, CamUIName, WindowStyle);
        }
    }

    public static void UIM(int h)
    {
        Mod();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            CamUIOpen = !CamUIOpen;
        }
        GUI.DragWindow();
    }

    public static void Mod()
    {
        GUILayout.BeginVertical();
        CamTabInt = GUILayout.Toolbar(CamTabInt, CamUITabNames, Buttonss);
        switch (CamTabInt)
        {
            case 0:
                FOV();
                break;
            case 1:
                CamSmoother();
                break;
        }
        GUILayout.EndVertical();
    }

    public static void FOV()
    {
        GUILayout.Label("Enable FOVs");
        GUILayout.BeginHorizontal();
        MainFOVEnabled = GUILayout.Toggle(MainFOVEnabled, "Enable Main FOV");
        PCFOVEnabled = GUILayout.Toggle(PCFOVEnabled, "Enable PC FOV (NW)");
        GUILayout.EndHorizontal();
        GUILayout.Label("Set FOVs");
        MainFOV = GUILayout.HorizontalSlider(MainFOV, 10f, 250f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Main FOV: {MainFOV:F1}");
        PCFOV = GUILayout.HorizontalSlider(PCFOV, 10f, 150f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"PC FOV (NW): {PCFOV:F1}");
        GUILayout.Label("Enable / Disable PC Camera");
        ThirdPersonEnabeld = GUILayout.Toggle(ThirdPersonEnabeld, "Third Person Camera Enabled");
    }

    public static void CamSmoother()
    {
        GUILayout.BeginVertical();
        MainEnabled = GUILayout.Toggle(MainEnabled, "Main Cam Smooth");
        LivEnabled = GUILayout.Toggle(LivEnabled, "LIV Smooth");
        GUILayout.EndVertical();
        GUILayout.Space(10);
        GUILayout.Label("Main Smooth: " +MainSmooth.ToString("F2"));
        MainSmooth = GUILayout.HorizontalSlider(MainSmooth, 0.01f, 0.5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label("Rotation Speed: " +RotSmooth.ToString("F1"));
        RotSmooth = GUILayout.HorizontalSlider(RotSmooth, 1f, 25f, SliderStyle, SliderThumbStyle);
        GUILayout.Space(10);
        GUILayout.Label("Kalman R: " +KalmanR.ToString("F0"));
        KalmanR = GUILayout.HorizontalSlider(KalmanR, 1f, 200f, SliderStyle, SliderThumbStyle);
    }


}