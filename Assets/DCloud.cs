using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCloud : MonoBehaviour
{
    public float LEFT_MARGIN = -24f;
    public float RIGHT_MARGIN = 24f;
    public float speed = 0.1f;
    float UPDATE_RATE = 0.1f;
    float count = 0;

    private void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            count = UPDATE_RATE;
            transform.position -= new Vector3(speed, 0);
            if (transform.position.x < LEFT_MARGIN)
            {
                transform.position = new Vector3(RIGHT_MARGIN, transform.position.y);
            }
        }
    }
}
