using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Managed;

public abstract class ExpMod
{
    public string Name { get; }
    public Cat Category { get; }
    public bool Enabled { get; private set; }

    protected ExpMod(string name, Cat c)
    {
        Name = name;
        Category = c;
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