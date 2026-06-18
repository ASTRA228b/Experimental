namespace Experimental.Mods.ModMenuMods.Managed;

public static class ModsManager
{
    private static readonly Dictionary<string, ExpMod> Mods = new();

    public static void Register(string name, ExpMod mod)
    {
        Mods[name] = mod;
    }

    public static void Toggle(string name)
    {
        if (!Mods.TryGetValue(name, out var mod))
            return;

        if (mod.Enabled)
            mod.Disable();
        else
            mod.Enable();
    }

    public static void Update()
    {
        foreach (var mod in Mods.Values)
        {
            if (mod.Enabled)
                mod.Update();
        }
    }

    public static void FixedUpdate()
    {
        foreach (var mod in Mods.Values)
        {
            if (mod.Enabled)
                mod.FixedUpdate();
        }
    }
}