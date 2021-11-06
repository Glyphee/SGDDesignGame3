using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 0);

    void Update()
    {
        transform.Rotate(direction * Time.deltaTime);
    }
}
