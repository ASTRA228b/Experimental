using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using Experimental.Core.GUIHelpers;
using Experimental.Mods.GUIs;
using Experimental.Mods.Settings;
using Experimental.Mods.OtherUtils;

namespace Experimental.Core;

public class Main : MonoBehaviour
{
    // internal vars (global vars are just for the mods)
    private Rect Window = new(200, 110, 500, 150);
    private bool Open = false;
    private bool SLoaded = false;
    private int TabInt = 0;
    private readonly string[] Tabs = { "GUIs", "Utils" };
    // for room stuff
    private string roomcode = "";

    private void OnGUI()
    {
        if (!SLoaded)
        {
            GlobalStyles.INIT();
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
}