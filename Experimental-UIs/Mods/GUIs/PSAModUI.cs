using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Other;
using Experimental.Core.GUIHelpers;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class PSAModUI
{
    public static void MakePSAModUI()
    {
        if (PSAOpen)
        {
            Wrect.height = PSADropdownOpen ? 520 : 460;
            Wrect = GUILayout.Window(PSAWid, Wrect, UIM, PSAMName, WindowStyle);
        }
    }

    public static void RunPSAMod()
    {
        if (PSAEnabled)
        {
            PSAMod(Speedd, MexGroundDis);
        }
    }

    public static void UIM(int pid)
    {
        PSAWindow();
        GUILayout.Space(10f);
        if (GUILayout.Button("Close", Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    public static void PSAWindow()
    {
        GUILayout.Label("Enable PSA");
        PSAEnabled = GUILayout.Toggle(PSAEnabled, "Enable PSA");
        GUILayout.Space(5f);
        Speedd = GUILayout.HorizontalSlider(Speedd, 1f, 12f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Speed {Speed:F1}");
        MexGroundDis = GUILayout.HorizontalSlider(MexGroundDis, 0.1f, 2f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"Ground Dist: {MexGroundDis:F2}");
        GUILayout.Space(5f);
        GUILayout.Label("Set Input:");

        int oldIndex = InputSelectors.SelectedIndex;

        InputSelectors.SelectedIndex = MenuHelper.Dropdown(
            "psa_input",
            InputSelectors.InputNames,
            InputSelectors.SelectedIndex,
            GUILayout.Width(200)
        );
        dropdownOpen = oldIndex != InputSelectors.SelectedIndex;
        GUILayout.Space(5f);
        GUILayout.Label("Presets:");

        if (GUILayout.Button("Legit", Buttonss))
        {
            Speedd = 4.5f;
            MexGroundDis = 0.6f;
        }

        if (GUILayout.Button("Comp", Buttonss))
        {
            Speedd = 6.5f;
            MexGroundDis = 0.8f;
        }

        if (GUILayout.Button("Speed Boost", Buttonss))
        {
            Speedd = 9.5f;
            MexGroundDis = 1.0f;
        }

        if (GUILayout.Button("Ice", Buttonss))
        {
            Speedd = 7.5f;
            MexGroundDis = 0.2f;
        }

        if (GUILayout.Button("Air Control", Buttonss))
        {
            Speedd = 8.5f;
            MexGroundDis = 0.4f;
        }

        if (GUILayout.Button("Random", Buttonss))
        {
            Speedd = UnityEngine.Random.Range(2f, 12f);
            MexGroundDis = UnityEngine.Random.Range(0.1f, 1.5f);
        }

        if (GUILayout.Button("Reset", Buttonss))
        {
            Speedd = 1f;
            MexGroundDis = 0.1f;
        }
    }

    public static void PSAMod(float speed, float maxGroundDist)
    {
        if (GTPlayer.Instance == null || GTPlayer.Instance.bodyCollider == null) return;
        RaycastHit hit;
        float groundY = 0f;
        bool grounded = false;
        if (Physics.Raycast(
            GTPlayer.Instance.transform.position,
            Vector3.down,
            out hit,
            10f,
            GTPlayer.Instance.locomotionEnabledLayers))
        {
            groundY = hit.point.y;
            float playerY = GTPlayer.Instance.bodyCollider.bounds.min.y;
            grounded = (playerY - groundY) <= maxGroundDist;
        }

        Vector2 input = InputSelectors.Axis;

        if (input.magnitude < 0.05f)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, 6f * Time.deltaTime);
            return;
        }
        Transform head = GTPlayer.Instance.headCollider.transform;
        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = head.right;
        right.y = 0f;
        right.Normalize();
        Vector3 targetDir = (forward * input.y + right * input.x).normalized;
        float accel = Mathf.Lerp(6f, 14f, speed / 10f);
        float airControl = Mathf.Lerp(0.3f, 0.7f, speed / 10f);
        float drag = Mathf.Lerp(0.90f, 0.98f, speed / 10f);
        float currentAccel = grounded ? accel : accel * airControl;
        velocity = Vector3.Lerp(velocity, targetDir * speed, currentAccel * Time.deltaTime);
        GTPlayer.Instance.transform.position += velocity * Time.deltaTime;
        if (!grounded)
        {
            velocity *= drag;
        }
        float currentY = GTPlayer.Instance.bodyCollider.bounds.min.y;
        if (grounded && currentY < groundY)
        {
            Vector3 pos = GTPlayer.Instance.transform.position;
            pos.y = Mathf.Lerp(pos.y, pos.y + (groundY - currentY), 12f * Time.deltaTime);
            GTPlayer.Instance.transform.position = pos;
        }
    }
}