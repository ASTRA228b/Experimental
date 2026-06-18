using Experimental.Core.Libraries;
using Experimental.Mods.ModMenuMods.Managed;
using GorillaLocomotion;
using UnityEngine;
using static Experimental.Mods.GUIs.AModMenuUI;

namespace Experimental.Mods.ModMenuMods.Mods.Movement;

public class Car : ExpMod
{
    private static Vector3 velocity;

    private const float Speed = 10f;
    private const float MaxGroundDist = 2f;

    private const float Accel = 14f;
    private const float AirControl = 0.7f;
    private const float Drag = 0.98f;

    public Car() : base("Car", Cat.Movement) { }

    public override void FixedUpdate()
    {
        GTPlayer player = GTPlayer.Instance;

        if (player == null || player.bodyCollider == null)
            return;

        bool grounded = Physics.Raycast(
            player.transform.position,
            Vector3.down,
            out RaycastHit hit,
            10f,
            player.locomotionEnabledLayers)
            && (player.bodyCollider.bounds.min.y - hit.point.y) <= MaxGroundDist;

        Vector2 input = InputLib.LeftJoyStickAxis;

        if (input.sqrMagnitude < 0.0025f)
        {
            velocity = Vector3.Lerp(
                velocity,
                Vector3.zero,
                6f * Time.deltaTime);

            return;
        }

        Transform head = player.headCollider.transform;

        Vector3 forward = head.forward;
        forward.y = 0f;

        Vector3 right = head.right;
        right.y = 0f;

        Vector3 moveDir =
            (forward.normalized * input.y +
             right.normalized * input.x).normalized;

        float accel = grounded ? Accel : Accel * AirControl;

        velocity = Vector3.Lerp(
            velocity,
            moveDir * Speed,
            accel * Time.deltaTime);

        player.transform.position += velocity * Time.deltaTime;

        if (!grounded)
        {
            velocity *= Drag;
            return;
        }

        float bodyY = player.bodyCollider.bounds.min.y;

        if (bodyY < hit.point.y)
        {
            Vector3 pos = player.transform.position;

            pos.y += (hit.point.y - bodyY) * 12f * Time.deltaTime;

            player.transform.position = pos;
        }
    }
}