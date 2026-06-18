using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class NoClip : ExpMod
{
    private bool LastTriggerState;

    public NoClip() : base("NoClip", Cat.Movement) { }

    public override void Update()
    {
        bool TriggerHeld = InputLib.RightTrigger;

        if (TriggerHeld && !LastTriggerState)
        {
            foreach (MeshCollider Collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                Collider.enabled = false;
            }
        }

        if (!TriggerHeld && LastTriggerState)
        {
            foreach (MeshCollider Collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                Collider.enabled = true;
            }
        }

        LastTriggerState = TriggerHeld;
    }

    public override void Disable()
    {
        foreach (MeshCollider Collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
        {
            Collider.enabled = true;
        }

        LastTriggerState = false;

        base.Disable();
    }
}