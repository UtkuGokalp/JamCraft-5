using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIController : MonoBehaviour
{
    [SerializeField]
    private Canvas canv;
    [SerializeField]
    private Canvas mainCanv;//The canvas of the life, weapons...
    private bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CrafSpace") && Input.GetKeyDown(KeyCode.E))
        {
            canv.enabled = true;
            mainCanv.GetComponent<Blacker>().blacker = true;
        }
    }

    private void Update()
    {
        if (active)
        {
            canv.enabled = false;
            mainCanv.GetComponent<Blacker>().blacker = true;
        }
    }
}
