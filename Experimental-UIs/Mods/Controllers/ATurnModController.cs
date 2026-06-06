using UnityEngine;
using GorillaLocomotion;
using Experimental.Core.Other;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.Controllers;

public class ATurnModController : MonoBehaviour
{
    private void FixedUpdate()
    {
        var TP = TurningPiv();
        if (TP != null)
        {
            if (TM)
            {
                if (ST)
                {
                    SnapTurn(TP, InputSelectors.ATLPressed, InputSelectors.ATRPressed);
                }
                else
                {
                    SmoothTurn(TP, InputSelectors.ATLPressed, InputSelectors.ATRPressed);
                }
            }
        }
    }

    private void SmoothTurn(Transform pivi, bool LP, bool RP)
    {
        float haii = 0f;
        if (LP)
            haii -= TurnSpeed;
        if (RP)
            haii += TurnSpeed;

        if (Smoothnes > 0.01f)
        {
            float no = Mathf.Lerp(50f, 2f, Smoothnes);
            TV = Mathf.Lerp(TV, haii, no * Time.deltaTime);
        }
        else
        {
            TV = haii;
        }
        if (Math.Abs(TV) > 0.01f)
        {
            float maybe = TV * Time.deltaTime;
            pivi.RotateAround(GetHeadPos(), Vector3.up, maybe);
        }
    }

    private void SnapTurn(Transform pivi, bool LPP, bool RPP)
    {
        
        if (LPP && !LP)
        {
            pivi.RotateAround(GetHeadPos(), Vector3.up, 0f - SnapAngle);
        }
        if (RPP && !RP)
        {
            pivi.RotateAround(GetHeadPos(), Vector3.up, SnapAngle);
        }
        RP = RPP;
        LP = LPP;
    }

    private Vector3 GetHeadPos()
    {
        if (Camera.main != null)
            return Camera.main.transform.position;

        return Vector3.zero;
    }

    private Transform TurningPiv()
    {
        if (GTPlayer.Instance != null)
            return GTPlayer.Instance.turnParent.transform;

        return Camera.main.transform.parent;
    }
}