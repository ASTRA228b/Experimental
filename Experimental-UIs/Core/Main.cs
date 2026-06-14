using Experimental.Core.GUIHelpers;
using Experimental.Core.MainManagers;
using Experimental.Mods.GUIs;
using Experimental.Mods.GUIs.total_chaos.All_apply;
using Experimental.Mods.OtherUtils;
using Experimental.Mods.Settings;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Experimental.Core;

public class Main : MonoBehaviour
{
    // internal vars (global vars are just for the mods)
    private Rect Window = new(200, 110, 550, 250);
    private bool Open = false;
    private bool SLoaded = false;
    private int TabInt = 0;
    private readonly string[] Tabs = { "GUIs", "Utils", "Settings", "SB" };
    // for room stuff
    private string roomcode = "";
    // yes 
    private Rect StyleChangerRect = new(300, 150, 400, 450);
    private bool StyleChangerOpne = false;

    private void OnGUI()
    {
        if (!SLoaded)
        {
            GlobalStyles.INIT();
            FileManager.LoadGUISettings();
            SLoaded = true;
        }
        if (Open)
        {
            Window = GUILayout.Window(2223213, Window, UIM, "Experimental", GlobalStyles.WindowStyle);
        }
        // loading all the other uis
        PullModUI.MakeUI();
        ApredsUI.MakePredsUI();
        GorillaTimeUI.MakeGTimeUI();
        PSAModUI.MakePSAModUI();
        VelMaxUI.MakeVelMaxUI();
        WallWalkUI.MakeWalkerUI();
        ATurnModGUI.MakeATurnModGUI();
        APitGeoModUI.MakePitGeoUI();
        GSoundBaordGUI.MakeGSoundBoardGUI();
        if (StyleChangerOpne)
        {
            StyleChangerRect = GUILayout.Window(7654398, StyleChangerRect, Style, "E - StyleChanger", GlobalStyles.WindowStyle);
        }
    }

    private void Update()
    {
        // Only For Apreds
        ApredsUI.RunMod(); // Apreds
        // Keybind
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Open = !Open;
        }
    }

    private void FixedUpdate()
    {
        // running RUNMODS Methods
        PullModUI.RunMods(); // PullMod
        GorillaTimeUI.RunTMod(); // Gorilla TimeV2
        PSAModUI.RunPSAMod(); // PSA Mod
        VelMaxUI.RunVMod(); // VelMaxMod
        WallWalkUI.RunWalkerMod(); // WallWalk
    }


    private void UIM(int id)
    {
        MMod();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", GlobalStyles.Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    private void MMod()
    {
        GUILayout.BeginVertical();
        TabInt = GUILayout.Toolbar(TabInt, Tabs, GlobalStyles.Buttonss);
        switch (TabInt)
        {
            case 0:
                GUIS();
                break;
            case 1:
                Utils();
                break;

            case 2:
                Settings();
                break;

            case 3:
                SoundBoard();
                break;
        }
        GUILayout.EndVertical();
    }

    private void GUIS()
    {
        GUILayout.Label("Guis");
        GUILayout.BeginHorizontal();
        GlobalVars.Open = GUILayout.Toggle(GlobalVars.Open, "Astras PM V1");
        GlobalVars.IsOpen = GUILayout.Toggle(GlobalVars.IsOpen, "Apreds UI");
        GlobalVars.GTVOpen = GUILayout.Toggle(GlobalVars.GTVOpen, "Gorilla TimeV2");
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
        GUILayout.BeginHorizontal();
        GlobalVars.PSAOpen = GUILayout.Toggle(GlobalVars.PSAOpen, "Astras PSA Mod");
        GlobalVars.VOpen = GUILayout.Toggle(GlobalVars.VOpen, "Astras VelMax Mod");
        GlobalVars.WalkOpen = GUILayout.Toggle(GlobalVars.WalkOpen, "AWallWalk V2");
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
        GUILayout.BeginHorizontal();
        GlobalVars.ATurnWindowOpen = GUILayout.Toggle(GlobalVars.ATurnWindowOpen, "Astras TurnMod");
        GlobalVars.PitGeoGUIOpen = GUILayout.Toggle(GlobalVars.PitGeoGUIOpen, "Astras PitGeo GUI");
        GUILayout.EndHorizontal();
    }

    private void Utils()
    {
        GUILayout.Label("Room Utils");
        GUILayout.BeginHorizontal();
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom != null)
        {
            GUILayout.Label("In Room: " + PhotonNetwork.CurrentRoom.Name);
            GUILayout.Label($"Players: {PhotonNetwork.CurrentRoom.PlayerCount}/10");
        }
        else
        {
            GUILayout.Label("Not in a room");
            GUILayout.Label("Players: 0/10");
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
        GUILayout.Label("Enter Code:");
        roomcode = GUILayout.TextField(roomcode, 10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Join Room: " + roomcode, GlobalStyles.Buttonss))
        {
            RoomMods.JoinRoom(roomcode);
        }
        if (GUILayout.Button("Join Random", GlobalStyles.Buttonss))
        {
            RoomMods.JoinRandom();
        }
        if (GUILayout.Button("Leave", GlobalStyles.Buttonss))
        {
            RoomMods.Leave();
        }
        GUILayout.EndHorizontal();
    }

    private void Settings()
    {
        StyleChangerOpne = GUILayout.Toggle(StyleChangerOpne, "Style Settings");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Style", GlobalStyles.Buttonss))
        {
            FileManager.SaveGUISettings();
        }

        if (GUILayout.Button("Load Style", GlobalStyles.Buttonss))
        {
            FileManager.LoadGUISettings();
        }
        GUILayout.EndHorizontal();
    }

    private void SoundBoard()
    {
        GlobalVars.GSoundsOpen = GUILayout.Toggle(GlobalVars.GSoundsOpen, "GSoundboard");
    }

    // style changer (help me god)
    private void Style(int id)
    {
        GlobalStyles.StyleColors Colors = GlobalStyles.PullColors();
        Color Window = YesHelper("Window", Colors.Window);
        Color Buttons = YesHelper("Buttons", Colors.Buttons);
        Color Track = YesHelper("Slider Track", Colors.SliderTrack);
        Color Thunb = YesHelper("Slider Thumb", Colors.SliderThumb);
        GlobalStyles.SetColors(Window, Buttons, Track, Thunb);
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", GlobalStyles.Buttonss))
        {
            StyleChangerOpne = !StyleChangerOpne;
        }
        GUI.DragWindow();
    }
    // lil helper 
    private Color YesHelper(string Label, Color y)
    {
        GUILayout.Label(Label);
        float r = GUILayout.HorizontalSlider(y.r, 0f, 1f, GlobalStyles.SliderStyle, GlobalStyles.SliderThumbStyle);
        float g = GUILayout.HorizontalSlider(y.g, 0f, 1f, GlobalStyles.SliderStyle, GlobalStyles.SliderThumbStyle);
        float b = GUILayout.HorizontalSlider(y.b, 0f, 1f, GlobalStyles.SliderStyle, GlobalStyles.SliderThumbStyle);
        return new Color(r, g, b, 1f);
    }
}