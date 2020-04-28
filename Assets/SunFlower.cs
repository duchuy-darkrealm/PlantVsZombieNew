using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : DPlant
{
    public override void SpawnObject()
    {
        GameObject coin = DGameSystem.LoadPool(stat.spawnObjName, transform.position + new Vector3(0,0.6f));
        coin.GetComponent<DCoin>().VERTICAL_DESTINATION = transform.position.y - 0.6f;
    }

    public override void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = Random.Range(stat.spawnRate * 0.75f, stat.spawnRate * 1.25f);
            SpawnObject();
        }
    }
}
