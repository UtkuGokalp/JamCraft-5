using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Development;

public class CameraReference : MonoBehaviour
{
    [SerializeField]
    private float speed;
    void Update()
    {
        transform.position = GameUtility.PlayerPosition;
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -speed * Time.deltaTime, 0);
        }
    }
}
