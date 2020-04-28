using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DZombieSpawner : MonoBehaviour
{
    public List<string> zombieObjNames;
    public float SPAWN_RATE = 1f;
    public float DELAY_TIME = 15f;
    float time;
    float nextSpawn;

    private void Start()
    {
        zombieObjNames = new List<string> { "ZombieNormal" };
        nextSpawn = DELAY_TIME;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time < DELAY_TIME)
            return;

        if (time > nextSpawn)
        {
            nextSpawn += SPAWN_RATE;

            int rowId = Random.Range(0, DGameSystem.VERTICAL_NUMBER);
            float verticalPos = DGameSystem.instance.transform.position.y + rowId * DGameSystem.PADDING;
            float horizontalPos = DGameSystem.instance.transform.position.x + DGameSystem.HORIZONTAL_NUMBER * DGameSystem.PADDING;
            Vector3 position = new Vector3(horizontalPos, verticalPos);
            GameObject zombie = DGameSystem.LoadPool("ZombieNormal", position);
            zombie.GetComponent<DZombie>().rowId = rowId;
        }
    }

}
