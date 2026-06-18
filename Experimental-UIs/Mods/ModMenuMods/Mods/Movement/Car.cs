using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Car : ExpMod
{
    public static Vector3 velocity;
    public Car() : base("Car", Cat.Movement) { }

    public override void FixedUpdate()
    {
        CarMod(10f, 2f);
    }

    public void CarMod(float speed, float maxGroundDist)
    {
        if (GTPlayer.Instance == null || GTPlayer.Instance.bodyCollider == null) return;
        RaycastHit hit;
        float groundY = 0f;
        bool grounded = false;
        if (Physics.Raycast(
            GTPlayer.Instance.transform.position,
            Vector3.down,
            out hit,
            10f,
            GTPlayer.Instance.locomotionEnabledLayers))
        {
            groundY = hit.point.y;
            float playerY = GTPlayer.Instance.bodyCollider.bounds.min.y;
            grounded = (playerY - groundY) <= maxGroundDist;
        }

        Vector2 input = InputLib.LeftJoyStickAxis;

        if (input.magnitude < 0.05f)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, 6f * Time.deltaTime);
            return;
        }
        Transform head = GTPlayer.Instance.headCollider.transform;
        Vector3 forward = head.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = head.right;
        right.y = 0f;
        right.Normalize();
        Vector3 targetDir = (forward * input.y + right * input.x).normalized;
        float accel = Mathf.Lerp(6f, 14f, speed / 10f);
        float airControl = Mathf.Lerp(0.3f, 0.7f, speed / 10f);
        float drag = Mathf.Lerp(0.90f, 0.98f, speed / 10f);
        float currentAccel = grounded ? accel : accel * airControl;
        velocity = Vector3.Lerp(velocity, targetDir * speed, currentAccel * Time.deltaTime);
        GTPlayer.Instance.transform.position += velocity * Time.deltaTime;
        if (!grounded)
        {
            velocity *= drag;
        }
        float currentY = GTPlayer.Instance.bodyCollider.bounds.min.y;
        if (grounded && currentY < groundY)
        {
            Vector3 pos = GTPlayer.Instance.transform.position;
            pos.y = Mathf.Lerp(pos.y, pos.y + (groundY - currentY), 12f * Time.deltaTime);
            GTPlayer.Instance.transform.position = pos;
        }
    }
}