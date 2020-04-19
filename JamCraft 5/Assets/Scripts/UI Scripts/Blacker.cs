using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blacker : MonoBehaviour
{
    [SerializeField]
    private Image Black;
    public bool blacker;

    private void FixedUpdate()
    {
        if (blacker)
        {
            Black.enabled = true;
        }
        else {
            Black.enabled = false;
        }
    }
}
