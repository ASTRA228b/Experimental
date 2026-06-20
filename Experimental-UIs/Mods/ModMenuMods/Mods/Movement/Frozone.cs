using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Frozone : ExpMod
{
    private float LastSpawnTime;
    public Frozone() : base("Frozone", Cat.Movement) { }

    public override void Update()
    {
        if (Time.time - LastSpawnTime < 0.05f)
            return;
        LastSpawnTime = Time.time;

        if (InputLib.LeftGrab)
        {
            CreateForzonPlatfrom(
                GTPlayer.Instance.LeftHand.controllerTransform.position +
                GTPlayer.Instance.LeftHand.controllerTransform.right * 0.05f,

                GTPlayer.Instance.LeftHand.controllerTransform.rotation
            );
        }

        if (InputLib.RightGrab)
        {
            CreateForzonPlatfrom(
                GTPlayer.Instance.RightHand.controllerTransform.position -
                GTPlayer.Instance.RightHand.controllerTransform.right * 0.05f,

                GTPlayer.Instance.RightHand.controllerTransform.rotation
            );
        }
    }

    private void CreateForzonPlatfrom(Vector3 pos, Quaternion rot)
    {
        GameObject Plat = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Plat.transform.position = pos;
        Plat.transform.rotation = rot;
        Plat.transform.localScale = new Vector3(0.015f, 0.28f, 0.28f);
        Renderer ren = Plat.GetComponent<Renderer>();
        ren.material.shader = Shader.Find("GorillaTag/UberShader");
        ren.transform.localScale = new Vector3(0.015f, 0.28f, 0.28f);
        GorillaSurfaceOverride sur = Plat.AddComponent<GorillaSurfaceOverride>();
        sur.overrideIndex = 61;
        GameObject.Destroy(Plat, 1f);
    }
}