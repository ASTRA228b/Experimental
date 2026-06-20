using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;
using static Experimental.Mods.ModMenuMods.Mods.Movement.GravitySettings;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class ZeroGav : ExpMod
{
    public ZeroGav() : base("ZeroGrav", Cat.Movement) { }

    public override void FixedUpdate()
    {
        ZeroGravity();
    }
}