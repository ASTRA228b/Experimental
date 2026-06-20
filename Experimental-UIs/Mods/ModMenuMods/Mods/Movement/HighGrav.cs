using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;
using static Experimental.Mods.ModMenuMods.Mods.Movement.GravitySettings;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class HighGrav : ExpMod
{
    public HighGrav() : base("HighGrav", Cat.Movement) { }

    public override void FixedUpdate()
    {
        HighGravity();
    }
}