using UnityEngine;
using Experimental.Mods.ModMenuMods.Managed;
using Experimental.Mods.ModMenuMods.Mods.Fun;
using Experimental.Mods.ModMenuMods.Mods.Movement;

namespace Experimental.Mods.Controllers;

public class ModRegSystem : MonoBehaviour
{
    public void Awake()
    {
        // Movement
        ModsManager.Register("SpeedBoost", new SpeedBoost());
        ModsManager.Register("Platforms", new Platforms());
        ModsManager.Register("Frozone", new Frozone());
        ModsManager.Register("Fly", new Fly());
        ModsManager.Register("SlingshotFly", new SlingshotFly());
        ModsManager.Register("Car", new Car());
        ModsManager.Register("Dash", new Dash());
        ModsManager.Register("EnderPearl", new EnderPearl());
        ModsManager.Register("IronMan", new IronMan());
        ModsManager.Register("NoClip", new NoClip());
        ModsManager.Register("UpAndDown", new UpAndDown());
        ModsManager.Register("ZeroGrav", new ZeroGav());
        ModsManager.Register("LowGrav", new LowGrav());
        ModsManager.Register("HighGrav", new HighGrav());
        ModsManager.Register("Fan", new Fan()); // buggy 
        // Fun
        ModsManager.Register("PunchMod", new PunhcMod()); // broken
        // Misc

        // OP

        // Player
    }

    public void Update()
    {
        ModsManager.Update();
    }

    public void FixedUpdate()
    {
        ModsManager.FixedUpdate();
    }
}