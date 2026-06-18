using UnityEngine;
using Experimental.Mods.ModMenuMods.Managed;
using Experimental.Mods.ModMenuMods.Mods.Movement;

namespace Experimental.Mods.Controllers;

public class ModRegSystem : MonoBehaviour
{
    public void Start()
    {
        // Movement
        ModsManager.Register("Platforms", new Platforms());
        ModsManager.Register("Car", new Car());
        ModsManager.Register("Dash", new Dash());
        ModsManager.Register("EnderPearl", new EnderPearl());
        ModsManager.Register("Fly", new Fly());
        ModsManager.Register("Frozone", new Frozone());
        ModsManager.Register("IronMan", new IronMan());
        ModsManager.Register("NoClip", new NoClip());
        ModsManager.Register("SpeedBoost", new SpeedBoost());

        // Fun

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