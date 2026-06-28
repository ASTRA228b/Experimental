using System;
using Experimental.Mods.Settings;

namespace Experimental.Core.Preset;

[Serializable]
public class PresetData
{
    // APullMod
    public float SpeedValue;
    public float Normalmuilty;
    public GlobalVars.HandPullMode CurrentMode;

    // A PSA Mod
    public float Speedd;
    public float MexGroundDis;

    // APreds
    public float PredSrength;
    public float movementThreshold;
    public float smoothness;
    public float maxArmLength;

    // Velmax (how does no one know what this is lol)
    public float VelMulti;
    public float VelMax;

    // WallWalk (more like a walk assist)
    public float WallwalkSpeed;

    // Gorilla Time V2 (tuff mod) trust 
    public GlobalVars.TimeSettingss TimeSetting;

    // ATurnMod
    public float TurnSpeed;
    public float Smoothnes;
    public float SnapAngle;
    public float TV;

    // APitGeo
    public GlobalVars.SlipWallPitOptions SlipOption;
    public GlobalVars.PitGroundOptions GroundOption;
    public float WallSlipMult;
    public float WallSlipMultOther;
    public float GroundMult;
    public float GroundMultOther;

    // CamUtils
    public float MainSmooth;
    public float RotSmooth;
    public float KalmanR;
    public float MainFOV;
    public float PCFOV;
    public bool MainEnabled;
    public bool LivEnabled;
    public bool MainFOVEnabled;
    public bool PCFOVEnabled;
    public bool ThirdPersonEnabled;

    // last state (windows)
    public bool ACamOpen;
    public bool AModMenuOpen;
    public bool AParticleOpen;
    public bool APitGeoOpen;
    public bool APredsOpen;
    public bool ATurnModOpen;
    public bool GTimeOpen;
    public bool PSAModOpen;
    public bool PullModOpen;
    public bool VelMaxOpen;
    public bool WallWalkOpen;
}