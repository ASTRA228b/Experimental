using UnityEngine;
using GorillaLocomotion;
using Experimental.Mods.Particle.Managers;
using Experimental.Mods.Particle.Mods;

namespace Experimental.Mods.Controllers;

public class ParticleInitManager : MonoBehaviour
{
    private void Start()
    {
        // fire Fists
        ParticleManager.Register("LeftFist", new FireFists("LeftFist", GTPlayer.Instance.LeftHand.controllerTransform));
        ParticleManager.Register("RightFist", new FireFists("RightFist", GTPlayer.Instance.RightHand.controllerTransform));
        // lightning
        ParticleManager.Register("Lightning", new Lightning("Lightning", new Vector3(-63.2589f, 9.4352f, -65.2775f), 0.05f));
    }

    private void Update()
    {
        ParticleManager.Update();
    }

    private void OnDestroy()
    {
        ParticleManager.Cleanup();
    }
}