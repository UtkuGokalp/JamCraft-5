using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScr : MonoBehaviour
{
    public int speed;
    public float runMultiplier;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))//It would be better to deffine an axis to the inputs in the edit>project settings>Input>axes
        {
            transform.Translate(0,0, speed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0,0, -speed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        
    }
}
