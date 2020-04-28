using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRotate : MonoBehaviour
{
    public float angle = 1f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
}
