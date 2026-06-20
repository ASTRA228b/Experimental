using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;
using static Experimental.Mods.ModMenuMods.Mods.Movement.GravitySettings;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class LowGrav : ExpMod
{
    public LowGrav() : base("LowGrav", Cat.Movement) { }

    public override void FixedUpdate()
    {
        LowGravity();
    }
}