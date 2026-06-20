using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class UpAndDown : ExpMod
{
    public UpAndDown() : base("UpAndDown", Cat.Movement) { }

    public override void FixedUpdate()
    {
        if (InputLib.LeftTrigger)
        {
            GTPlayer.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 50f, 0f), ForceMode.Acceleration);
        }
        if (InputLib.RightTrigger)
        {
            GTPlayer.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -50f, 0f), ForceMode.Acceleration);
        }
    }
}