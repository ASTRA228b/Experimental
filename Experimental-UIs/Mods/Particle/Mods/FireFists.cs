using UnityEngine;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class FireFists : Effect
{
    private Transform Hand;

    public FireFists(string name, Transform hand) : base(name)
    {
        Hand = hand;
    }
    public override void Create()
    {
        Root = new GameObject(Name);
        Root.transform.SetParent(Hand, false);
        System = Root.AddComponent<ParticleSystem>();
        var main = System.main;
        main.startColor = Color.red;
        main.startSize = 0.05f;
        main.startLifetime = 1.5f;
        main.startSpeed = 0.25f;
        main.loop = true;
        var emission = System.emission;
        emission.rateOverTime = 15;
        Root.SetActive(false);
    }

}