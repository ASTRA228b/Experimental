using UnityEngine;
using Experimental.Mods.ModMenuMods.Managed;
using Experimental.Mods.ModMenuMods.Mods.Movement;

namespace Experimental.Mods.Controllers;

public class ModRegSystem : MonoBehaviour
{
    public void Start()
    {
        ModsManager.Register("Platforms", new Platforms());
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