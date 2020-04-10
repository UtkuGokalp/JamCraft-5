using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region Variables
    private GameObject obj;
    #endregion
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (obj.tag)
                //for every type of interaction define what does the script do
            {
                case "Door":
                    //GetComponent<DoorScr>().Use();
                    break;
                    //specify eveery action for every kind of interaction
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        obj = col.gameObject;
    }

}
