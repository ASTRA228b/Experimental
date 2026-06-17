using UnityEngine;
using Experimental.Mods.Particle.Managers;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class AParticleSystems
{
    // internal vars
    private static bool FistsEnabled = false;
    private static bool Lightning = false;

    public static void MakeParticleUI()
    {
        if (PartUIOpen)
        {
            PartUIRect = GUILayout.Window(PartWId, PartUIRect, UIM, PartUIName, WindowStyle);
        }
    }

    public static void UIM(int i)
    {
        Mods();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            PartUIOpen = !PartUIOpen;
        }
        GUI.DragWindow();
    }

    public static void Mods()
    {
        GUILayout.Label("Particle Systems");
        FistsEnabled = GUILayout.Toggle(FistsEnabled, "FireFists");
        Lightning = GUILayout.Toggle(Lightning, "Forest Lightning");
    }

    public static void Handler()
    {
        // fire Fists
        ParticleManager.Toggle("LeftFist", FistsEnabled);
        ParticleManager.Toggle("RightFist", FistsEnabled);
        ParticleManager.Toggle("Lightning", Lightning);
    }
}