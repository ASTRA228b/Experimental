using UnityEngine;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class NebulaStorm : Effect
{
    private readonly Vector3 Position;
    public NebulaStorm(string name, Vector3 position) : base(name)
    {
        Position = position;
    }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.transform.position = Position;
        System = Root.AddComponent<ParticleSystem>();
        var main = System.main;
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.2f, 0.3f, 0.6f, 0.3f), new Color(0.6f, 0.1f, 0.7f, 0.4f));
        main.startSize = 3f;
        main.startLifetime = 4f;
        main.startSpeed = 0f;
        main.loop = true;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 100;
        var emission = System.emission;
        emission.rateOverTime = 5f;
        var shape = System.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 2f;
        var renderer = System.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        GameObject starsObj = new GameObject("Stars");
        starsObj.transform.SetParent(Root.transform, false);
        ParticleSystem stars = starsObj.AddComponent<ParticleSystem>();
        var starsMain = stars.main;
        starsMain.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color(1f, 1f, 0.8f));
        starsMain.startSize = 0.05f;
        starsMain.startLifetime = 2f;
        starsMain.startSpeed = 0f;
        starsMain.loop = true;
        starsMain.simulationSpace = ParticleSystemSimulationSpace.World;
        starsMain.maxParticles = 50;
        var starsEmission = stars.emission;
        starsEmission.rateOverTime = 10f;
        var starsShape = stars.shape;
        starsShape.shapeType = ParticleSystemShapeType.Sphere;
        starsShape.radius = 2.5f;
        var starsRenderer = stars.GetComponent<ParticleSystemRenderer>();
        starsRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        Root.SetActive(false);
    }
}