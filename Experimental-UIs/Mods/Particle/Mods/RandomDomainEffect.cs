using UnityEngine;
using Experimental.Mods.Particle.Managers;

namespace Experimental.Mods.Particle.Mods;

public class RandomDomianEffect : Effect
{
    private readonly Vector3 Center;
    private readonly float Spwan;
    private float TimelastSpwan;

    public RandomDomianEffect(string name, Vector3 center, float spwanTime) : base(name)
    {
        Center = center;
        Spwan = spwanTime;
    }

    public override void Create()
    {
        Root = new GameObject(Name);
        Root.SetActive(false);
    }

    public override void Update()
    {
        TimelastSpwan += Time.deltaTime;
        if (TimelastSpwan > Spwan)
            return;

        TimelastSpwan = 0f;
        Lines();
        Orbs();
    }

    private void Lines() // ig
    {
        GameObject Yes = new GameObject("DomainLines");
        LineRenderer YesLine = Yes.AddComponent<LineRenderer>();
        YesLine.material = new Material(Shader.Find("Sprites/Default"));
        Color col = UnityEngine.Random.ColorHSV();
        YesLine.startColor = col;
        YesLine.endColor = col;
        YesLine.startWidth = 0.05f;
        YesLine.endWidth = 0.05f;
        YesLine.positionCount = 2;
        Vector3 Start = Center + new Vector3(UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-25f, 25f));
        Vector3 End = Center + new Vector3(UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-25f, 25f));
        YesLine.SetPosition(0, Start);
        YesLine.SetPosition(1, End);
        GameObject.Destroy(Yes, 1.5f);
    }

    private void Orbs()
    {
        GameObject Orbs = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Orbs.transform.position = Center + new Vector3(UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-25f, 25f), UnityEngine.Random.Range(-25f, 25f));
        Orbs.transform.localScale = Vector3.one * 0.3f;
        Collider Col = Orbs.GetComponent<Collider>();
        if (Col != null)
            GameObject.Destroy(Col);
        Renderer Render = Orbs.GetComponent<Renderer>();
        if (Render != null)
            Render.material.color = UnityEngine.Random.ColorHSV();

        GameObject.Destroy(Orbs, 2f);
    }
}