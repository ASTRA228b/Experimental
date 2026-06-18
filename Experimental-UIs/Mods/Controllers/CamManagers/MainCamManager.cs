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
    private Camera? mainCam;
    private Camera? pcOutputCam;
    private CinemachineCamera? pcCam;


    private void Start()
    {
        cam = Camera.main;
        smoothing = new SmoothingManager();
        mainCam = Camera.main;

        GameObject shoulderCamObj = GameObject.Find("Shoulder Camera");

        if (shoulderCamObj != null)
        {
            pcOutputCam = shoulderCamObj.GetComponent<Camera>();
            pcCam = shoulderCamObj.GetComponentInChildren<CinemachineCamera>();
        }
    }

    private void SetFOV()
    {
        if (mainCam != null && MainFOVEnabled)
        {
            mainCam.fieldOfView = MainFOV;
        }
        if (pcOutputCam != null && PCFOVEnabled)
        {
            pcOutputCam.fieldOfView = PCFOV;
        }
    }

    private void HandleThirdPerson()
    {
        if (pcOutputCam == null) return;

        pcOutputCam.enabled = ThirdPersonEnabeld;
    }

    private void LateUpdate()
    {
        if (livTarget == null || cam == null) return;
        if (smoothing == null) return;
        float dt = Time.deltaTime;
        livTarget.position = smoothing.ApplyKalman(livTarget.position, dt);
        smoothing.ApplyMain(cam, livTarget);

        SetFOV();
        HandleThirdPerson();
    }
}