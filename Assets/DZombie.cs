using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DZombie : MonoBehaviour
{
    public DZombieStat stat;
    public int rowId;

    enum STATE {WALKING, EATING};
    STATE state;

    GameObject enemy;
    float attackRange;
    float count;

    float currentHp;

   
    private void OnEnable()
    {
        state = STATE.WALKING;
        attackRange = Random.Range(0.75f, 1.25f) * stat.attackRange;
        currentHp = stat.hp;
    }

    private void Update()
    {
        if (transform.position.x < -25f)
            gameObject.SetActive(false);

        if (state == STATE.WALKING)
        {
            transform.position -= new Vector3(stat.speed * Time.deltaTime, 0);
            GameObject collidePlant = CheckCollidePlant();
            if (collidePlant != null)
            {
                state = STATE.EATING;
                GetComponent<Animator>().SetTrigger("eating");
                enemy = collidePlant;
            }
        }
        else
        {
            if (enemy == null || enemy.activeSelf == false)
            {
                enemy = CheckCollidePlant();
                if (enemy == null || enemy.activeSelf == false)
                {
                    state = STATE.WALKING;
                    GetComponent<Animator>().SetTrigger("walking");
                }
                else
                    Attack();
            }
            else
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = stat.attackRate;
            enemy.SendMessage("GetHit", stat.dame, SendMessageOptions.DontRequireReceiver);
        }
    }

    private GameObject CheckCollidePlant()
    {
        for (int i = 0; i < DGameSystem.HORIZONTAL_NUMBER; i++)
        {
            if (DGameSystem.grids[rowId][i] == null)
                continue;

            if (DGameSystem.grids[rowId][i].CompareTag("Plant") == false)
                continue;

            float distance = transform.position.x - DGameSystem.grids[rowId][i].transform.position.x;

            if (distance > -0.3f && distance < attackRange && DGameSystem.hasPlants[rowId][i] == true)
            {
                return DGameSystem.grids[rowId][i];
            }
        }

        return null;
    }

    public void GetHit(float dame)
    {
        Debug.Log("I get Hit!");
        currentHp -= dame;
        if (currentHp < 0)
        {
            DGameSystem.LoadPool("Explosion", transform.position);
            gameObject.SetActive(false);
        }
    }
}
