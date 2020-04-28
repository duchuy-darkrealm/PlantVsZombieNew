using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DStarEffect : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("SetActiveFalse", 1f);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
