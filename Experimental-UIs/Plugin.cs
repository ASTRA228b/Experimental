using UnityEngine;
using BepInEx;
using Experimental.Core;
using Experimental.Stuff;
using Experimental.Mods.Controllers.ControllerManagers;
using Experimental.Core.IntroManager;
using Experimental.Core.MainManagers;

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
        SoundManager.Init();
        GameObject Plugin = new(Constantss.ObjectName);
        Plugin.AddComponent<Main>();
        Plugin.AddComponent<ControllerSystemManager>();
        Plugin.AddComponent<IntroPlayer>(); // fuck ts bro im lowk pissed ive spent 4 hours debugging ts its not worth it at all   
        DontDestroyOnLoad(Plugin);
    }
}