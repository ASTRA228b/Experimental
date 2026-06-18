using GorillaLocomotion;
using UnityEngine;

namespace Experimental.Core.Other;

public static class Extensions
{
    public static Vector3 FormatTeleportPosition(Vector3 teleportPosition) =>
           teleportPosition - GorillaTagger.Instance.bodyCollider.transform.position +
           GorillaTagger.Instance.transform.position;

    public static void Obliterate(this Component comp)
    {
        if (comp != null) UnityEngine.Object.Destroy(comp);
    }

    public static void TeleportPlayer(Vector3 destinationPosition)
    {
        GTPlayer.Instance.TeleportTo(FormatTeleportPosition(destinationPosition), GTPlayer.Instance.transform.rotation);
        VRRig.LocalRig.transform.position = destinationPosition;
    }
}