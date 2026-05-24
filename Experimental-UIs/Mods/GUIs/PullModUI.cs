using UnityEngine;
using GorillaLocomotion;
using Experimental.Mods.Settings;
using Experimental.Core.GUIHelpers;
using Experimental.Core.Other;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;



namespace Experimental.Mods.GUIs;

public static class PullModUI
{
    public static void MakeUI()
    {
        if (Open)
        {
            Window.height = dropdownOpen ? 520 : 460;
            Window = GUILayout.Window(WindowID, Window, UIM, PName, WindowStyle);
        }
    }

    private static void UIM(int id)
    {
        Mod();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    private static void Mod()
    {
        GUILayout.Label("Change PullSpeed:");
        pullPower = GUILayout.HorizontalSlider(pullPower, 0.001f, 0.2f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Speed set to {pullPower:F3}");
        GUILayout.Label("Change UphillPower:");
        UpHillPull = GUILayout.HorizontalSlider(UpHillPull, 0.001f, 0.1f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Uphill set to {UpHillPull:F3}");
        GUILayout.Space(5f);
        GUILayout.Label("Change Input:");
        int OldIndex = InputSelectors.PSelectedIndex;
        InputSelectors.PSelectedIndex = MenuHelper.Dropdown(
            "EPM_Input",
            InputSelectors.PInputNames,
            InputSelectors.PSelectedIndex,
            GUILayout.Width(200)
        );
        dropdownOpen = OldIndex != InputSelectors.PSelectedIndex;
        GUILayout.Label($"Current input: {InputSelectors.PInputNames[InputSelectors.PSelectedIndex]}");
        GUILayout.Space(5f);
        if (GUILayout.Button("Speed Boost Setting: (25)", Buttonss))
        {
            pullPower = 0.025f;
            UpHillPull = 0.020f;
        }
        if (GUILayout.Button("Legit Setting: (70)", Buttonss))
        {
            pullPower = 0.070f;
            UpHillPull = 0.065f;
        }
        if (GUILayout.Button("Random Setting", Buttonss))
        {
            UpHillPull = UnityEngine.Random.Range(0.001f, 0.1f);
            pullPower = UnityEngine.Random.Range(0.001f, 0.2f);
        }
        if (GUILayout.Button("Reset", Buttonss))
        {
            pullPower = 0.001f;
            UpHillPull = 0.001f;
        }
        GUILayout.Space(5f);
        GUILayout.Label("Hand Options:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Both, "Both"))
        {
            CurrentMode = HandPullMode.Both;
        }
        GUILayout.Space(5f);
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Left, "Left"))
        {
            CurrentMode = HandPullMode.Left;
        }
        GUILayout.Space(5f);
        if (GUILayout.Toggle(CurrentMode == HandPullMode.Right, "Right"))
        {
            CurrentMode = HandPullMode.Right;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("PullMode: " + CurrentMode.ToString());
        GUILayout.Label("Good Speed Boost");
        Speed = GUILayout.Toggle(Speed, "Speed Boost (Good For Set 0.025)");
    }

    public static void RunMods()
    {
        PullMod();
        if (Speed)
        {
            GTPlayer.Instance.jumpMultiplier = Normalmuilty;
            GTPlayer.Instance.maxJumpSpeed = SpeedValue;
        }
    }

    private static void PullMod()
    {
        bool leftTouching = GTPlayer.Instance.IsHandTouching(true);
        bool rightTouching = GTPlayer.Instance.IsHandTouching(false);
        bool leftReleased = !leftTouching && lasttouchleft;
        bool rightReleased = !rightTouching && lasttouchright;
        if (InputSelectors.PPressed)
        {
            bool ShouldPull = CurrentMode switch
            {
                HandPullMode.Left => leftReleased,
                HandPullMode.Right => rightReleased,
                HandPullMode.Both => leftReleased || rightReleased,
                _ => false
            };
            if (ShouldPull)
            {
                Vector3 Vel = GorillaTagger.Instance.rigidbody.linearVelocity;
                GTPlayer.Instance.transform.position += new Vector3(Vel.x * pullPower, Vel.y * UpHillPull, Vel.z * pullPower);
            }
        }
        lasttouchleft = leftTouching;
        lasttouchright = rightTouching;
    }
}