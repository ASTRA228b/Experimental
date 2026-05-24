using UnityEngine;
using BepInEx;
using Experimental.Core;
using Experimental.Stuff.Pathces;
using Experimental.Stuff;
using Experimental.Core.Libraries;

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
        GameObject Plugin = new GameObject(Constantss.ObjectName);
        Plugin.AddComponent<Main>();
        // Plugin.AddComponent<OnScreenNotify>(); // maybe adding back never used 
        // Plugin.AddComponent<JoinManager>(); // stopped loading it was buggy
        DontDestroyOnLoad(Plugin);
    }
}