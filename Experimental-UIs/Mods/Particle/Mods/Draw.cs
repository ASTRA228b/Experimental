using UnityEngine;
using UnityEngine.InputSystem;
using Experimental.Core.Libraries;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class Draw : Effect
{
    private GameObject? DrawEffect;

    public Draw(string name) : base(name) { }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.SetActive(false);
    }

    public override void Update()
    {
        bool pressed = InputLib.RightControllerAButton || Keyboard.current.tKey.isPressed;
        if (pressed && DrawEffect == null)
        {
            DrawEffect = new GameObject("DrawEffect");
            ParticleSystem ps = DrawEffect.AddComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.blue);
            main.startSize = 0.05f;
            main.startSpeed = 0.1f;
            main.startLifetime = 2f;
            main.loop = true;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            var emission = ps.emission;
            emission.rateOverTime = 100f;
            var shape = ps.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 0.005f;
            var velocity = ps.velocityOverLifetime;
            velocity.enabled = true;
            velocity.z = new ParticleSystem.MinMaxCurve(12f);
            ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
            renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            ps.Play();
        }
        if (pressed && DrawEffect != null)
        {
            DrawEffect.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            DrawEffect.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
        }
        if (!pressed && DrawEffect != null)
        {
            GameObject.Destroy(DrawEffect);
            DrawEffect = null;
        }
    }

}