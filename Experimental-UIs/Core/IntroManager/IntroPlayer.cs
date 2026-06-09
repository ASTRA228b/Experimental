using Experimental.Stuff;
using System.Reflection;
using UnityEngine;
using UnityEngine.Video;

namespace Experimental.Core.IntroManager;

public class IntroPlayer : MonoBehaviour
{
    private VideoPlayer? ExperimentalPlayer;
    private RenderTexture? ExperimentalRender;
    private bool Playin;
    private bool finished;
    private float delayTimer = 45f;
    private bool readyToShow = false;

    private void Start()
    {
        string TempPath = PullFileFromDLL();
        GameObject Video = new("StartupVideo");
        DontDestroyOnLoad(Video);
        ExperimentalPlayer = Video.AddComponent<VideoPlayer>();
        ExperimentalRender = new(1920, 1080, 0);
        ExperimentalPlayer.renderMode = VideoRenderMode.RenderTexture;
        ExperimentalPlayer.targetTexture = ExperimentalRender;
        ExperimentalPlayer.url = TempPath;
        ExperimentalPlayer.playOnAwake = false;
        ExperimentalPlayer.isLooping = false;
        ExperimentalPlayer.waitForFirstFrame = true;
        ExperimentalPlayer.prepareCompleted += vp =>
        {
            vp.Play();
            Playin = true;
        };
        ExperimentalPlayer.loopPointReached += vp =>
        {
            Playin = false;
            finished = true;

            Destroy(vp.gameObject);
            Destroy(this.gameObject);
        };
        ExperimentalPlayer.Prepare();
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
    private void Update()
    {
        if (readyToShow || finished) return;

        delayTimer -= Time.deltaTime;

        if (delayTimer <= 0f)
        {
            readyToShow = true;
        }
    }

    private void OnGUI()
    {
        if (finished || !readyToShow || ExperimentalRender == null) return;

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