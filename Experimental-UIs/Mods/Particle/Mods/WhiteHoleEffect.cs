using UnityEngine;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class WhiteHoleEffect : Effect
{
    private readonly Vector3 position;
    public WhiteHoleEffect(string name, Vector3 position) : base(name)
    {
        this.position = position;
    }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.transform.position = position;
        System = Root.AddComponent<ParticleSystem>();
        var main = System.main;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color(0.9f, 0.9f, 0.9f));
        main.startSize = 0.4f;
        main.startSpeed = 0.5f;
        main.startLifetime = 2f;
        main.loop = true;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 150;
        var emission = System.emission;
        emission.rateOverTime = 30f;
        var shape = System.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 2.5f;
        shape.randomDirectionAmount = 0.1f;
        var rotation = System.rotationOverLifetime;
        rotation.enabled = true;
        rotation.z = new ParticleSystem.MinMaxCurve(0.5f, 1f);
        var velocity = System.velocityOverLifetime;
        velocity.enabled = true;
        velocity.x = 0f;
        velocity.y = 0f;
        velocity.z = new ParticleSystem.MinMaxCurve(1f, 2f);
        var renderer = System.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        Root.SetActive(false);
    }

    public override void Update()
    {
        if (Root == null || !Root.activeSelf)
            return;

        Collider[] colliders = Physics.OverlapSphere(Root.transform.position, 10f);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb == null)
                continue;

            Vector3 direction = rb.position - Root.transform.position;
            float distance = direction.magnitude;
            if (distance < 0.1f)
                continue;

            float force = Mathf.Clamp(1000f / distance, 0f, 10f);
            rb.AddForce(direction.normalized * force * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}