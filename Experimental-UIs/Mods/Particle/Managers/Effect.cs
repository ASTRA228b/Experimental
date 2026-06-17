using UnityEngine;

namespace Experimental.Mods.Particle.Managers;

public class Effect
{
    public string? Name;
    public bool Enabled;
    public GameObject? Root;
    public ParticleSystem? System;
    public Effect(string name)
    {
        Name = name;
    }
    public virtual void Create()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void Destroy()
    {
        if (Root != null)
            GameObject.Destroy(Root);
    }
    public void SetColor(Color color)
    {
        if (System == null)
            return;

        var main = System.main;
        main.startColor = color;
    }
}