using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class IronMan : ExpMod
{
    public IronMan() : base("IronMan", Cat.Movement) { }

    public override void FixedUpdate()
    {
        Rigidbody rb = GorillaTagger.Instance.rigidbody;

        float flySpeed = 25f * 1.5f;

        if (InputLib.LeftGrab)
        {
            Vector3 leftForce =
                -GorillaTagger.Instance.leftHandTransform.right *
                flySpeed;

            rb.AddForce(
                leftForce * Time.fixedDeltaTime,
                ForceMode.VelocityChange
            );
        }

        if (InputLib.RightGrab)
        {
            Vector3 rightForce =
                GorillaTagger.Instance.rightHandTransform.right *
                flySpeed;

            rb.AddForce(
                rightForce * Time.fixedDeltaTime,
                ForceMode.VelocityChange
            );
        }
    }
}