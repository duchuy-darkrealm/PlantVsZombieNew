using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownExplode : MonoBehaviour
{
    public string explodeObjName;
    public float countDownTime = 3;
    public float speed = 1f;

    public float scale_x = 1;
    public float scale_y = 1;

    float count;
    

    private void OnEnable()
    {
        count = 0;
        transform.localScale = new Vector3(scale_x, scale_y);
    }

    private void Update()
    {
        count += Time.deltaTime;

        if (count < countDownTime)
        {
            float newSize = (count / countDownTime) * scale_x + scale_x;
            transform.localScale = new Vector3(newSize, newSize);
        }
        else
        {
            DBattle battle = GetComponent<DBattle>();
            if (battle != null)
            {
                DGameSystem.LoadPool(explodeObjName, transform.position);
                battle.Dead();
            }
            else
            {
                DGameSystem.LoadPool(explodeObjName, transform.position);
                gameObject.SetActive(false);
            }
        }
    }

}
