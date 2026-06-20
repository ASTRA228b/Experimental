using UnityEngine;
using GorillaLocomotion;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Fun;

public class PunhcMod : ExpMod
{
    public Vector3[] lastRight = new Vector3[20];
    public Vector3[] lastLeft = new Vector3[20];
    public PunhcMod() : base("PunchMod", Cat.Fun) { }

    public override void Update()
    {
        Punching(); // maybe buggy had to change alot of code from the og
    }

    public void Punching()
    {
        int yes = -1;
        foreach (VRRig rig in VRRigCache.ActiveRigs)
        {
            if (rig != GorillaTagger.Instance.offlineVRRig)
            {
                yes++;
                Vector3 pos = rig.leftHandTransform.position;
                Vector3 pos2 = GorillaTagger.Instance.offlineVRRig.head.rigTarget.position;
                float H = Vector3.Distance(pos, pos2);
                if (H < 0.25f)
                {
                    var comp = GTPlayer.Instance.GetComponent<Rigidbody>();
                    comp.angularVelocity += Vector3.Normalize(pos * 10f);
                }
                lastLeft[yes] = pos;
                pos = rig.rightHandTransform.position;
                float  no = Vector3.Distance(pos, pos2);
                if (no < 0.25f)
                {
                    var comp = GTPlayer.Instance.GetComponent<Rigidbody>();
                    comp.angularVelocity += Vector3.Normalize(pos * 10f);
                }
                lastRight[yes] = pos;
            }
        }
    }

}