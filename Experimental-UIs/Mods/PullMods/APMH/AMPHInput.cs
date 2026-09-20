using UnityEngine;
using Experimental.Core.Libraries;

namespace Experimental.Mods.PullMods.AMPH;

public static class AMPHInput
{
    public enum InputType { Grip, Trigger, Joystick, A, B, X, Y }

    public static InputType Input = InputType.Grip;
    public static readonly string[] InputNames = { "Grip", "Trigger", "Joystick", "A", "B", "X", "Y" };

    public static int SelectedIndex
    {
        get => (int)Input;
        set => Input = (InputType)Mathf.Clamp(value, 0, InputNames.Length - 1);
    }

    public static bool LeftPressed => Input switch
    {
        InputType.Grip => InputLib.LeftGrab,
        InputType.Trigger => InputLib.LeftTrigger,
        InputType.Joystick => InputLib.LeftJoystickClick,
        InputType.X => InputLib.LeftControllerXButton,
        InputType.Y => InputLib.LeftControllerYButton,
        _ => false
    };

    public static bool RightPressed => Input switch
    {
        InputType.Grip => InputLib.RightGrab,
        InputType.Trigger => InputLib.RightTrigger,
        InputType.Joystick => InputLib.RightJoystickClick,
        InputType.A => InputLib.RightControllerAButton,
        InputType.B => InputLib.RightControllerBButton,
        _ => false
    };

    public static bool Pressed => AMPHSystem.Hand switch
    {
        AMPHSystem.HandMode.Left => LeftPressed,
        AMPHSystem.HandMode.Right => RightPressed,
        AMPHSystem.HandMode.Both => LeftPressed || RightPressed,
        _ => false
    };

    public static void Reset()
    {
        Input = InputType.Grip;
    }
}