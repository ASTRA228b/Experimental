using Experimental.Mods.Settings;
using Experimental.Core.Other;
using Experimental.Mods.PullMods.AMPH;
using Experimental.Mods.PullMods.PMV2;

namespace Experimental.Core.Preset;

public static class PresetHelpers
{
    public static PresetData Pull()
    {
        return new()
        {
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
            PMV2Open = GlobalVars.PMV2Open,
            AMPHOpen = GlobalVars.AMPHOpen,
            PSAModOpen = GlobalVars.PSAOpen,
            VelMaxOpen = GlobalVars.VOpen,
            WallWalkOpen = GlobalVars.WalkOpen,

            StartupSoundEnabled = GlobalVars.StartupSoundEnabled,

            PMV2Enabled = PMV2System.Enabled,
            PMV2PullPower = PMV2System.PullPower,
            PMV2UpHillPower = PMV2System.UpHillPower,
            PMV2Momentum = PMV2System.Momentum,
            PMV2MaxPull = PMV2System.MaxPull,
            PMV2ClampVelocity = PMV2System.ClampVelocity,
            PMV2MaxVelocity = PMV2System.MaxVelocity,
            PMV2Mode = (int)PMV2System.Mode,
            PMV2Hand = (int)PMV2System.Hand,
            PMV2Activation = (int)PMV2System.Activation,
            PMV2Input = InputSelectors.PMV2SelectedIndex,

            PMV2SpeedEnabled = PMV2SpeedSystem.Enabled,
            PMV2SpeedOnlyWithPull = PMV2SpeedSystem.OnlyWithPull,
            PMV2Speed = PMV2SpeedSystem.Speed,
            PMV2Multiplier = PMV2SpeedSystem.Multiplier,

            AMPHEnabled = AMPHSystem.Enabled,
            AMPHMethod = (int)AMPHMethods.Method,
            AMPHPullPower = AMPHMethods.PullPower,
            AMPHUpHillPower = AMPHMethods.UpHillPower,
            AMPHMomentum = AMPHMethods.Momentum,
            AMPHMaxPull = AMPHMethods.MaxPull,
            AMPHMaxVelocity = AMPHMethods.MaxVelocity,
            AMPHExtraPower = AMPHMethods.ExtraPower,
            AMPHClampVelocity = AMPHMethods.ClampVelocity,

            AMPHInput = AMPHInput.SelectedIndex,
            AMPHHand = (int)AMPHSystem.Hand,
            AMPHActivation = (int)AMPHSystem.Activation,
            AMPHReleaseWindow = AMPHSystem.ReleaseWindow,
            AMPHTouchLinger = AMPHSystem.TouchLinger,
        };
    }

    public static void Apply(PresetData data)
    {
        PMV2System.Enabled = data.PMV2Enabled;
        PMV2System.PullPower = data.PMV2PullPower;
        PMV2System.UpHillPower = data.PMV2UpHillPower;
        PMV2System.Momentum = data.PMV2Momentum;
        PMV2System.MaxPull = data.PMV2MaxPull;
        PMV2System.ClampVelocity = data.PMV2ClampVelocity;
        PMV2System.MaxVelocity = data.PMV2MaxVelocity;
        PMV2System.Mode = (PMV2System.PullMode)data.PMV2Mode;
        PMV2System.Hand = (PMV2System.HandMode)data.PMV2Hand;
        PMV2System.Activation = (PMV2System.ActivationMode)data.PMV2Activation;
        InputSelectors.PMV2SelectedIndex = data.PMV2Input;

        PMV2SpeedSystem.Enabled = data.PMV2SpeedEnabled;
        PMV2SpeedSystem.OnlyWithPull = data.PMV2SpeedOnlyWithPull;
        PMV2SpeedSystem.Speed = data.PMV2Speed;
        PMV2SpeedSystem.Multiplier = data.PMV2Multiplier;

        AMPHSystem.Enabled = data.AMPHEnabled;
        AMPHMethods.Method = (AMPHMethods.PullMethod)data.AMPHMethod;
        AMPHMethods.PullPower = data.AMPHPullPower;
        AMPHMethods.UpHillPower = data.AMPHUpHillPower;
        AMPHMethods.Momentum = data.AMPHMomentum;
        AMPHMethods.MaxPull = data.AMPHMaxPull;
        AMPHMethods.MaxVelocity = data.AMPHMaxVelocity;
        AMPHMethods.ExtraPower = data.AMPHExtraPower;
        AMPHMethods.ClampVelocity = data.AMPHClampVelocity;

        AMPHInput.SelectedIndex = data.AMPHInput;
        AMPHSystem.Hand = (AMPHSystem.HandMode)data.AMPHHand;
        AMPHSystem.Activation = (AMPHSystem.ActivationMode)data.AMPHActivation;
        AMPHSystem.ReleaseWindow = data.AMPHReleaseWindow;
        AMPHSystem.TouchLinger = data.AMPHTouchLinger;
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
        GlobalVars.PMV2Open = data.PMV2Open;
        GlobalVars.AMPHOpen = data.AMPHOpen;
        GlobalVars.VOpen = data.VelMaxOpen;
        GlobalVars.WalkOpen = data.WallWalkOpen;

        GlobalVars.StartupSoundEnabled = data.StartupSoundEnabled;
        if (PMV2System.Enabled && AMPHSystem.Enabled)
            AMPHSystem.Enabled = false;
    }
}
