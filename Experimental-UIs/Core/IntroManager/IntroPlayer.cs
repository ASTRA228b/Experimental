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

    private float delayTimer = 15f;
    private bool started;
    private GameObject? yes;
    private Rect IntroRect = new(0, 0, Screen.width, Screen.height);
    private bool openinging = false;
    private void Start()
    {
        string TempPath = PullFileFromDLL();

        Debug.Log($"[{Constantss.GUID}]: Video Path: {TempPath}");

        yes = new GameObject("StartupVideo");
        DontDestroyOnLoad(yes);

        ExperimentalPlayer = yes.AddComponent<VideoPlayer>();

        ExperimentalRender = new RenderTexture(1920, 1080, 0);
        ExperimentalRender.Create();

        ExperimentalPlayer.renderMode = VideoRenderMode.RenderTexture;
        ExperimentalPlayer.targetTexture = ExperimentalRender;

        ExperimentalPlayer.url = TempPath;
        ExperimentalPlayer.playOnAwake = false;
        ExperimentalPlayer.isLooping = false;
        ExperimentalPlayer.waitForFirstFrame = true;
        ExperimentalPlayer.audioOutputMode = VideoAudioOutputMode.None;

        ExperimentalPlayer.errorReceived += (vp, msg) =>
        {
            Debug.LogError($"[{Constantss.GUID}]: Video Error - {msg}");
        };

      
    }

    private void Update()
    {
        if (started)
            return;

        delayTimer -= Time.deltaTime;

        if (delayTimer <= 0f)
        {
            started = true;
           


            if (ExperimentalPlayer == null)
            {
                Debug.LogError($"[{Constantss.GUID}]: VideoPlayer was null");
                return;
                
            }

            Debug.Log($"[{Constantss.GUID}]: Preparing Intro Video");
            ExperimentalPlayer.Prepare();
             if (ExperimentalPlayer != null)
            ExperimentalPlayer.prepareCompleted += vp =>
            {
                vp.Play();
            };
            if (ExperimentalPlayer != null)
                ExperimentalPlayer.loopPointReached += vp =>
                {
                  Debug.Log($"[{Constantss.GUID}]: Video Finished");
                  OnVideoEnd(ExperimentalPlayer);
                    started = false;
                   
                };
        }
    }

    private void OnGUI()
    {

        IntroRect.width = Screen.width;
        IntroRect.height = Screen.height;
        if (openinging)
        {
            GUILayout.Window(9900090, IntroRect, yesss, "");
        }
        if (started == true)
        {
            openinging = true;
        }
        

    }
    private void OnVideoEnd(VideoPlayer vp) { if (vp != null) { Destroy(vp.gameObject); } }

    private void yesss(int h)
    {
        GUI.DrawTexture(
           new Rect(0, 0, IntroRect.width, IntroRect.height),
           Texture2D.blackTexture
       );

        GUI.DrawTexture(
            new Rect(0, 0, IntroRect.width, IntroRect.height),
            ExperimentalRender,
            ScaleMode.ScaleToFit
        );
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

        string InternalPath = Path.Combine(
            Application.persistentDataPath,
            "Experimental.mp4"
        );

        using FileStream EFile = new(
            InternalPath,
            FileMode.Create,
            FileAccess.Write
        );

        STD.CopyTo(EFile);

        Debug.Log($"[{Constantss.GUID}]: Extracted Video To: {InternalPath}");

        return InternalPath;
    }
}