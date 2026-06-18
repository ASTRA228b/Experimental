using Fusion;
using Unity.Cinemachine;
using UnityEngine;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.Controllers.CamManagers;

public class MainCamManager : MonoBehaviour
{
    public Transform? livTarget;
    private Camera? cam;
    private SmoothingManager? smoothing;


    private void Start()
    {
        cam = Camera.main;
        smoothing = new SmoothingManager();
    }

    private void LateUpdate()
    {
        if (livTarget == null || cam == null) return;
        if (smoothing == null) return;
        float dt = Time.deltaTime;
        livTarget.position = smoothing.ApplyKalman(livTarget.position, dt);
        smoothing.ApplyMain(cam, livTarget);
    }
}