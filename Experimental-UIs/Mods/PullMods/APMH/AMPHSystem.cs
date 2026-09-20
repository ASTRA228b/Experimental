using UnityEngine;
using GorillaLocomotion;

namespace Experimental.Mods.PullMods.AMPH;

public static class AMPHSystem
{
    public enum HandMode
    {
        Both,
        Left,
        Right
    }

    public enum ActivationMode
    {
        Release,
        Touch,
        Hold
    }

    public static bool Enabled;

    public static HandMode Hand = HandMode.Both;
    public static ActivationMode Activation = ActivationMode.Release;

    public static float ReleaseWindow = 0.10f;
    public static float TouchLinger = 0.06f;

    private static bool LastLeftTouch, LastRightTouch;
    private static float ReleaseTime, TouchTime;

    public static void Update()
    {
        if (!Enabled || GTPlayer.Instance == null || GorillaTagger.Instance == null)
            return;

        bool leftTouch = GTPlayer.Instance.IsHandTouching(true);
        bool rightTouch = GTPlayer.Instance.IsHandTouching(false);

        bool leftRelease = !leftTouch && LastLeftTouch;
        bool rightRelease = !rightTouch && LastRightTouch;

        if (CheckHand(leftRelease, rightRelease))
            ReleaseTime = ReleaseWindow;

        if (CheckHand(leftTouch, rightTouch))
            TouchTime = TouchLinger;

        ReleaseTime = Mathf.Max(0f, ReleaseTime - Time.fixedDeltaTime);
        TouchTime = Mathf.Max(0f, TouchTime - Time.fixedDeltaTime);

        bool activate = Activation switch
        {
            ActivationMode.Release => ReleaseTime > 0f,
            ActivationMode.Touch => CheckHand(leftTouch, rightTouch) || TouchTime > 0f,
            ActivationMode.Hold => true,
            _ => false
        };

        LastLeftTouch = leftTouch;
        LastRightTouch = rightTouch;

        if (!AMPHInput.Pressed || !activate)
            return;

        AMPHMethods.Run();
    }

    private static bool CheckHand(bool left, bool right)
    {
        return Hand switch
        {
            HandMode.Left => left,
            HandMode.Right => right,
            HandMode.Both => left || right,
            _ => false
        };
    }

    public static void Reset()
    {
        Enabled = false;
        Hand = HandMode.Both;
        Activation = ActivationMode.Release;
        ReleaseWindow = 0.10f;
        TouchLinger = 0.06f;

        LastLeftTouch = false;
        LastRightTouch = false;
        ReleaseTime = 0f;
        TouchTime = 0f;
    }
}