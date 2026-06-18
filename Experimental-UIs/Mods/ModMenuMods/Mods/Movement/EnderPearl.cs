using Experimental.Core.Libraries;
using Experimental.Core.Other;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.Rendering;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class EnderPearl : ExpMod
{
    private GameObject? Pearl;
    private Rigidbody? PearlRb;
    private Renderer? PearlRenderer;
    private Material? PMat;

    private bool RightHandThrow;

    public EnderPearl() : base("EnderPearl", Cat.Movement) { }

    public override void Update()
    {
        bool leftGrab = InputLib.LeftGrab;
        bool rightGrab = InputLib.RightGrab;

        if (leftGrab || rightGrab)
        {
            if (Pearl == null)
            {
                CreatePearl();
            }

            if (PearlRb != null)
            {
                PearlRb.Obliterate();
                PearlRb = null;
            }

            RightHandThrow = rightGrab;

            Pearl!.transform.position =
                rightGrab
                    ? GorillaTagger.Instance.rightHandTransform.position
                    : GorillaTagger.Instance.leftHandTransform.position;
        }
        else if (Pearl != null)
        {
            if (PearlRb == null)
            {
                PearlRb = Pearl.AddComponent<Rigidbody>();

                PearlRb.linearVelocity =
                    RightHandThrow
                        ? GTPlayer.Instance.RightHand.velocityTracker.GetAverageVelocity(true, 0)
                        : GTPlayer.Instance.LeftHand.velocityTracker.GetAverageVelocity(true, 0);
            }

            Physics.Raycast(
                Pearl.transform.position,
                PearlRb.linearVelocity.normalized,
                out RaycastHit hit,
                0.25f,
                GTPlayer.Instance.locomotionEnabledLayers
            );

            if (hit.collider != null)
            {
                Extensions.TeleportPlayer(Pearl.transform.position);

                GameObject.Destroy(Pearl);

                Pearl = null;
                PearlRb = null;
                PearlRenderer = null;
            }
        }
    }

    public override void FixedUpdate()
    {
        if (PearlRb == null)
            return;

        PearlRb.AddForce(
            Vector3.up * 6.66f,
            ForceMode.Acceleration
        );
    }

    public override void Disable()
    {
        if (Pearl != null)
        {
            GameObject.Destroy(Pearl);
        }

        Pearl = null;
        PearlRb = null;
        PearlRenderer = null;

        base.Disable();
    }

    private void CreatePearl()
    {
        Pearl = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        Pearl.GetComponent<Collider>().Obliterate();

        Pearl.transform.localScale = Vector3.one * 0.1f;

        PearlRenderer = Pearl.GetComponent<Renderer>();

        if (PMat == null)
        {
            PMat = new Material(
                Shader.Find("Universal Render Pipeline/Unlit")
            )
            {
                color = Color.black
            };

            PMat.SetFloat("_Surface", 1);
            PMat.SetFloat("_Blend", 0);
            PMat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
            PMat.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
            PMat.SetFloat("_ZWrite", 0);

            PMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");

            PMat.renderQueue = (int)RenderQueue.Transparent;
        }

        PearlRenderer.material = PMat;
    }
}