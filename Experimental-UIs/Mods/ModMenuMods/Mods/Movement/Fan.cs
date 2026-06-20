using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Fan : ExpMod
{
    public Fan() : base("Fan", Cat.Movement) { }

    public override void FixedUpdate()
    {
        Fans();
    }

    public void Fans()
    {
        if (InputLib.LeftControllerXButton)
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
            GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            try
            {
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            }
            catch { }
            GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.up * Mathf.Cos(Time.time * 15f) * 2f + GorillaTagger.Instance.offlineVRRig.transform.right * Mathf.Sin(Time.time * 15f) * 2f;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position - GorillaTagger.Instance.offlineVRRig.transform.up * Mathf.Cos(Time.time * 15f) * 2f + GorillaTagger.Instance.offlineVRRig.transform.right * Mathf.Sin(Time.time * 15f) * 2f;
            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;
        }
        else
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }
    }
}