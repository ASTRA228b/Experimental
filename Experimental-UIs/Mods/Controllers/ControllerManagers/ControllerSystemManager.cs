using UnityEngine;
using Experimental.Stuff;
using Experimental.Mods.Controllers;

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
        DontDestroyOnLoad(ModSystemManagers);
    }
}