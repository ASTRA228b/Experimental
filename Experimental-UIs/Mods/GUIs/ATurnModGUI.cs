using UnityEngine;
using Experimental.Core.Other;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;
using Experimental.Core.GUIHelpers;


namespace Experimental.Mods.GUIs;

public static class ATurnModGUI
{
    public static void MakeATurnModGUI()
    {
        if (ATurnWindowOpen)
        {
            ATurnModWindow.height = ATDropDownOpen ? 520 : 460;
            ATurnModWindow = GUILayout.Window(ATrunModWindowInt, ATurnModWindow, UIM, ATrunModGUIName, WindowStyle);
        }
    }

    public static void UIM(int id)
    {
        Mod();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            ATurnWindowOpen = !ATurnWindowOpen;
        }
        GUI.DragWindow();
    }

    public static void Mod()
    {
        GUILayout.BeginVertical();
        ATurnModTabInt = GUILayout.Toolbar(ATurnModTabInt, ATurnModTabs, Buttonss);
        switch (ATurnModTabInt)
        {
            case 0:
                MMainMod(); break;
            case 1:
                Settings(); break;
        }
        GUILayout.EndVertical();
    }

    public static void MMainMod()
    {
        GUILayout.Label("Enable TrunMod");
        TM = GUILayout.Toggle(TM, "Enable TM");
        GUILayout.Space(5f);
        GUILayout.Label("Enable SnapTrun");
        ST = GUILayout.Toggle(ST, "Enable ST");
        if (ST)
        {
            SnapAngle = GUILayout.HorizontalSlider(SnapAngle, 15f, 90f, SliderStyle, SliderThumbStyle);
            GUILayout.Label($"Snap Angle: {Mathf.RoundToInt(SnapAngle)}°");
        }
        else
        {
            TurnSpeed = GUILayout.HorizontalSlider(TurnSpeed, 30f, 900f, SliderStyle, SliderThumbStyle);
            GUILayout.Label($"Turn Speed: {Mathf.Round(TurnSpeed)}°/s");
            Smoothnes = GUILayout.HorizontalSlider(Smoothnes, 0f, 5f, SliderStyle, SliderThumbStyle);
            GUILayout.Label($"Smoothness: {Smoothnes:F2}");
        }
        GUILayout.Space(2f);
        GUILayout.Label($"Current inputs: L:{InputSelectors.ATLInputNames[InputSelectors.ATLSelectedIndex]} R:{InputSelectors.ATRInputNames[InputSelectors.ATRSelectedIndex]}");
        GUILayout.Label("Go into Settings to change this");
    }

    public static void Settings()
    {
        GUILayout.Label("Set Inputs:");
        int OldIndex = InputSelectors.ATLSelectedIndex;
        int newIndex = InputSelectors.ATRSelectedIndex;
        InputSelectors.ATLSelectedIndex = MenuHelper.Dropdown(
            "TML_Input",
            InputSelectors.ATLInputNames,
            InputSelectors.ATLSelectedIndex,
            GUILayout.Width(200)
        );
        InputSelectors.ATRSelectedIndex = MenuHelper.Dropdown(
           "TMR_Input",
           InputSelectors.ATRInputNames,
           InputSelectors.ATRSelectedIndex,
           GUILayout.Width(200)
        );
        dropdownOpen = OldIndex != InputSelectors.ATLSelectedIndex;
        dropdownOpen = newIndex != InputSelectors.ATRSelectedIndex;
        GUILayout.Label($"Current inputs: L:{InputSelectors.ATLInputNames[InputSelectors.ATLSelectedIndex]} R:{InputSelectors.ATRInputNames[InputSelectors.ATRSelectedIndex]}");
    }
}