using UnityEngine;
using GorillaLocomotion;
using GorillaLocomotion.Climbing;
using Experimental.Core.GUIHelpers;
using Experimental.Core.Other;
using Experimental.Mods.Settings;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;
using static Experimental.Core.Other.Extensions;

namespace Experimental.Mods.GUIs;

public static class ApredsUI
{
    public static void MakePredsUI()
    {
        if (IsOpen)
        {
            PredsWindow = GUILayout.Window(APWID, PredsWindow, UIM, APName, WindowStyle);
        }
    }

    public static void RunMod()
    {
        if (IsPredOn != lastPredState)
        {
            if (IsPredOn) EnablePreds();
            else DisablePreds();

            lastPredState = IsPredOn;
        }

        if (IsPredOn)
            MakePreds();
    }

    public static void UIM(int id)
    {
        Mods();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            IsOpen = !IsOpen;
        }
        GUI.DragWindow();
    }

    public static void Mods()
    {
        GUILayout.Label("Enable Preds:");
        IsPredOn = GUILayout.Toggle(IsPredOn, "Enable Predictions");
        GUILayout.Space(5f);
        GUILayout.Label("Preds Strength");
        PredSrength = GUILayout.HorizontalSlider(PredSrength, 0.001f, 0.2f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Strength set to {PredSrength:F3}");
        GUILayout.Label("Movement Threshold");
        movementThreshold = GUILayout.HorizontalSlider(movementThreshold, 0.01f, 0.3f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Threshold set to {movementThreshold:F3}");
        GUILayout.Label("Smoothness");
        smoothness = GUILayout.HorizontalSlider(smoothness, 0.01f, 0.5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Smoothness set to {smoothness:F3}");
        GUILayout.Label("Max Arm Length");
        maxArmLength = GUILayout.HorizontalSlider(maxArmLength, 1.0f, 2.5f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"MAL set to {maxArmLength:F3}");
        GUILayout.Space(5f);
        GUILayout.Label("Presets:");
        if (GUILayout.Button("Max", Buttonss))
            PredSrength = 0.2f;
        if (GUILayout.Button("Random Setting", Buttonss))
            PredSrength = UnityEngine.Random.Range(0.001f, 0.2f);
        if (GUILayout.Button("Reset", Buttonss))
            PredSrength = 0.001f;
    }
    public static void EnablePreds()
    {
        if (LT != null || RT != null)
            return;

        LT = GameObject.CreatePrimitive(PrimitiveType.Cube);
        LT.GetComponent<BoxCollider>().Obliterate();
        LT.GetComponent<Rigidbody>().Obliterate();
        LT.GetComponent<Renderer>().enabled = false;
        LT.AddComponent<GorillaVelocityTracker>();

        RT = GameObject.CreatePrimitive(PrimitiveType.Cube);
        RT.GetComponent<BoxCollider>().Obliterate();
        RT.GetComponent<Rigidbody>().Obliterate();
        RT.GetComponent<Renderer>().enabled = false;
        RT.AddComponent<GorillaVelocityTracker>();
    }

    public static void DisablePreds()
    {
        if (LT != null)
        {
            GameObject.Destroy(LT);
            LT = null;
        }

        if (RT != null)
        {
            GameObject.Destroy(RT);
            RT = null;
        }
    }


    public static void MakePreds()
    {
        if (LT == null || RT == null)
            return;
        if (GTPlayer.Instance == null || GorillaTagger.Instance == null)
            return;
        if (GorillaTagger.Instance.leftHandTransform == null ||
            GorillaTagger.Instance.rightHandTransform == null ||
            GorillaTagger.Instance.headCollider == null)
            return;
        Transform leftHand = GorillaTagger.Instance.leftHandTransform;
        Transform rightHand = GorillaTagger.Instance.rightHandTransform;
        Transform head = GorillaTagger.Instance.headCollider.transform;
        LT.transform.position = leftHand.position;
        RT.transform.position = rightHand.position;
        GorillaVelocityTracker leftTracker = LT.GetComponent<GorillaVelocityTracker>();
        GorillaVelocityTracker rightTracker = RT.GetComponent<GorillaVelocityTracker>();
        if (leftTracker == null || rightTracker == null)
            return;
        Vector3 leftVel = leftTracker.GetAverageVelocity(true, 1f);
        Vector3 rightVel = rightTracker.GetAverageVelocity(true, 1f);
        leftVel = Vector3.ClampMagnitude(leftVel, 5f);
        rightVel = Vector3.ClampMagnitude(rightVel, 5f);
        bool leftMoving = leftVel.magnitude > movementThreshold;
        bool rightMoving = rightVel.magnitude > movementThreshold;
        if (leftMoving)
        {
            Vector3 target = leftHand.position + leftVel * PredSrength;
            leftHand.position = Vector3.Lerp(leftHand.position, target, smoothness);
        }
        if (rightMoving)
        {
            Vector3 target = rightHand.position + rightVel * PredSrength;
            rightHand.position = Vector3.Lerp(rightHand.position, target, smoothness);
        }
        leftHand.position = Vector3.ClampMagnitude(leftHand.position - head.position, maxArmLength) + head.position;
        rightHand.position = Vector3.ClampMagnitude(rightHand.position - head.position, maxArmLength) + head.position;
    }

}