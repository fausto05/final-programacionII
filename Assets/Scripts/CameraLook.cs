using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public GameObject Jonh;
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Jonh.transform.position.x;
        transform.position = position;
    }
}
