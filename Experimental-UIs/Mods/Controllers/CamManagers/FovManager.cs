using Unity.Cinemachine;
using UnityEngine;
using static Experimental.Mods.Settings.GlobalVars;

namespace Experimental.Mods.Controllers.CamManagers;

public class FovManager : MonoBehaviour
{
    private Camera? mainCam;
    private Camera? pcOutputCam;
    private CinemachineCamera? pcCam;
    private GameObject? shoulderCamObj;

    private void Start()
    {
        mainCam = Camera.main;

        shoulderCamObj = GameObject.Find("Player Objects/Third Person Camera");

        if (shoulderCamObj != null)
        {
            pcOutputCam = shoulderCamObj.GetComponent<Camera>();
            pcCam = shoulderCamObj.GetComponentInChildren<CinemachineCamera>();
        }
        Debug.Log($"[EXP]: Found Cam ->  {shoulderCamObj}");
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
        if (shoulderCamObj == null)
            return;

        shoulderCamObj.SetActive(ThirdPersonEnabeld);
    }


    private void LateUpdate()
    {
        SetFOV();
        HandleThirdPerson();
    }
}