using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public GameObject Jonh; // el pj
    void Update()
    {
        if (Jonh != null)
        {
            Vector3 position = transform.position;
            position.x = Jonh.transform.position.x;
            transform.position = position;

            position.y = Jonh.transform.position.y;
            transform.position = position;
        }
    }
}
