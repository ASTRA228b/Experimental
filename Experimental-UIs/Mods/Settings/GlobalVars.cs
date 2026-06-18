using UnityEngine;

namespace Experimental.Mods.Settings;

public static class GlobalVars
{
    // PullMod (UI)
    public static Rect Window = new(155, 155, 360, 460);
    public static bool Open = false;
    public static bool dropdownOpen = false;
    // other ui stuff
    public static string PName = "E - Astras PullMod V1";
    public static int WindowID = 17865439;
    // vars
    public static float pullPower = 0f;
    public static float UpHillPull = 0f;
    public static bool lasttouchleft;
    public static bool lasttouchright;
    public static float SpeedValue = 8.5f;
    public static float Normalmuilty = 1.5f;
    public static bool Speed = false;
    public enum HandPullMode
    {
        Left,
        Right,
        Both
    }
    public static HandPullMode CurrentMode = HandPullMode.Both;

    // Psa Mod (UI)
    public static Rect Wrect = new(155, 155, 360, 460);
    public static bool PSAOpen = false;
    public static bool PSADropdownOpen = false;
    public static string PSAMName = "E - Astras PSA Mod";
    public static int PSAWid = 6767589;
    // other
    public static float Speedd = 0f;
    public static float MexGroundDis = 0.5f;
    public static bool PSAEnabled = false;
    public static Vector3 velocity;

    // Preds (UI)
    public static Rect PredsWindow = new(155, 155, 360, 460);
    public static string APName = "E - APreds UI";
    public static int APWID = 698456;
    public static bool IsOpen = false;
    // other vars
    public static GameObject? LT;
    public static GameObject? RT;
    public static float PredSrength = 0f;
    public static float movementThreshold = 0.01f;
    public static float smoothness = 0.05f;
    public static float maxArmLength = 1.0f;
    public static bool IsPredOn = false;
    public static bool lastPredState = false;


    // Velmax (UI)
    public static Rect MRect = new(155, 155, 360, 460);
    public static bool VOpen = false;
    public static bool OpenDropdown = false;
    public static string VName = "E - Astras Velmax Mod";
    public static int VID = 99887765;
    // other
    public static bool Velmax = false;
    public static float VelMulti = 0f;
    public static float VelMax = 0f;

    // WallWalk (UI)
    public static Rect WalkWindow = new(155, 155, 360, 460);
    public static bool WalkOpen = false;
    public static bool WalkDOpen = false;
    public static int CTabb = 0;
    public static readonly string[] WTabs = { "Main", "Input Settings" };
    public static string WalkName = "E - Astras WallWalk V2";
    public static int WalkerID = 234543219;
    // other
    public static bool WallWalk = false;
    public static float WallwalkSpeed = 1f;

    // Gorilla time (UI)
    public enum TimeSettingss
    {
        None,
        Morning,
        TenAM,
        Day,
        Evning, // keeping funny spelling error
        Night
    }
    public static TimeSettingss timeSettings = TimeSettingss.None;
    public static Rect GTWindow = new Rect(110f, 110f, 220f, 220f);
    public static string GTIMEName = "E - Gorilla TimeV2";
    public static bool GTVOpen = false;
    public static int GTVID = 5678943;
    // ATurnMod

    // mod bs
    public static float TurnSpeed = 0f;
    public static float Smoothnes = 0f;
    public static float SnapAngle = 0f;
    public static float TV = 0f;
    public static bool ST = false;
    public static bool TM = false;
    // press bs
    public static bool LP = false;
    public static bool RP = false;
    // gui bs
    public static Rect ATurnModWindow = new(155, 155, 360, 460);
    public static bool ATurnWindowOpen = false;
    public static bool ATDropDownOpen = false;
    public static int ATurnModTabInt = 0;
    public static string[] ATurnModTabs = { "Main", "Input Settings" };
    public static string ATrunModGUIName = "E - Astras TurnMod";
    public static int ATrunModWindowInt = 67123456;

    // pitgeoooooooooooooooooooooooooooooooooooooooooooooooooooooo (im lowk havuing a strroke)

    // da mod vars

    public enum SlipWallPitOptions
    {
        None,
        UpperSlipWall,
        LowerSlipWall,
        BothSlipWalls,
    }

    public enum PitGroundOptions
    {
        None,
        PitGroundTop,
        PitGroundBottom
    }

    public static SlipWallPitOptions SlipOptions = SlipWallPitOptions.None;
    public static PitGroundOptions GroundOptions = PitGroundOptions.None;

    public static GameObject? PitWallUpper;
    public static GameObject? PitWallLower;
    public static GameObject? PitGroundTop;
    public static GameObject? PitGroundBottom;

    public static string ObjPath = "Environment Objects/LocalObjects_Prefab/Forest/Terrain/pitgeo/";

    public static float WallSlipMult = 1f;
    public static float WallSlipMultOther = 1f;

    public static float GroundMult = 1f;
    public static float GroundMultOther = 1f;

    public static bool PitModOnn = false;

    // ui vars 
    public static Rect PitGeoWindow = new(155, 155, 360, 460);
    public static bool PitGeoGUIOpen = false;
    public static bool PitGeoDropOpen = false;
    public static int PitGeoTabsIntager = 0;
    public static string[] PitGeoTabs = { "Main", "Input Settings" };
    public static int PitGeoWindowID = 2345763;
    public static string PitGeoWindowName = "E - Astras PitGeos";
    // GSoundBoard
    public static Rect GSoundBoardWindow = new(155, 155, 700, 500);
    public static bool GSoundsOpen = false;
    public static int GsoundsTabInt = 0;
    public static string[] GSoundsTabs = { "Main", "Settings" };
    public static string GSoundsWindowName = "E - GSoundBoard";
    public static int GSoundBoardsID = 98452341;
    public static Vector2 SoundBoardScroll;

    // Particle and other
    public static Rect PartUIRect = new(155, 155, 360, 460);
    public static bool PartUIOpen = false;
    public static string PartUIName = "E - AParticleSystems";
    public static int PartWId = 4827;
    // maybe 
    public static string[] PartUITabs = { "Main", "Settings" };
    public static int PartUITabInt = 0;
    // Mod Menu 
    // (all other todo with system is internal)
    public static Rect ModMUIRect = new(155, 155, 360, 460);
    public static bool ModMUIOpen = false;
    public static string ModMUIName = "E - AModMenu";
    public static int ModMWId = 4899712;
}