using Experimental.Mods.Controllers;
using Experimental.Mods.Controllers.CamManagers;
using Experimental.Stuff;
using UnityEngine;

namespace Experimental.Mods.Controllers.ControllerManagers;

public class ControllerSystemManager : MonoBehaviour
{
    private GameObject? ModSystemManagers;

    private void Start() => Debug.Log($"[{Constantss.ManagerObjectConst}]: Object's Loaded Fully (Might have a Delay)");

    private void Awake()
    {
        ModSystemManagers = new(Constantss.ManagerObjectConst);
        ModSystemManagers.AddComponent<ATurnModController>();
        ModSystemManagers.AddComponent<PitGeoManager>();
        ModSystemManagers.AddComponent<ParticleInitManager>(); // Might be buggy - Astra
        ModSystemManagers.AddComponent<ModRegSystem>();
        ModSystemManagers.AddComponent<MainCamManager>();
        ModSystemManagers.AddComponent<FovManager>();
        DontDestroyOnLoad(ModSystemManagers);
    }
}