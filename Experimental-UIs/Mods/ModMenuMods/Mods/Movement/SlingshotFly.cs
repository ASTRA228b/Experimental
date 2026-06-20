using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class SlingshotFly : ExpMod
{
    public SlingshotFly() : base("SlingshotFly", Cat.Movement) { }

    public override void FixedUpdate()
    {
        if (InputLib.RightControllerAButton)
        {
            GorillaTagger.Instance.rigidbody.linearVelocity += GTPlayer.Instance.headCollider.transform.forward * (Time.deltaTime * (25 * 2));
        } // fuck it we nest 
    }
}