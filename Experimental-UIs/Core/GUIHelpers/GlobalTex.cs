using UnityEngine;

namespace Experimental.Core.GUIHelpers;

public static class GlobalTex
{
    public static Texture2D MakeTex(int WW, int HH, Color H)
    {
        Texture2D Yes = new(WW, HH);
        Yes.SetPixel(0, 0, H);
        Yes.Apply();
        return Yes;
    }
}
// GlobalTex.MakeTex();