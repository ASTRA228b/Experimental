using GorillaLocomotion;
using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Dash : ExpMod
{
    public Dash() : base("Dash", Cat.Movement) { }

    public override void FixedUpdate()
    {
        if (InputLib.LeftControllerXButton)
        {
            GorillaTagger.Instance.rigidbody.linearVelocity +=
                    GTPlayer.Instance.headCollider.transform.forward * 25f;
        }
    }
}