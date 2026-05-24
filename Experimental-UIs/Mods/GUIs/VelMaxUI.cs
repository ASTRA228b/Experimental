using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Other;
using Experimental.Core.GUIHelpers;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class VelMaxUI
{
    public static void MakeVelMaxUI()
    {
        if (VOpen)
        {
            MRect.height = OpenDropdown ? 530 : 460;
            MRect = GUILayout.Window(VID, MRect, UIM, VName, WindowStyle);
        }
    }

    public static void RunVMod()
    {
        if (Velmax)
        {
            VelMaxMod();
        }
    }

    public static void UIM(int Vid)
    {
        MMod();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            VOpen = !VOpen;
        }
        GUI.DragWindow();
    }

    public static void MMod()
    {
        GUILayout.Label("Enable Velmax");
        Velmax = GUILayout.Toggle(Velmax, "Enable Velmax");
        GUILayout.Space(5f);
        GUILayout.Label("Sliders");
        VelMulti = GUILayout.HorizontalSlider(VelMulti, 0.001f, 12f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"VelMult: {VelMulti:F3}");
        VelMax = GUILayout.HorizontalSlider(VelMax, 0.001f, 10f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"VelMax: {VelMax:F3}");
        GUILayout.Space(5f);
        GUILayout.Label("Change Input:");
        int OldIndex = InputSelectors.VSelectedIndex;
        InputSelectors.VSelectedIndex = MenuHelper.Dropdown(
            "VelMax",
            InputSelectors.VInputNames,
            InputSelectors.VSelectedIndex,
            GUILayout.Width(200)
        );
        OpenDropdown = OldIndex != InputSelectors.VSelectedIndex;
        GUILayout.Label($"Current input: {InputSelectors.InputNames[InputSelectors.VSelectedIndex]}");
        GUILayout.Label("Presets:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset", Buttonss))
        {
            VelMulti = 0.001f;
            VelMax = 0.001f;
        }
        if (GUILayout.Button("Max", Buttonss))
        {
            VelMulti = 12f;
            VelMax = 10f;
        }
        if (GUILayout.Button("Random", Buttonss))
        {
            VelMulti = UnityEngine.Random.Range(0.5f, 12f);
            VelMax = UnityEngine.Random.Range(0.5f, 10f);
        }
        GUILayout.EndHorizontal();
    }

    public static void VelMaxMod()
    {
        if (GTPlayer.Instance == null) return;
        float mult = (Velmax && InputSelectors.VPressed) ? VelMulti : 1f;
        float max = (Velmax && InputSelectors.VPressed) ? VelMax : 1f;
        var Left = GTPlayer.Instance.LeftHand.surfaceOverride;
        var Right = GTPlayer.Instance.RightHand.surfaceOverride;
        if (Left != null)
        {
            Left.extraVelMultiplier = mult;
            Left.extraVelMaxMultiplier = max;
        }

        if (Right != null)
        {
            Right.extraVelMultiplier = mult;
            Right.extraVelMaxMultiplier = max;
        }
    }
}