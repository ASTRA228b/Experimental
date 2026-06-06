using UnityEngine;
using Experimental.Stuff;
using Experimental.Mods.Controllers;

namespace Experimental.Mods.Controllers.ControllerManagers;

public class ControllerSystemManager : MonoBehaviour
{
    private GameObject? ModSystemManagers;

    private void Start() => Debug.LogWarning($"[{Constantss.ManagerObjectConst}]: Object Loaded");

    private void Awake()
    {
        ModSystemManagers = new(Constantss.ManagerObjectConst);
        ModSystemManagers.AddComponent<ATurnModController>();
        DontDestroyOnLoad(ModSystemManagers);
    }
}