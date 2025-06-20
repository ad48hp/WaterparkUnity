using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightRandomFlicker : MonoBehaviour
{
    public float baseIntensity = 1f;
    public float intensityVariation = 0.5f;

    public float minInterval = 0.1f;
    public float maxInterval = 0.5f;

    private Light targetLight;

    void Start()
    {
        targetLight = GetComponent<Light>();
        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            float offset = Random.Range(-intensityVariation, intensityVariation);
            targetLight.intensity = baseIntensity + offset;

            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
