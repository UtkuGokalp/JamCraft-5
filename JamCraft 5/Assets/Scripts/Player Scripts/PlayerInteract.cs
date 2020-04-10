using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    #region Variable(s)
    private GameObject obj;
    #endregion
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactables inter = new Error();
            switch (obj.tag)
            //for every type of interaction define what does the script do
            {
                case "Door":
                    inter = new Door();
                    break;
                case "Lever":
                    inter = new Lever();
                    break;
                    //specify eveery class and define the code in the class
            }
            inter.Interact(obj);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        obj = col.gameObject;
    }
    #region MainAbstractClass
    public abstract class Interactables{

        public abstract void Interact( GameObject obj);

    }
    #endregion

    #region ErrorCode
    public class Error : Interactables
    {
        public override void Interact(GameObject obj)
        {
            print("Player interaction error: object not found");
        }
    }
    #endregion

    #region (Test) Door
    public class Door : Interactables
    {
        public override void Interact(GameObject obj)
        {
            //The code for using the door
        }
    }
    #endregion

    #region (Test) Lever
    public class Lever : Interactables
    {
        public override void Interact(GameObject obj)
        {
            //The code for using the door
        }
    }
    #endregion
}
