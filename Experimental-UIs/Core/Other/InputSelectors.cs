using UnityEngine;
using Experimental.Core.Libraries;

namespace Experimental.Core.Other;

public static class InputSelectors
{
    // PullMod
    public static string[] PInputNames = {
        "Right Grab",
        "Left Grab",
        "Right Trigger",
        "Left Trigger",
        "RightJoyStick (Hold)",
        "LeftJoyStick (Hold)",
        "A Button",
        "B Button",
        "X Button",
        "Y Button"
    };

    public static Func<bool>[] PInputs =
    {
        () => InputLib.RightGrab,
        () => InputLib.LeftGrab,
        () => InputLib.RightTrigger,
        () => InputLib.LeftTrigger,
        () => InputLib.RightJoystickClick,
        () => InputLib.LeftJoystickClick,
        () => InputLib.RightControllerAButton,
        () => InputLib.RightControllerBButton,
        () => InputLib.LeftControllerXButton,
        () => InputLib.LeftControllerYButton
    };

    public static int PSelectedIndex = 0;
    public static bool PPressed => PInputs[PSelectedIndex]?.Invoke() ?? false;

    // VelMax
    public static string[] VInputNames = {
        "Right Grab",
        "Left Grab",
        "Right Trigger",
        "Left Trigger",
        "RightJoyStick (Hold)",
        "LeftJoyStick (Hold)",
        "A Button",
        "B Button",
        "X Button",
        "Y Button"
    };

    public static Func<bool>[] VInputs =
    {
        () => InputLib.RightGrab,
        () => InputLib.LeftGrab,
        () => InputLib.RightTrigger,
        () => InputLib.LeftTrigger,
        () => InputLib.RightJoystickClick,
        () => InputLib.LeftJoystickClick,
        () => InputLib.RightControllerAButton,
        () => InputLib.RightControllerBButton,
        () => InputLib.LeftControllerXButton,
        () => InputLib.LeftControllerYButton
    };

    public static int VSelectedIndex = 0;
    public static bool VPressed => VInputs[VSelectedIndex]?.Invoke() ?? false;

    // PSA
    public static string[] InputNames =
    {
        "Right Joystick",
        "Left Joystick"
    };

    public static Func<Vector2>[] AxisInputs =
    {
        () => InputLib.RightJoyStickAxis,
        () => InputLib.LeftJoyStickAxis
    };

    public static int SelectedIndex = 0;

    public static Vector2 Axis => AxisInputs[SelectedIndex]?.Invoke() ?? Vector2.zero;

    // Wallwalk
    public static string[] UseWalkInputNames =
    {
        "LT & RT (Hold)",
        "LG & RG (Hold)",
        "LJ & RJ (Hold)",
        "LB & RB (Hold)"
    };

    public static Func<bool>[] UseWalkInputs =
    {
      () => InputLib.LeftTrigger && InputLib.RightTrigger,
      () => InputLib.LeftGrab && InputLib.RightGrab,
      () => InputLib.LeftJoystickClick && InputLib.RightJoystickClick,
      () => (InputLib.LeftControllerXButton || InputLib.LeftControllerYButton) && (InputLib.RightControllerBButton || InputLib.RightControllerAButton)
    };

    public static int UseWalkIndex = 0;

    public static bool UseWalkPressed => UseWalkInputs[UseWalkIndex]?.Invoke() ?? false;

    // ATurnMod
    public static string[] ATLInputNames =
   {
        "X",
        "Y",
        "Left Grab",
        "Left Trigger"
    };

    public static Func<bool>[] ATLInputs =
    {
        () => InputLib.LeftControllerXButton,
        () => InputLib.LeftControllerYButton,
        () => InputLib.LeftGrab,
        () => InputLib.LeftTrigger
    };

    public static int ATLSelectedIndex = 0;

    public static bool ATLPressed => ATLInputs[ATLSelectedIndex]?.Invoke() ?? false;

    public static string[] ATRInputNames =
    {
        "A",
        "B",
        "Right Grab",
        "Right Trigger"
    };

    public static Func<bool>[] ATRInputs =
    {
        () => InputLib.RightControllerAButton,
        () => InputLib.RightControllerBButton,
        () => InputLib.RightGrab,
        () => InputLib.RightTrigger
    };

    public static int ATRSelectedIndex = 0;

    public static bool ATRPressed => ATRInputs[ATRSelectedIndex]?.Invoke() ?? true;
}