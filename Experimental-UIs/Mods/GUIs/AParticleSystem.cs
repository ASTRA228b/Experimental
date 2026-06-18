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
    private static bool NabulaStorm = false;
    private static bool BlackWhole = false;
    private static bool WhiteWhole = false;

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
        NabulaStorm = GUILayout.Toggle(NabulaStorm, "NebulaStorm");
        BlackWhole = GUILayout.Toggle(BlackWhole, "Black Whole");
        WhiteWhole = GUILayout.Toggle(WhiteWhole, "White Whole");
    }

    public static void Handler()
    {
        // fire Fists
        ParticleManager.Toggle("LeftFist", FistsEnabled);
        ParticleManager.Toggle("RightFist", FistsEnabled);
        // light thingy yes
        ParticleManager.Toggle("Lightning", Lightning);
        // Nebula
        ParticleManager.Toggle("NebulaStorm", NabulaStorm);
        // black & white whole
        ParticleManager.Toggle("BlackHole", BlackWhole);
        ParticleManager.Toggle("WhiteHole", WhiteWhole);
    }
}