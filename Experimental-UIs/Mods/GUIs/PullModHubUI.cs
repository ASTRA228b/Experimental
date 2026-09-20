using Experimental.Mods.PullMods.AMPH;
using Experimental.Mods.PullMods.PMV2;
using Experimental.Mods.Settings;
using UnityEngine;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class PullModHubUI
{
    private static readonly string[] Tabs = { "Pull", "Methods", "Input" };
    private static int CurrentTab;
    private static bool Advanced;

    public static void MakeUI()
    {
        if (!GlobalVars.AMPHOpen)
            return;

        GlobalVars.AMPHWindow.width = 460f;
        GlobalVars.AMPHWindow.height = 470f;
        GlobalVars.AMPHWindow = GUILayout.Window(GlobalVars.AMPHWindowID, GlobalVars.AMPHWindow, UIM, "E - Astra's PullMod Hub", WindowStyle);
    }

    private static void UIM(int id)
    {
        CurrentTab = GUILayout.Toolbar(CurrentTab, Tabs, Buttonss);
        GUILayout.Space(10f);

        switch (CurrentTab)
        {
            case 0: Pull(); break;
            case 1: Methods(); break;
            case 2: Input(); break;
        }

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();

        GUILayout.Label($"Method: {AMPHMethods.Method}");

        if (GUILayout.Button("Close", Buttonss, GUILayout.Width(80)))
            GlobalVars.AMPHOpen = false;

        GUILayout.EndHorizontal();

        GUI.DragWindow();
    }

    private static void Pull()
    {
        bool enabled = GUILayout.Toggle(AMPHSystem.Enabled, "Pull Mod");

        if (enabled && !AMPHSystem.Enabled)
        {
            PMV2System.Enabled = false;
            AMPHSystem.Enabled = true;
        }
        else if (!enabled)
        {
            AMPHSystem.Enabled = false;
        }

        GUILayout.Space(10f);

        GUILayout.Label($"Pull Power: {AMPHMethods.PullPower:F3}");
        AMPHMethods.PullPower = GUILayout.HorizontalSlider(AMPHMethods.PullPower, 0.001f, 1f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Uphill Power: {AMPHMethods.UpHillPower:F3}");
        AMPHMethods.UpHillPower = GUILayout.HorizontalSlider(AMPHMethods.UpHillPower, 0.001f, 0.5f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Max Pull: {AMPHMethods.MaxPull:F3}");
        AMPHMethods.MaxPull = GUILayout.HorizontalSlider(AMPHMethods.MaxPull, 0.01f, 1f, SliderStyle, SliderThumbStyle);

        GUILayout.Space(5f);

        AMPHMethods.ClampVelocity = GUILayout.Toggle(AMPHMethods.ClampVelocity, "Velocity Clamp");

        if (AMPHMethods.ClampVelocity)
        {
            GUILayout.Label($"Max Velocity: {AMPHMethods.MaxVelocity:F1}");
            AMPHMethods.MaxVelocity = GUILayout.HorizontalSlider(AMPHMethods.MaxVelocity, 5f, 150f, SliderStyle, SliderThumbStyle);
        }

        GUILayout.Space(5f);

        if (GUILayout.Button(Advanced ? "Advanced ▲" : "Advanced ▼", Buttonss))
            Advanced = !Advanced;

        if (Advanced)
        {
            GUILayout.Space(5f);

            GUILayout.Label($"Momentum: {AMPHMethods.Momentum:F4}");
            AMPHMethods.Momentum = GUILayout.HorizontalSlider(AMPHMethods.Momentum, 0f, 0.05f, SliderStyle, SliderThumbStyle);

            GUILayout.Label($"Extra Power: {AMPHMethods.ExtraPower:F2}");
            AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
        }

        GUILayout.Space(5f);

        if (GUILayout.Button("Reset Pull", Buttonss))
            AMPHMethods.Reset();
    }

    private static void Methods()
    {
        GUILayout.Label("Pull Methods");

        int method = (int)AMPHMethods.Method;
        method = GUILayout.SelectionGrid(method, AMPHMethods.MethodNames, 3, Buttonss);
        AMPHMethods.Method = (AMPHMethods.PullMethod)method;

        GUILayout.Space(10f);

        GUILayout.Label($"Selected: {AMPHMethods.Method}");
        GUILayout.Label(MethodInfo());

        GUILayout.Space(10f);

        MethodSettings();
    }

    private static void MethodSettings()
    {
        switch (AMPHMethods.Method)
        {
            case AMPHMethods.PullMethod.PMV1:
                GUILayout.Label("Classic Astra's PullMod V1.");
                break;

            case AMPHMethods.PullMethod.PMV2:
                GUILayout.Label($"Momentum: {AMPHMethods.Momentum:F4}");
                AMPHMethods.Momentum = GUILayout.HorizontalSlider(AMPHMethods.Momentum, 0f, 0.05f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Regular:
                GUILayout.Label($"Forward Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Gravity:
                GUILayout.Label($"Gravity Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Surface:
                GUILayout.Label("Projects movement along the surface below you.");
                break;

            case AMPHMethods.PullMethod.Burst:
                GUILayout.Label($"Burst Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Inertia:
                GUILayout.Label($"Inertia Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Elastic:
                GUILayout.Label($"Elastic Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Directional:
                GUILayout.Label($"Directional Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.Curve:
                GUILayout.Label($"Curve Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.AirPull:
                GUILayout.Label($"Air Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;

            case AMPHMethods.PullMethod.GroundPull:
                GUILayout.Label($"Ground Power: {AMPHMethods.ExtraPower:F2}");
                AMPHMethods.ExtraPower = GUILayout.HorizontalSlider(AMPHMethods.ExtraPower, 0.1f, 10f, SliderStyle, SliderThumbStyle);
                break;
        }
    }

    private static string MethodInfo()
    {
        return AMPHMethods.Method switch
        {
            AMPHMethods.PullMethod.PMV1 => "Astra's PullMod V1",
            AMPHMethods.PullMethod.PMV2 => "Astra's PullMod V2",
            AMPHMethods.PullMethod.Regular => "Velocity & forward movement",
            AMPHMethods.PullMethod.Gravity => "Pull with upward assistance",
            AMPHMethods.PullMethod.Surface => "Follows the surface below you",
            AMPHMethods.PullMethod.Burst => "Adds a velocity burst",
            AMPHMethods.PullMethod.Inertia => "Adds momentum directly into velocity",
            AMPHMethods.PullMethod.Elastic => "Speed based elastic pull",
            AMPHMethods.PullMethod.Directional => "Stronger when moving forward",
            AMPHMethods.PullMethod.Curve => "Non-linear speed scaling",
            AMPHMethods.PullMethod.AirPull => "Stronger while airborne",
            AMPHMethods.PullMethod.GroundPull => "Stronger while touching",
            _ => ""
        };
    }

    private static void Input()
    {
        GUILayout.Label("Input");

        int input = AMPHInput.SelectedIndex;
        input = GUILayout.SelectionGrid(input, AMPHInput.InputNames, 4, Buttonss);
        AMPHInput.SelectedIndex = input;

        GUILayout.Space(10f);
        GUILayout.Label("Hand");

        int hand = (int)AMPHSystem.Hand;
        hand = GUILayout.Toolbar(hand, new[] { "Both", "Left", "Right" }, Buttonss);
        AMPHSystem.Hand = (AMPHSystem.HandMode)hand;

        GUILayout.Space(10f);
        GUILayout.Label("Activation");

        int activation = (int)AMPHSystem.Activation;
        activation = GUILayout.Toolbar(activation, new[] { "Release", "Touch", "Hold" }, Buttonss);
        AMPHSystem.Activation = (AMPHSystem.ActivationMode)activation;

        GUILayout.Space(10f);

        GUILayout.Label($"Release Window: {AMPHSystem.ReleaseWindow:F2}");
        AMPHSystem.ReleaseWindow = GUILayout.HorizontalSlider(AMPHSystem.ReleaseWindow, 0.01f, 0.5f, SliderStyle, SliderThumbStyle);

        GUILayout.Label($"Touch Linger: {AMPHSystem.TouchLinger:F2}");
        AMPHSystem.TouchLinger = GUILayout.HorizontalSlider(AMPHSystem.TouchLinger, 0f, 0.5f, SliderStyle, SliderThumbStyle);

        GUILayout.Space(10f);

        if (GUILayout.Button("Reset Input", Buttonss))
        {
            AMPHInput.Reset();
            AMPHSystem.Hand = AMPHSystem.HandMode.Both;
            AMPHSystem.Activation = AMPHSystem.ActivationMode.Release;
            AMPHSystem.ReleaseWindow = 0.10f;
            AMPHSystem.TouchLinger = 0.06f;
        }
    }
}