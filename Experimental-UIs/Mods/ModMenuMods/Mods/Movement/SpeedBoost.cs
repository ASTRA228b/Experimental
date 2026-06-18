using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;
using GorillaLocomotion;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class SpeedBoost : ExpMod
{
    public SpeedBoost() : base("SpeedBoost", Cat.Movement) { }

    public override void FixedUpdate()
    {
        GTPlayer.Instance.maxJumpSpeed = 999f;
        GTPlayer.Instance.jumpMultiplier = 1f;
    }
}