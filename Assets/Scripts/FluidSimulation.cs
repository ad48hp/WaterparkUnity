using System.Collections.Generic;
using UnityEngine;

public class FluidSpawner : MonoBehaviour
{
    [Header("Kapka / Prefab")]
    public GameObject waterDropPrefab;
    public int maxDrops = 200;
    public float spawnRate = 50f;
    public float dropLifetime = 5f;

    [Header("Síla a gravitace")]
    public float forceDown = 3f;
    public float forwardForce = 1f;

    private float timer = 0f;
    private List<Drop> activeDrops = new List<Drop>();

    void Update()
    {
        timer += Time.deltaTime;
        float interval = 1f / spawnRate;

        while (timer >= interval && activeDrops.Count < maxDrops)
        {
            timer -= interval;
            SpawnDrop();
        }

        // Mazání starých kapek
        for (int i = activeDrops.Count - 1; i >= 0; i--)
        {
            if (Time.time - activeDrops[i].spawnTime > dropLifetime)
            {
                Destroy(activeDrops[i].gameObject);
                activeDrops.RemoveAt(i);
            }
        }
    }

    void SpawnDrop()
    {
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.05f, 0.05f), 0f, Random.Range(-0.05f, 0.05f));
        GameObject drop = Instantiate(waterDropPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = drop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Gravitace dolů + mírné posunutí vpřed (aby kapka tekla)
            Vector3 force = Vector3.down * forceDown + transform.forward * forwardForce;
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        activeDrops.Add(new Drop(drop, Time.time));
    }

    class Drop
    {
        public GameObject gameObject;
        public float spawnTime;

        public Drop(GameObject go, float time)
        {
            gameObject = go;
            spawnTime = time;
        }
    }
}
