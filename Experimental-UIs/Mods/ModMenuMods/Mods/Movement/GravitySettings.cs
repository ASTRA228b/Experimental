using UnityEngine;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public static class GravitySettings
{
    public static void ZeroGravity()
    {
        Physics.gravity = Vector3.zero;
    }

    public static void LowGravity()
    {
        GorillaTagger.Instance.rigidbody.AddForce(Vector3.up * 6.66f, ForceMode.Acceleration);
    }

    public static void HighGravity()
    {
        GorillaTagger.Instance.rigidbody.AddForce(Vector3.down * 7.77f, ForceMode.Acceleration);
    }
}