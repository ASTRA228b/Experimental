using UnityEngine;
using GorillaLocomotion;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Fun;

public class PunhcMod : ExpMod
{
    private readonly Vector3[] lastRight = new Vector3[20];
    private readonly Vector3[] lastLeft = new Vector3[20];

    public PunhcMod() : base("PunchMod", Cat.Fun) { }

    public override void Update()
    {
        Punching();
    }

    private void Punching()
    {
        int index = -1;

        foreach (VRRig rig in VRRigCache.ActiveRigs)
        {
            if (rig == null || rig == GorillaTagger.Instance.offlineVRRig)
                continue;

            index++;

            if (index >= lastLeft.Length)
                break;

            Vector3 localPos = GorillaTagger.Instance.offlineVRRig.bodyTransform.position;

            Vector3 leftPos = rig.leftHandTransform.position;
            float leftDistance = Vector3.Distance(leftPos, localPos);

            if (leftDistance < 0.25f)
            {
                Vector3 velocity = leftPos - lastLeft[index];

                if (velocity.sqrMagnitude > 0.0001f)
                {
                    GorillaTagger.Instance.rigidbody.linearVelocity +=
                        velocity.normalized * 10f;
                }
            }

            lastLeft[index] = leftPos;

            Vector3 rightPos = rig.rightHandTransform.position;
            float rightDistance = Vector3.Distance(rightPos, localPos);

            if (rightDistance < 0.25f)
            {
                Vector3 velocity = rightPos - lastRight[index];

                if (velocity.sqrMagnitude > 0.0001f)
                {
                    GorillaTagger.Instance.rigidbody.linearVelocity +=
                        velocity.normalized * 10f;
                }
            }

            lastRight[index] = rightPos;
        }
    }
}