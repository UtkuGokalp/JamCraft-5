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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CraftSpace") && Input.GetKeyDown(KeyCode.E))
        {
            canv.enabled = true;
            mainCanv.GetComponent<Blacker>().blacker = true;
            PlayerUnlocking.playerPause = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
    public void Exit()
    {
        mainCanv.GetComponent<Blacker>().blacker = false;
        PlayerUnlocking.playerPause = false;
        canv.enabled = false;

    }
}
