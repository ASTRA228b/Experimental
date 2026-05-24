using UnityEngine;

namespace Experimental.Core.GUIHelpers;

public static class GlobalStyles
{
    public static GUIStyle? WindowStyle, SliderStyle, SliderThumbStyle, Buttonss;

    private static Texture2D? Windowtex, Background, Slidertex, SliderThumbtex;
    private static Color WindowColor = new(0.1f, 0.1f, 0.1f, 1f);
    private static Color sliderTrackColor = new(0.15f, 0.15f, 0.15f, 1f);
    private static Color sliderThumbColor = new(0.0f, 0.6f, 1f, 1f);
    private static Color ButtonColor = new(0.2f, 0.2f, 0.2f, 1f);

    public static void INIT()
    {
        Windowtex = GlobalTex.MakeTex(1, 1, WindowColor);
        Slidertex = GlobalTex.MakeTex(1, 1, sliderTrackColor);
        SliderThumbtex = GlobalTex.MakeTex(1, 1, sliderThumbColor);
        Background = GlobalTex.MakeTex(1, 1, ButtonColor);
        WindowStyle = new GUIStyle(GUI.skin.window);
        Buttonss = new GUIStyle(GUI.skin.button);
        SliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
        SliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
        WindowStyle.normal.textColor = Color.white;
        WindowStyle.fontStyle = FontStyle.Normal;
        Buttonss.normal.textColor = Color.white;
        Buttonss.hover.textColor = Color.blue;
        Buttonss.active.textColor = Color.red;
        Buttonss.focused.textColor = Color.white;
        Buttonss.onNormal.textColor = Color.blue;
        Buttonss.onHover.textColor = Color.blue;
        Buttonss.onActive.textColor = Color.blue;
        Buttonss.onFocused.textColor = Color.blue;
        // only used for buttons and windows 
        ApplyBackground(Buttonss, Background);
        ApplyBackground(WindowStyle, Windowtex);
        ApplyBackground(SliderThumbStyle, SliderThumbtex);
        ApplyBackground(SliderStyle, Slidertex);
    }
    public static void ApplyBackground(GUIStyle style, Texture2D tex)
    {
        style.normal.background = tex;
        style.hover.background = tex;
        style.active.background = tex;
        style.focused.background = tex;
        style.onNormal.background = tex;
        style.onHover.background = tex;
        style.onActive.background = tex;
        style.onFocused.background = tex;
    }
}