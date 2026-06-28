using Experimental.Mods.Settings;

namespace Experimental.Core.Preset;

public static class PresetHelpers
{
    public static PresetData Pull()
    {
        return new()
        {
            SpeedValue = GlobalVars.SpeedValue,
            Normalmuilty = GlobalVars.Normalmuilty,
            CurrentMode = GlobalVars.CurrentMode,
            Speedd = GlobalVars.Speedd,
            MexGroundDis = GlobalVars.MexGroundDis,
            PredSrength = GlobalVars.PredSrength,
            movementThreshold = GlobalVars.movementThreshold,
            smoothness = GlobalVars.smoothness,
            maxArmLength = GlobalVars.maxArmLength,
            VelMulti = GlobalVars.VelMulti,
            VelMax = GlobalVars.VelMax,
            WallwalkSpeed = GlobalVars.WallwalkSpeed,
            TimeSetting = GlobalVars.timeSettings,
            TurnSpeed = GlobalVars.TurnSpeed,
            Smoothnes = GlobalVars.Smoothnes,
            SnapAngle = GlobalVars.SnapAngle,
            TV = GlobalVars.TV,
            SlipOption = GlobalVars.SlipOptions,
            GroundOption = GlobalVars.GroundOptions,
            WallSlipMult = GlobalVars.WallSlipMult,
            WallSlipMultOther = GlobalVars.WallSlipMultOther,
            GroundMult = GlobalVars.GroundMult,
            GroundMultOther = GlobalVars.GroundMultOther,
            MainSmooth = GlobalVars.MainSmooth,
            RotSmooth = GlobalVars.RotSmooth,
            KalmanR = GlobalVars.KalmanR,
            MainFOV = GlobalVars.MainFOV,
            PCFOV = GlobalVars.PCFOV,
            MainEnabled = GlobalVars.MainEnabled,
            LivEnabled = GlobalVars.LivEnabled,
            MainFOVEnabled = GlobalVars.MainFOVEnabled,
            PCFOVEnabled = GlobalVars.PCFOVEnabled,
            ThirdPersonEnabled = GlobalVars.ThirdPersonEnabeld,
            ACamOpen = GlobalVars.CamUIOpen,
            AModMenuOpen = GlobalVars.ModMUIOpen,
            AParticleOpen = GlobalVars.PartUIOpen,
            APitGeoOpen = GlobalVars.PitGeoGUIOpen,
            APredsOpen = GlobalVars.IsOpen,
            ATurnModOpen = GlobalVars.ATurnWindowOpen,
            GTimeOpen = GlobalVars.GTVOpen,
            PSAModOpen = GlobalVars.PSAOpen,
            PullModOpen = GlobalVars.Open,
            VelMaxOpen = GlobalVars.VOpen,
            WallWalkOpen = GlobalVars.WalkOpen
        };
    }

    public static void Apply(PresetData data)
    {
        GlobalVars.SpeedValue = data.SpeedValue;
        GlobalVars.Normalmuilty = data.Normalmuilty;
        GlobalVars.CurrentMode = data.CurrentMode;
        GlobalVars.Speedd = data.Speedd;
        GlobalVars.MexGroundDis = data.MexGroundDis;
        GlobalVars.PredSrength = data.PredSrength;
        GlobalVars.movementThreshold = data.movementThreshold;
        GlobalVars.smoothness = data.smoothness;
        GlobalVars.maxArmLength = data.maxArmLength;
        GlobalVars.VelMulti = data.VelMulti;
        GlobalVars.VelMax = data.VelMax;
        GlobalVars.WallwalkSpeed = data.WallwalkSpeed;
        GlobalVars.timeSettings = data.TimeSetting;
        GlobalVars.TurnSpeed = data.TurnSpeed;
        GlobalVars.Smoothnes = data.Smoothnes;
        GlobalVars.SnapAngle = data.SnapAngle;
        GlobalVars.TV = data.TV;
        GlobalVars.SlipOptions = data.SlipOption;
        GlobalVars.GroundOptions = data.GroundOption;
        GlobalVars.WallSlipMult = data.WallSlipMult;
        GlobalVars.WallSlipMultOther = data.WallSlipMultOther;
        GlobalVars.GroundMult = data.GroundMult;
        GlobalVars.GroundMultOther = data.GroundMultOther;
        GlobalVars.MainSmooth = data.MainSmooth;
        GlobalVars.RotSmooth = data.RotSmooth;
        GlobalVars.KalmanR = data.KalmanR;
        GlobalVars.MainFOV = data.MainFOV;
        GlobalVars.PCFOV = data.PCFOV;
        GlobalVars.MainEnabled = data.MainEnabled;
        GlobalVars.LivEnabled = data.LivEnabled;
        GlobalVars.MainFOVEnabled = data.MainFOVEnabled;
        GlobalVars.PCFOVEnabled = data.PCFOVEnabled;
        GlobalVars.ThirdPersonEnabeld = data.ThirdPersonEnabled;
        GlobalVars.CamUIOpen = data.ACamOpen;
        GlobalVars.ModMUIOpen = data.AModMenuOpen;
        GlobalVars.PartUIOpen = data.AParticleOpen;
        GlobalVars.PitGeoGUIOpen = data.APitGeoOpen;
        GlobalVars.IsOpen = data.APredsOpen;
        GlobalVars.ATurnWindowOpen = data.ATurnModOpen;
        GlobalVars.GTVOpen = data.GTimeOpen;
        GlobalVars.PSAOpen = data.PSAModOpen;
        GlobalVars.Open = data.PullModOpen;
        GlobalVars.VOpen = data.VelMaxOpen;
        GlobalVars.WalkOpen = data.WallWalkOpen;
    }
}
