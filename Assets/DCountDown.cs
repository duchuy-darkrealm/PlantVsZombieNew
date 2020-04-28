using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCountDown : MonoBehaviour
{
    public enum CountDownAction { DoNothing, SetActiveFalse, Destroy, SetActiveFalseParent, DestroyParent }

    public float countDownTime;
    public float count;
    public CountDownAction countDownAction;

    private void OnEnable()
    {
        count = 0;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= countDownTime)
        {
            if (countDownAction == CountDownAction.SetActiveFalse) gameObject.SetActive(false);
            if (countDownAction == CountDownAction.Destroy) Destroy(gameObject);
            if (countDownAction == CountDownAction.SetActiveFalseParent) transform.parent.gameObject.SetActive(false);
            if (countDownAction == CountDownAction.DestroyParent) Destroy(transform.parent.gameObject);
        }
    }
}
