using UnityEngine;
using UnityEngine.InputSystem;
using Experimental.Core.Libraries;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class Fireball : Effect
{
    private readonly Vector3 SpawnPosition;
    private readonly Vector3 Direction;
    private readonly float Speed;

    private bool IsFireballCast;

    public Fireball(string name, Vector3 spawnPosition, Vector3 direction, float speed = 20f) : base(name)
    {
        SpawnPosition = spawnPosition;
        Direction = direction;
        Speed = speed;
    }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.SetActive(false);
    }

    public override void Update()
    {
        if ((InputLib.RightControllerAButton || Keyboard.current.rKey.isPressed) && !IsFireballCast)
        {
            IsFireballCast = true;
            GameObject fireball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fireball.transform.position = SpawnPosition;
            fireball.transform.localScale = Vector3.one;
            Rigidbody rb = fireball.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(Direction * Speed, ForceMode.Impulse);
            ParticleSystem particles = fireball.AddComponent<ParticleSystem>();
            var main = particles.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0.3f, 0f), new Color(1f, 0.6f, 0f));
            main.startSize = 0.5f;
            main.startLifetime = 2f;
            main.startSpeed = 1f;
            var emission = particles.emission;
            emission.rateOverTime = 50f;
            GameObject.Destroy(fireball, 5f);
        }

        if (!InputLib.RightControllerAButton || !Keyboard.current.rKey.isPressed)
        {
            IsFireballCast = false;
        }
    }
}