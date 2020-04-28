using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBullet : MonoBehaviour
{
    public float speed = 3;
    public float dame = 30;
    public float existTime = 5f;
    float count;

    private void OnEnable()
    {
        count = existTime;
    }

    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);

        count -= Time.deltaTime;
        if (count < 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            collision.gameObject.SendMessage("GetHit", dame, SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
        }
    }
}
