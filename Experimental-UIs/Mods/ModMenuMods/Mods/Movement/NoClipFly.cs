using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class NoClipFly : ExpMod
{
    private Vector3 lastFlyDirection;
    private bool wasFlying;

    private bool lastXState;

    private static int FlySpeed => 25;

    public NoClipFly() : base("No Clip Fly", Cat.Movement)
    {
    }

    public override void FixedUpdate()
    {
        bool isFlying = InputLib.LeftControllerXButton;

        Rigidbody rb = GorillaTagger.Instance.rigidbody;
        Vector3 forward = GTPlayer.Instance.headCollider.transform.forward;

        if (isFlying)
        {
            lastFlyDirection = forward.normalized;

            GTPlayer.Instance.transform.position +=
                lastFlyDirection * FlySpeed * Time.deltaTime;

            rb.linearVelocity = Vector3.zero;

            wasFlying = true;
        }
        else if (wasFlying)
        {
            rb.linearVelocity = lastFlyDirection * FlySpeed;
            wasFlying = false;
        }

        bool pressed = isFlying && !lastXState;
        bool released = !isFlying && lastXState;

        if (pressed)
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                collider.enabled = false;
        }

        if (released)
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                collider.enabled = true;
        }

        lastXState = isFlying;
    }

    public override void Disable()
    {
        foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            collider.enabled = true;

        base.Disable();
    }
}