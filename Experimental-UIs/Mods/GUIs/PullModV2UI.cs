using Experimental.Core.GUIHelpers;
using Experimental.Core.Other;
using Experimental.Mods.PullMods.AMPH;
using Experimental.Mods.PullMods.PMV2;
using Experimental.Mods.Settings;
using UnityEngine;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class PullModV2UI
{
    private static readonly string[] Tabs = { "Pull", "Speed", "Settings" };
    private static int CurrentTab;
    private static bool Advanced;

    public static void MakeUI()
    {
        if (!GlobalVars.PMV2Open)
            return;

        GlobalVars.PMV2Window.width = 400f;
        GlobalVars.PMV2Window.height = 430f;
        GlobalVars.PMV2Window = GUILayout.Window(GlobalVars.PMV2WindowID, GlobalVars.PMV2Window, UIM, "E - Astra's PullMod V2", WindowStyle);
    }

    private static void UIM(int id)
    {
        CurrentTab = GUILayout.Toolbar(CurrentTab, Tabs, Buttonss);
        GUILayout.Space(10f);

        switch (CurrentTab)
        {
            case 0: Pull(); break;
            case 1: Speed(); break;
            case 2: Settings(); break;
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Close", Buttonss))
            GlobalVars.PMV2Open = false;

        GUI.DragWindow();
    }

    private static void Pull()
    {
        bool enabled = GUILayout.Toggle(PMV2System.Enabled, "Pull Mod");

        if (enabled && !PMV2System.Enabled)
        {
            AMPHSystem.Enabled = false;
            PMV2System.Enabled = true;
        }
        else if (!enabled)
        {
            PMV2System.Enabled = false;
        }

        GUILayout.Space(5f);
        GUILayout.Label("Mode");

        int mode = (int)PMV2System.Mode;
        mode = GUILayout.Toolbar(mode, new[] { "Stable", "Dynamic", "Strong" }, Buttonss);
        PMV2System.Mode = (PMV2System.PullMode)mode;

        GUILayout.Space(10f);

        GUILayout.Label($"Pull Power: {PMV2System.PullPower:F3}");
        PMV2System.PullPower = GUILayout.HorizontalSlider(PMV2System.PullPower, 0.001f, 1f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Uphill Power: {PMV2System.UpHillPower:F3}");
        PMV2System.UpHillPower = GUILayout.HorizontalSlider(PMV2System.UpHillPower, 0.001f, 0.5f, SliderStyle, SliderThumbStyle);

        GUILayout.Space(5f);
        GUILayout.Label("Presets");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Speed", Buttonss))
        {
            PMV2System.PullPower = 0.025f;
            PMV2System.UpHillPower = 0.020f;
            PMV2System.Momentum = 0.0005f;
            PMV2System.MaxPull = 0.050f;
            PMV2System.Mode = PMV2System.PullMode.Stable;
        }

        if (GUILayout.Button("Legit", Buttonss))
        {
            PMV2System.PullPower = 0.070f;
            PMV2System.UpHillPower = 0.065f;
            PMV2System.Momentum = 0.001f;
            PMV2System.MaxPull = 0.090f;
            PMV2System.Mode = PMV2System.PullMode.Dynamic;
        }

        if (GUILayout.Button("Strong", Buttonss))
        {
            PMV2System.PullPower = 0.100f;
            PMV2System.UpHillPower = 0.075f;
            PMV2System.Momentum = 0.002f;
            PMV2System.MaxPull = 0.150f;
            PMV2System.Mode = PMV2System.PullMode.Strong;
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(5f);

        if (GUILayout.Button(Advanced ? "Advanced ▲" : "Advanced ▼", Buttonss))
            Advanced = !Advanced;

        if (!Advanced)
            return;

        GUILayout.Space(5f);

        GUILayout.Label($"Momentum: {PMV2System.Momentum:F4}");
        PMV2System.Momentum = GUILayout.HorizontalSlider(PMV2System.Momentum, 0f, 0.05f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Max Pull: {PMV2System.MaxPull:F3}");
        PMV2System.MaxPull = GUILayout.HorizontalSlider(PMV2System.MaxPull, 0.01f, 1f, SliderStyle, SliderThumbStyle);

        PMV2System.ClampVelocity = GUILayout.Toggle(PMV2System.ClampVelocity, "Velocity Clamp");

        if (PMV2System.ClampVelocity)
        {
            GUILayout.Label($"Max Velocity: {PMV2System.MaxVelocity:F1}");
            PMV2System.MaxVelocity = GUILayout.HorizontalSlider(PMV2System.MaxVelocity, 5f, 150f, SliderStyle, SliderThumbStyle);
        }

        GUILayout.Space(5f);

        if (GUILayout.Button("Reset Pull", Buttonss))
            PMV2System.ResetPull();
    }

    private static void Speed()
    {
        PMV2SpeedSystem.Enabled = GUILayout.Toggle(PMV2SpeedSystem.Enabled, "Speed Boost");

        GUILayout.Space(10f);

        GUILayout.Label($"Jump Speed: {PMV2SpeedSystem.Speed:F1}");
        PMV2SpeedSystem.Speed = GUILayout.HorizontalSlider(PMV2SpeedSystem.Speed, 1f, 15f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Jump Multiplier: {PMV2SpeedSystem.Multiplier:F2}");
        PMV2SpeedSystem.Multiplier = GUILayout.HorizontalSlider(PMV2SpeedSystem.Multiplier, 1f, 3f, SliderStyle, SliderThumbStyle);

        GUILayout.Space(5f);

        PMV2SpeedSystem.OnlyWithPull = GUILayout.Toggle(PMV2SpeedSystem.OnlyWithPull, "Only With Pull Mod");

        GUILayout.Space(10f);
        GUILayout.Label("Presets");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Normal", Buttonss))
            PMV2SpeedSystem.Normal();

        if (GUILayout.Button("Boost", Buttonss))
            PMV2SpeedSystem.Boost();

        if (GUILayout.Button("Strong", Buttonss))
            PMV2SpeedSystem.Strong();

        GUILayout.EndHorizontal();

        GUILayout.Space(10f);

        if (GUILayout.Button("Reset Speed", Buttonss))
            PMV2SpeedSystem.Reset();
    }

    private static void Settings()
    {
        GUILayout.Label("Input");

        InputSelectors.PMV2SelectedIndex = MenuHelper.Dropdown(
           "EPMV2_Input",
           InputSelectors.PMV2InputNames,
           InputSelectors.PMV2SelectedIndex,
           GUILayout.Width(200)
        );

        GUILayout.Space(5f);
        GUILayout.Label($"Current Input: {InputSelectors.PMV2InputNames[InputSelectors.PMV2SelectedIndex]}");

        GUILayout.Space(10f);
        GUILayout.Label("Hand");

        int hand = (int)PMV2System.Hand;
        hand = GUILayout.Toolbar(hand, new[] { "Both", "Left", "Right" }, Buttonss);
        PMV2System.Hand = (PMV2System.HandMode)hand;

        GUILayout.Space(10f);
        GUILayout.Label("Activation");

        int activation = (int)PMV2System.Activation;
        activation = GUILayout.Toolbar(activation, new[] { "Release", "Touch", "Hold" }, Buttonss);
        PMV2System.Activation = (PMV2System.ActivationMode)activation;

        GUILayout.Space(10f);

        if (GUILayout.Button("Reset Settings", Buttonss))
        {
            PMV2System.ResetPull();
            PMV2System.Hand = PMV2System.HandMode.Both;
            PMV2System.Activation = PMV2System.ActivationMode.Release;
            InputSelectors.PMV2SelectedIndex = 0;
        }
    }
}