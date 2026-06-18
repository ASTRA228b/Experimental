using System;
using System.Collections.Generic;
using UnityEngine;
using Experimental.Mods.ModMenuMods.Managed;
using static Experimental.Mods.Settings.GlobalVars;
using static Experimental.Core.GUIHelpers.GlobalStyles;

namespace Experimental.Mods.GUIs;

public static class AModMenuUI
{
    public enum Cat
    {
        Fun,
        Movement,
        Misc,
        OP,
        Player
    }
    public static Cat Yes = Cat.Fun;
    private static int CPage = 0;
    private const int ModPerPage = 6;
    public static void MakeModUI()
    {
        if (ModMUIOpen)
        {
            ModMUIRect = GUILayout.Window(ModMWId, ModMUIRect, UIM, ModMUIName, WindowStyle);
        }
    }

    public static void UIM(int i)
    {
        Category();
        GUILayout.Space(5f);
        Mods();
        GUILayout.Space(5f);
        Pages();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            ModMUIOpen = !ModMUIOpen;
        }
        GUI.DragWindow();
    }

    public static void Category()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", Buttonss))
        {
            int CatIndex = (int)Yes - 1;

            if (CatIndex < 0)
                CatIndex = Enum.GetValues(typeof(Cat)).Length - 1;

            Yes = (Cat)CatIndex;
            CPage = 0;
        }

        GUILayout.Label($"< {Yes} >");

        if (GUILayout.Button(">", Buttonss))
        {
            int CatIndex = (int)Yes + 1;

            if (CatIndex >= Enum.GetValues(typeof(Cat)).Length)
                CatIndex = 0;

            Yes = (Cat)CatIndex;
            CPage = 0;
        }

        GUILayout.EndHorizontal();
    }

    public static void Mods()
    {
        List<ExpMod> Mods = ModsManager.GetMods(Yes);

        int StartIndex = CPage * ModPerPage;
        int EndIndex = Mathf.Min(StartIndex + ModPerPage, Mods.Count);

        for (int i = StartIndex; i < EndIndex; i++)
        {
            ExpMod Mod = Mods[i];

            bool NewValue = GUILayout.Toggle(Mod.Enabled, Mod.Name);

            if (NewValue != Mod.Enabled)
            {
                ModsManager.Toggle(Mod.Name);
            }
        }
    }

    public static void Pages()
    {
        int ModCount = ModsManager.GetMods(Yes).Count;

        int MaxPages = Mathf.Max
        (
            1,
            Mathf.CeilToInt((float)ModCount / ModPerPage)
        );

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("<", Buttonss))
        {
            CPage--;

            if (CPage < 0)
                CPage = MaxPages - 1;
        }

        GUILayout.Label($"Page {CPage + 1}/{MaxPages}");

        if (GUILayout.Button(">", Buttonss))
        {
            CPage++;

            if (CPage >= MaxPages)
                CPage = 0;
        }

        GUILayout.EndHorizontal();
    }
}