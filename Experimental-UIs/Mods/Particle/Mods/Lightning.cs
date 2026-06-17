using UnityEngine;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class Lightning : Effect
{
    private readonly Vector3 StrikePosition;
    private readonly float StrikeInterval;
    private float timeSinceLastStrike;
    public Lightning(string name, Vector3 strikePosition, float strikeInterval = 2f): base(name)
    {
        StrikePosition = strikePosition;
        StrikeInterval = strikeInterval;
    }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.SetActive(false);
    }

    public override void Update()
    {
        timeSinceLastStrike += Time.deltaTime;
        if (timeSinceLastStrike < StrikeInterval)
            return;
        timeSinceLastStrike = 0f;
        GameObject strike = new GameObject("LightningStrike");
        LineRenderer line = strike.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.white;
        line.endColor = new Color(0.5f, 0.5f, 1f);
        line.useWorldSpace = true;
        Vector3 start = new Vector3(StrikePosition.x + UnityEngine.Random.Range(-25f, 25f), StrikePosition.y + UnityEngine.Random.Range(10f, 20f), StrikePosition.z + UnityEngine.Random.Range(-25f, 25f));
        Vector3 end = new Vector3(StrikePosition.x + UnityEngine.Random.Range(-10f, 10f), StrikePosition.y - UnityEngine.Random.Range(10f, 20f), StrikePosition.z + UnityEngine.Random.Range(-10f, 10f));
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        Light light = strike.AddComponent<Light>();
        light.color = Color.white;
        light.intensity = UnityEngine.Random.Range(4f, 8f);
        light.range = 10f;
        light.shadows = LightShadows.None;
        GameObject.Destroy(light, 0.1f);
        GameObject.Destroy(strike, UnityEngine.Random.Range(0.1f, 0.5f));
    } 
}