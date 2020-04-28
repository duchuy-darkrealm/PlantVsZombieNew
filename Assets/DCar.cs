using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCar : MonoBehaviour
{
    public int rowId;
    public bool run;
    public float speed = 2;

    private void OnEnable()
    {
        run = false;
    }

    private void Update()
    {
        if (run)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
            Debug.Log("running!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            run = true;
            DGameSystem.LoadPool("Explosion", collision.transform.position);
            collision.gameObject.SetActive(false);
        }
    }

}
