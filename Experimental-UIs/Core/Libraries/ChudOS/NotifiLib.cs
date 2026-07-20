using UnityEngine;

namespace Experimental.Core.Libraries.ChudOS;

public static class NotifiLib
{
    public class ToastData
    {
        public string toast = "";
        public long toastUntil;
    }
    private static readonly List<ToastData> Toasts = new();

    public static void InitToast()
    {
        GUIStylesEx.Init();
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        Toasts.RemoveAll(toast => now >= toast.toastUntil);
        const float width = 320f;
        const float height = 54f;
        const float spacing = 8f;
        float x = Screen.width - width - 22f;
        float bottomY = Screen.height - height - 22f;

        for (int i = Toasts.Count - 1; i >= 0; i--)
        {
            ToastData toast = Toasts[i];
            int stackIndex = Toasts.Count - 1 - i;
            float y = bottomY - stackIndex * (height + spacing);
            Rect backgroundRect = new(x, y, width, height);
            Rect accentRect = new(x, y, 6f, height);
            Rect textRect = new(x + 20f, y, width - 30f, height);
            GUI.Box(backgroundRect, GUIContent.none, GUIStylesEx.ToastBackground);
            GUI.Box(accentRect, GUIContent.none, GUIStylesEx.AccentBar);
            GUI.Label(textRect, toast.toast, GUIStylesEx.ToastLabel);
        }
    }

    public static void MessageToast(string message, int durationMilliseconds = 5000)
    {
        Toasts.Add(new ToastData
        {
            toast = message,
            toastUntil = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + durationMilliseconds
        });
    }
}
