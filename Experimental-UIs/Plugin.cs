using UnityEngine;
using BepInEx;
using Experimental.Core;
using Experimental.Stuff;
using Experimental.Mods.Controllers.ControllerManagers;

namespace Experimental.Plugin;

[BepInPlugin(Constantss.GUID, Constantss.Name, Constantss.Version)]
public class Plugin : BaseUnityPlugin
{
    private void Start()
    {
        PatchLoader.Apply();
    }

    private void Awake()
    {
        GameObject Plugin = new(Constantss.ObjectName);
        Plugin.AddComponent<Main>();
        Plugin.AddComponent<ControllorSystemManager>();
        DontDestroyOnLoad(Plugin);
    }
}