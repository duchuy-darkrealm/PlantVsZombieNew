using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnSystem : MonoBehaviour
{
    public DZombieSpawner[] spawners;
    public WinCondition winCondition;

    public DZombieSpawner hugeSpawner;
    public float PREPARE_TIME = 300f;
    public float SHOW_MESSAGE_TIME = 4f;
    public float SPAWN_TIME = 15f;
    public int WAVE_AMOUNT = 2;
    public float BASE_HUGE_RATE_SPAWN = 0.1f;
    int waveCount = 0;

    float count;
    float MESSAGE_POINT;
    float SPAWN_POINT;
    float END_SPAWN_POINT;

    bool showMessage = false;

    private void OnEnable()
    {
        MESSAGE_POINT = PREPARE_TIME;
        SPAWN_POINT = PREPARE_TIME + SHOW_MESSAGE_TIME;
        END_SPAWN_POINT = PREPARE_TIME + SHOW_MESSAGE_TIME + SPAWN_TIME;
        waveCount = 0;
        count = 0;

        hugeSpawner.enabled = false;
        winCondition.enabled = false;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > END_SPAWN_POINT)
        {
            waveCount += 1;
            if (waveCount == WAVE_AMOUNT)
            {
                hugeSpawner.enabled = false;
                for (int i = 0; i < spawners.Length; i++)
                    spawners[i].enabled = false;

                winCondition.enabled = true;
            }  
            else
            {
                MESSAGE_POINT = count + PREPARE_TIME;
                SPAWN_POINT = count + PREPARE_TIME + SHOW_MESSAGE_TIME;
                END_SPAWN_POINT = count + PREPARE_TIME + SHOW_MESSAGE_TIME + SPAWN_TIME;

                //for (int i = 0; i < spawners.Length; i++)
                //    spawners[i].enabled = true;

                hugeSpawner.enabled = false;
                showMessage = false;
            }
        }
        else if (count > SPAWN_POINT)
        {
            hugeSpawner.enabled = true;
            hugeSpawner.SPAWN_RATE = BASE_HUGE_RATE_SPAWN / waveCount;
        }
        else if (count > MESSAGE_POINT)
        {
            if (showMessage)
            {
                return;
            } 
            else
            {
                //for (int i = 0; i < spawners.Length; i++)
                //    spawners[i].enabled = false;

                DGameSystem.instance.ShowMessage("A huge wave is coming!!", SHOW_MESSAGE_TIME);
                showMessage = true;
            } 
        }
    }
}
