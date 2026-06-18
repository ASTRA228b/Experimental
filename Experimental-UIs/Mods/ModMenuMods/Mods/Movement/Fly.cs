using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Fly : ExpMod
{
    private Vector3 LastFlyDirection;
    private bool WasFlying;

    private static int FlySpeed => 35;

    public Fly() : base("Fly", Cat.Movement) { }

    public override void FixedUpdate()
    {
        bool IsFlying = InputLib.LeftControllerXButton;

        Rigidbody RB = GorillaTagger.Instance.rigidbody;

        Vector3 Forward =
            GTPlayer.Instance.headCollider.transform.forward;

        if (IsFlying)
        {
            LastFlyDirection = Forward.normalized;

            GTPlayer.Instance.transform.position +=
                LastFlyDirection *
                FlySpeed *
                Time.fixedDeltaTime;

            RB.linearVelocity = Vector3.zero;

            WasFlying = true;
        }
        else if (WasFlying)
        {
            RB.linearVelocity =
                LastFlyDirection *
                FlySpeed;

            WasFlying = false;
        }
    }

    public override void Disable()
    {
        WasFlying = false;
        LastFlyDirection = Vector3.zero;

        base.Disable();
    }
}