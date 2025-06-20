using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(ParticleSystemRenderer))]
public class WaterParticleSetup : MonoBehaviour
{
    public Texture2D waterTexture; // Přetáhni texturu vody sem v Inspectoru

    void Start()
    {
        SetupWaterParticles();
    }

    void SetupWaterParticles()
    {
        var ps = GetComponent<ParticleSystem>();
        var psMain = ps.main;
        psMain.duration = 5f;
        psMain.loop = true;
        psMain.startLifetime = 2f;
        psMain.startSpeed = 2f;
        psMain.startSize = 0.15f;
        psMain.simulationSpace = ParticleSystemSimulationSpace.World;
        psMain.maxParticles = 15000;

        var emission = ps.emission;
        emission.rateOverTime = 3000f;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(0.3f, 0.3f, 0.3f);

        var force = ps.forceOverLifetime;
        force.enabled = true;
        force.y = -9.81f; // gravitace

        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(1f, 1f, 1f), 0.0f),
                new GradientColorKey(new Color(1f, 1f, 1f), 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.4f, 0.1f),
                new GradientAlphaKey(0.4f, 0.9f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);

        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.5f);
        curve.AddKey(1.0f, 0.0f);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, curve);

        var collision = ps.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.mode = ParticleSystemCollisionMode.Collision3D;
        collision.dampen = 0.2f;
        collision.bounce = 0f;
        collision.lifetimeLoss = 0.1f;
        collision.collidesWith = LayerMask.GetMask("Default");
        collision.sendCollisionMessages = true;

        // Nastavení renderování s texturou vody
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.sortingFudge = 2f;

        Material mat = new Material(Shader.Find("Particles/Standard Unlit"));
        if (waterTexture != null)
        {
            mat.mainTexture = waterTexture;
        }
        mat.color = new Color(1f, 1f, 1f, 0.4f); // lehce modrá průhledná
        mat.SetFloat("_Mode", 4); // Transparent
        /*mat.EnableKeyword("_ALPHABLEND_ON");
		mat.EnableKeyword("_EMISSION");
		mat.EnableKeyword("_ADDITIVE_ON");
		
		mat.EnableKeyword("COLOR_ADD");
		mat.DisableKeyword("COLOR_MULTIPLY");
		mat.DisableKeyword("COLOR_OVERLAY");
		mat.DisableKeyword("COLOR_SUBTRACT");*/

        renderer.material = mat;

        Debug.Log("✅ Realistická voda připravena.");
    }
}
