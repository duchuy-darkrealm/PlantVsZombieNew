using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPlant : MonoBehaviour
{
    public PlantStat stat;
    public float count;

    private void OnEnable()
    {
        count = Random.Range(stat.spawnRate*0.75f, stat.spawnRate * 1.25f);
    }

    public virtual void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = Random.Range(stat.spawnRate * 0.75f, stat.spawnRate * 1.25f);
            SpawnObject();
        }
    }

    public virtual void SpawnObject()
    {
        DGameSystem.LoadPool(stat.spawnObjName, transform.position);
    }
}
