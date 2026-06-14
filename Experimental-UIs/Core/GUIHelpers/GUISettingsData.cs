using UnityEngine;

namespace Experimental.Core.GUIHelpers;

[System.Serializable]
public class GUISettingsData
{
    public Color WindowColor = new(0.1f, 0.1f, 0.1f, 1f);
    public Color ButtonColor = new(0.2f, 0.2f, 0.2f, 1f);
    public Color SliderTrackColor = new(0.15f, 0.15f, 0.15f, 1f);
    public Color SliderThumbColor = new(0f, 0.6f, 1f, 1f);
}