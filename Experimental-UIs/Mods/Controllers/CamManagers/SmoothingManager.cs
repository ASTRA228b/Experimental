using UnityEngine;
using Liv.Lck.Smoothing;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.Controllers.CamManagers;

public class SmoothingManager
{
    private Vector3 velocity;
    private readonly KalmanFilter kx = new(0f, 1f);
    private readonly KalmanFilter ky = new(0f, 1f);
    private readonly KalmanFilter kz = new(0f, 1f);

    public Vector3 ApplyKalman(Vector3 raw, float dt)
    {
        if (!LivEnabled) return raw;

        return new Vector3(
            kx.Update(raw.x, dt, KalmanR),
            ky.Update(raw.y, dt, KalmanR),
            kz.Update(raw.z, dt, KalmanR)
        );
    }

    public void ApplyMain(Camera cam, Transform target)
    {
        if (!MainEnabled || cam == null || target == null) return;

        Transform t = cam.transform;

        t.position = Vector3.SmoothDamp(
            t.position,
            target.position,
            ref velocity,
            MainSmooth
        );

        t.rotation = Quaternion.Slerp(
            t.rotation,
            target.rotation,
            Time.deltaTime * RotSmooth
        );
    }
}