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
    private Material? PMat;
    private bool Yes;

    public EnderPearl() : base("EnderPearl", Cat.Movement) { }

    public override void Update()
    {
        bool LeftGrab = InputLib.LeftGrab;
        bool RightGrab = InputLib.RightGrab;

        if (LeftGrab || RightGrab)
        {
            if (Pearl == null)
            {
                Pearl = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pearl.GetComponent<Collider>().Obliterate();
                Pearl.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                if (PMat == null)
                {
                    PMat = new Material(Shader.Find("Universal Render Pipeline/Unlit"))
                    {
                        color = Color.black,
                    };
                    PMat.SetFloat("_Surface", 1);
                    PMat.SetFloat("_Blend", 0);
                    PMat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
                    PMat.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
                    PMat.SetFloat("_ZWrite", 0);
                    PMat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                    PMat.renderQueue = (int)RenderQueue.Transparent;
                }
                Pearl.GetComponent<Renderer>().material = PMat;
            }
            if (Pearl.GetComponent<Rigidbody>() != null)
                Pearl.GetComponent<Rigidbody>().Obliterate();

            Yes = RightGrab;
            Pearl.transform.position = RightGrab ? GorillaTagger.Instance.rightHandTransform.position : GorillaTagger.Instance.leftHandTransform.position;
        }
        else
        {
            if (Pearl != null)
            {
                if (Pearl.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody? comp = Pearl.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    comp!.linearVelocity = Yes ? GTPlayer.Instance.RightHand.velocityTracker.GetAverageVelocity(true, 0) : GTPlayer.Instance.LeftHand.velocityTracker.GetAverageVelocity(true, 0);
                }
                Physics.Raycast(Pearl.transform.position, Pearl.GetComponent<Rigidbody>().linearVelocity, out RaycastHit Ray, 0.25f, GTPlayer.Instance.locomotionEnabledLayers);
                if (Ray.collider != null)
                {
                    Extensions.TeleportPlayer(Pearl.transform.position);
                    Pearl.Destroy();
                }
            }
        }
        if (Pearl == null)
            return;

        Pearl.GetComponent<Rigidbody>()?.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
    }

    public override void Disable()
    {
        if (Pearl != null)
            Pearl.Destroy();
    }
}