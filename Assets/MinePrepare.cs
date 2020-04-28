using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePrepare : MonoBehaviour
{
    readonly float PREPARE_TIME = 10f;
    float count;

    public PlantStat stat;

    private void OnEnable()
    {
        count = 0;
        GetComponent<Animator>().SetBool("isReady", false);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > PREPARE_TIME)
        {
            GetComponent<Animator>().SetBool("isReady", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (count < PREPARE_TIME)
            return;

        if (collision.CompareTag("Zombie"))
        {
            DGameSystem.LoadPool("Spudow", transform.position);
            GetComponent<DBattle>().Dead();
            gameObject.SetActive(false);
        }
    }
}
