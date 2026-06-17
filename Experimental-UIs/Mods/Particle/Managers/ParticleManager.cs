using UnityEngine;
using System.Collections.Generic;

namespace Experimental.Mods.Particle.Managers;

public static class ParticleManager
{
    public static Color GlobalColor = Color.red;
    private static Dictionary<string, Effect> Effects = new();

    public static void Register(string id, Effect eff)
    {
        if (Effects.ContainsKey(id))
            return;

        eff.Create();
        Effects.Add(id, eff);
    }

    public static T? Get<T>(string id) where T : Effect
    {
        if (!Effects.ContainsKey(id))
            return null;

        return Effects[id] as T;
    }

    public static void Toggle(string id, bool state)
    {
        if (!Effects.ContainsKey(id))
            return;

        Effect e = Effects[id];
        e.Enabled = state;
        if (e.Root != null)
            e.Root.SetActive(state);
    }

    public static void SetColor(Color color)
    {
        GlobalColor = color;

        foreach (Effect effect in Effects.Values)
        {
            effect.SetColor(color);
        }
    }

    public static void Update()
    {
        foreach (Effect effect in Effects.Values)
        {
            if (effect.Enabled)
                effect.Update();
        }
    }

    public static void Cleanup()
    {
        foreach (Effect effect in Effects.Values)
        {
            effect.Destroy();
        }

        Effects.Clear();
    }
}