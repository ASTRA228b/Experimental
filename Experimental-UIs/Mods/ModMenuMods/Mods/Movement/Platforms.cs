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

    public Platforms() : base("Platforms", Cat.Movement) { }
    public override void Enable()
    {
        base.Enable();
        leftPlatform = CreateLeftPlatform();
        rightPlatform = CreateRightPlatform();
    }

    public override void Disable()
    {
        if (leftPlatform != null)
            GameObject.Destroy(leftPlatform);

        if (rightPlatform != null)
            GameObject.Destroy(rightPlatform);

        leftPlatform = null;
        rightPlatform = null;
        base.Disable();
    }

    public override void Update()
    {
        if (leftPlatform == null || rightPlatform == null)
            return;
        bool leftGripHeld = InputLib.LeftGrab || Keyboard.current.lKey.isPressed;
        if (leftGripHeld)
        {
            leftPlatform.SetActive(true);
            leftPlatform.transform.position = GTPlayer.Instance.LeftHand.controllerTransform.position;
            leftPlatform.transform.rotation = GTPlayer.Instance.LeftHand.controllerTransform.rotation;
        }
        else
        {
            leftPlatform.SetActive(false);
        }

        bool rightGripHeld = InputLib.RightGrab || Keyboard.current.kKey.isPressed;

        if (rightGripHeld)
        {
            rightPlatform.SetActive(true);
            rightPlatform.transform.position = GTPlayer.Instance.RightHand.controllerTransform.position;
            rightPlatform.transform.rotation = GTPlayer.Instance.RightHand.controllerTransform.rotation;
        }
        else
        {
            rightPlatform.SetActive(false);
        }
    }


    private GameObject CreateLeftPlatform()
    {
        GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(platform.GetComponent<Rigidbody>());
        GameObject.Destroy(platform.GetComponent<BoxCollider>());
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
        GameObject.Destroy(platform.GetComponent<BoxCollider>());
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