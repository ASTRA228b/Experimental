using UnityEngine;

namespace Experimental.Core.MainManagers;

public class GSoundAutoPlayManager : MonoBehaviour
{
    private bool switchingSong;

    private void OnEnable()
    {
        SoundManager.ClipFinished += OnClipFinished;
    }

    private void OnDisable()
    {
        SoundManager.ClipFinished -= OnClipFinished;
    }

    private void Update()
    {
        SoundManager.Tick();
    }

    private async void OnClipFinished()
    {
        if (!GSoundGUIManager.AutoPlayNext)
            return;

        if (switchingSong)
            return;

        if (GSoundGUIManager.CurrentCard == null)
            return;

        switchingSong = true;

        try
        {
            await GSoundGUIManager.Next();
        }
        finally
        {
            switchingSong = false;
        }
    }
}