using Experimental.Stuff;
using System.Reflection;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

namespace Experimental.Core.IntroManager;

public class IntroPlayer : MonoBehaviour
{
    private VideoPlayer? ExperimentalPlayer;
    private RenderTexture? ExperimentalRender;

    private bool readyToShow;
    private bool videoReady;
    private bool finished;

    private float delayTimer = 45f;

    private void Start()
    {
        string TempPath = PullFileFromDLL();

        GameObject Video = new("StartupVideo");
        DontDestroyOnLoad(Video);

        ExperimentalPlayer = Video.AddComponent<VideoPlayer>();

        ExperimentalRender = new RenderTexture(1920, 1080, 0);

        ExperimentalPlayer.renderMode = VideoRenderMode.RenderTexture;
        ExperimentalPlayer.targetTexture = ExperimentalRender;

        ExperimentalPlayer.url = TempPath;
        ExperimentalPlayer.playOnAwake = false;
        ExperimentalPlayer.isLooping = false;
        ExperimentalPlayer.waitForFirstFrame = true;

        ExperimentalPlayer.audioOutputMode = VideoAudioOutputMode.None;
    }

    private void Update()
    {
        if (finished) return;

        if (!readyToShow)
        {
            delayTimer -= Time.deltaTime;

            if (delayTimer <= 0f)
            {
                readyToShow = true;

                if (ExperimentalPlayer == null) return;

                ExperimentalPlayer.prepareCompleted += vp =>
                {
                    vp.Play();
                    videoReady = true;
                };

                ExperimentalPlayer.Prepare();
                ExperimentalPlayer.Play();
            }
        }
    }

    private string PullFileFromDLL()
    {
        Assembly ExperimentalASM = Assembly.GetExecutingAssembly();
        string RCSName = "Experimental.RCS.Experimental.mp4";

        using Stream STD = ExperimentalASM.GetManifestResourceStream(RCSName);

        if (STD == null)
        {
            Debug.LogError($"[{Constantss.GUID}]: Embedded Video Not Found");
            return "";
        }

        string InternalPath = Path.Combine(Application.persistentDataPath, "Experimental.mp4");

        using FileStream EFile = new(InternalPath, FileMode.Create, FileAccess.Write);
        STD.CopyTo(EFile);

        return InternalPath;
    }

    private void OnGUI()
    {
        if (finished || !readyToShow || !videoReady || ExperimentalRender == null)
            return;

        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            Texture2D.blackTexture
        );

        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            ExperimentalRender,
            ScaleMode.ScaleToFit
        );
    }
}