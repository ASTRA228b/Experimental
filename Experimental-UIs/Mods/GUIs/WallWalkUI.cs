using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Other;
using Experimental.Core.GUIHelpers;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;


namespace Experimental.Mods.GUIs;

public static class WallWalkUI
{
    public static void MakeWalkerUI()
    {
        if (WalkOpen)
        {
            WalkWindow.height = WalkDOpen ? 520 : 460;
            WalkWindow = GUILayout.Window(WalkerID, WalkWindow, UIM, WalkName, WindowStyle);
        }
    }

    public static void RunWalkerMod()
    {
        if (WallWalk)
        {
            Walker();
        }
    }

    public static void UIM(int walker)
    {
        Mod();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            WalkOpen = !WalkOpen;
        }
        GUI.DragWindow();
    }

    public static void Mod()
    {
        GUILayout.BeginVertical();
        CTabb = GUILayout.Toolbar(CTabb, WTabs, Buttonss);
        switch (CTabb)
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

    private static void MainMod()
    {
        GUILayout.Label("Enable WallWalk:");
        WallWalk = GUILayout.Toggle(WallWalk, "Enable WallWalk");
        GUILayout.Space(2f);
        GUILayout.Label("Set WallWalk Speed:");
        WallwalkSpeed = GUILayout.HorizontalSlider(WallwalkSpeed, 1f, 100f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Speed: {WallwalkSpeed:F1}");
        GUILayout.Space(2f);
        GUILayout.Label("Presets:");
        if (GUILayout.Button("Good Setting: (15)", Buttonss))
        {
            WallwalkSpeed = 15f;
        }
        if (GUILayout.Button("Fast Setting: (55)", Buttonss))
        {
            WallwalkSpeed = 55f;
        }
        if (GUILayout.Button("Random Setting", Buttonss))
        {
            WallwalkSpeed = UnityEngine.Random.Range(1f, 100f);
        }
        if (GUILayout.Button("Reset", Buttonss))
        {
            WallwalkSpeed = 0f;
        }
        GUILayout.Space(2f);
        GUILayout.Label($"Current input: {InputSelectors.UseWalkInputNames[InputSelectors.UseWalkIndex]}");
        GUILayout.Label("Go into Settings to change this");
    }

    private static void Settings()
    {
        GUILayout.Label("Set Inputs:");
        int oldIndex = InputSelectors.UseWalkIndex;
        InputSelectors.UseWalkIndex = MenuHelper.Dropdown(
          "walk_input",
          InputSelectors.UseWalkInputNames,
          InputSelectors.UseWalkIndex,
          GUILayout.Width(200)
        );
        dropdownOpen = oldIndex != InputSelectors.UseWalkIndex;
        GUILayout.Label($"Current input: {InputSelectors.UseWalkInputNames[InputSelectors.UseWalkIndex]}");
    }

    public static void Walker()
    {
        if (InputSelectors.UseWalkPressed)
        {
            GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(
                GTPlayer.Instance.bodyCollider.transform.forward * WallwalkSpeed,
                ForceMode.Acceleration
            );
        }
    }
}