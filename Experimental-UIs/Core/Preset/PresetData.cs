using System;
using Experimental.Mods.Settings;

namespace Experimental.Core.Preset;

[Serializable]
public class PresetData
{
    // Astra's PullMod V2
    public bool PMV2Enabled;
    public float PMV2PullPower;
    public float PMV2UpHillPower;
    public float PMV2Momentum;
    public float PMV2MaxPull;
    public bool PMV2ClampVelocity;
    public float PMV2MaxVelocity;
    public int PMV2Mode;
    public int PMV2Hand;
    public int PMV2Activation;
    public int PMV2Input;

    public bool PMV2SpeedEnabled;
    public bool PMV2SpeedOnlyWithPull;
    public float PMV2Speed;
    public float PMV2Multiplier;

    // Astra's PullMod Hub
    public bool AMPHEnabled;
    public int AMPHMethod;
    public float AMPHPullPower;
    public float AMPHUpHillPower;
    public float AMPHMomentum;
    public float AMPHMaxPull;
    public float AMPHMaxVelocity;
    public float AMPHExtraPower;
    public bool AMPHClampVelocity;

    public int AMPHInput;
    public int AMPHHand;
    public int AMPHActivation;
    public float AMPHReleaseWindow;
    public float AMPHTouchLinger;

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
    public bool PMV2Open;
    public bool AMPHOpen;
    public bool VelMaxOpen;
    public bool WallWalkOpen;

    // Startup settings
    public bool StartupSoundEnabled = true;
}