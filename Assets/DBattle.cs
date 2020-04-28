using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBattle : MonoBehaviour
{
    public DStat stat;
    public int gridPosX;
    public int gridPosY;

    float currentHp;

    private void OnEnable()
    {
        currentHp = stat.hp;
    }

    public void GetHit(float dame)
    {
        currentHp -= dame;
        if (currentHp < 0)
        {
            DGameSystem.LoadPool("Explosion", transform.position);
            DGameSystem.hasPlants[gridPosX][gridPosY] = false;
            gameObject.SetActive(false);

        }
    }

    public void Dead()
    {
        DGameSystem.LoadPool("Explosion", transform.position);
        DGameSystem.hasPlants[gridPosX][gridPosY] = false;
        gameObject.SetActive(false);
    }
}
