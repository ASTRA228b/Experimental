using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;
using UnityEngine.InputSystem;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Platforms : ExpMod
{
    private GameObject? leftPlatform;
    private GameObject? rightPlatform;
    private bool leftSpawned;
    private bool rightSpawned;

    public Platforms() : base("Platforms", Cat.Movement) { }
    public override void Enable()
    {
        base.Enable();

        leftPlatform = null;
        rightPlatform = null;

        leftSpawned = false;
        rightSpawned = false;
    }

    public override void Disable()
    {
        if (leftPlatform != null)
            GameObject.Destroy(leftPlatform);

        if (rightPlatform != null)
            GameObject.Destroy(rightPlatform);

        leftPlatform = null;
        rightPlatform = null;

        leftSpawned = false;
        rightSpawned = false;

        base.Disable();
    }

    public override void Update()
    {
        bool leftGrip = InputLib.LeftGrab;
        bool rightGrip = InputLib.RightGrab;

        if (leftGrip && leftPlatform == null)
            leftPlatform = CreateLeftPlatform();

        if (rightGrip && rightPlatform == null)
            rightPlatform = CreateRightPlatform();

        if (leftGrip && leftPlatform != null && !leftSpawned)
        {
            leftPlatform.transform.position =
                GTPlayer.Instance.LeftHand.controllerTransform.position;

            leftPlatform.transform.rotation =
                GTPlayer.Instance.LeftHand.controllerTransform.rotation;

            leftSpawned = true;
        }

        if (rightGrip && rightPlatform != null && !rightSpawned)
        {
            rightPlatform.transform.position =
                GTPlayer.Instance.RightHand.controllerTransform.position;

            rightPlatform.transform.rotation =
                GTPlayer.Instance.RightHand.controllerTransform.rotation;

            rightSpawned = true;
        }

        if (!leftGrip && leftPlatform != null)
        {
            GameObject.Destroy(leftPlatform);
            leftPlatform = null;
            leftSpawned = false;
        }

        if (!rightGrip && rightPlatform != null)
        {
            GameObject.Destroy(rightPlatform);
            rightPlatform = null;
            rightSpawned = false;
        }
    }


    private GameObject CreateLeftPlatform()
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(platform.GetComponent<Rigidbody>());
        GameObject.Destroy(platform.GetComponent<Renderer>());
        platform.transform.localScale = new Vector3(0.25f, 0.3f, 0.25f);
        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(visual.GetComponent<Rigidbody>());
        visual.transform.SetParent(platform.transform);
        visual.transform.rotation = Quaternion.identity;
        visual.transform.localScale = new Vector3(0.1f, 1f, 1f);
        Renderer renderer = visual.GetComponent<Renderer>();
        renderer.material.shader = Shader.Find("GorillaTag/UberShader");
        renderer.material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
        visual.transform.localPosition = new Vector3(0.02f, 0f, 0f);
        return platform;
    }

    private GameObject CreateRightPlatform()
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(platform.GetComponent<Rigidbody>());
        GameObject.Destroy(platform.GetComponent<Renderer>());
        platform.transform.localScale = new Vector3(0.25f, 0.3f, 0.25f);
        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(visual.GetComponent<Rigidbody>());
        visual.transform.SetParent(platform.transform);
        visual.transform.rotation = Quaternion.identity;
        visual.transform.localScale = new Vector3(0.1f, 1f, 1f);
        Renderer renderer = visual.GetComponent<Renderer>();
        renderer.material.shader = Shader.Find("GorillaTag/UberShader");
        renderer.material.color = GorillaTagger.Instance.offlineVRRig.playerColor;
        visual.transform.localPosition = new Vector3(-0.02f, 0f, 0f);
        return platform;
    }
}