using UnityEngine;
using GorillaLocomotion;
using Experimental.Mods.Particle.Managers;
using Experimental.Mods.Particle.Mods;

namespace Experimental.Mods.Controllers
{
    public class ParticleInitManager : MonoBehaviour
    {
        private bool initialized;

        private void Update()
        {
            if (!initialized)
            {
                if (GTPlayer.Instance == null)
                    return;
                Transform left = GTPlayer.Instance.LeftHand.controllerTransform;
                Transform right = GTPlayer.Instance.RightHand.controllerTransform;
                if (left == null || right == null)
                    return;
                InitializeParticles();
                initialized = true;
            }
            ParticleManager.Update();
        }

        private void InitializeParticles()
        {
            // fire fists
            Transform left = GTPlayer.Instance.LeftHand.controllerTransform;
            Transform right = GTPlayer.Instance.RightHand.controllerTransform;
            ParticleManager.Register("LeftFist", new FireFists("LeftFist", left));
            ParticleManager.Register("RightFist", new FireFists("RightFist", right));
            // lightning
            ParticleManager.Register("Lightning", new Lightning("Lightning", new Vector3(-63.2589f, 9.4352f, -65.2775f), 0.05f));
            // storm
            ParticleManager.Register("NebulaStorm", new NebulaStorm("NebulaStorm", new Vector3(-63.2589f, 9.4352f, -65.2775f)));
            // black and white holes
            ParticleManager.Register("BlackHole", new BlackHoleEffect("BlackHole", new Vector3(-63.2589f, 9.4352f, -65.2775f)));
            ParticleManager.Register("WhiteHole", new WhiteHoleEffect("WhiteHole", new Vector3(-63.2589f, 9.4352f, -65.2775f)));
            // Domain
            ParticleManager.Register("Domain", new RandomDomianEffect("Domain", new Vector3(-63.2589f, 9.4352f, -65.2775f), 0.15f));
            // cast magic 
            ParticleManager.Register("Fireball", new Fireball("Fireball", right.position, right.forward, 20f));
            ParticleManager.Register("Draw", new Draw("Draw"));
        }

        private void OnDestroy()
        {
            ParticleManager.Cleanup();
        }
    }
}