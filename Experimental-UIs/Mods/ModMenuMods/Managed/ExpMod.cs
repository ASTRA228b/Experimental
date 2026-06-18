using UnityEngine;

namespace Experimental.Mods.ModMenuMods.Managed;

public abstract class ExpMod
{
    public string Name { get; }
    public bool Enabled { get; private set; }

    protected ExpMod(string name)
    {
        Name = name;
    }

    public virtual void Enable()
    {
        Enabled = true;
    }

    public virtual void Disable()
    {
        Enabled = false;
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}