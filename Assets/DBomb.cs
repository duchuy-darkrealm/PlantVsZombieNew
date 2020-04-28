using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBomb : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            DGameSystem.LoadPool("ZombieBurned", collision.transform.position);
            collision.gameObject.SetActive(false);
        }
    }
}
