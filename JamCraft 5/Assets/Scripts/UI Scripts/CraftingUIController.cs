using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using JamCraft5.Crafting;
using JamCraft5.Player.Inventory;
using JamCraft5.Items;
using Utility.Development;

public class CraftingUIController : MonoBehaviour
{
    [SerializeField]
    private Canvas canv;
    [SerializeField]
    private Canvas mainCanv;//The canvas of the life, weapons...
    [SerializeField]
    private List<Craftable> craftableList;
    [SerializeField]
    private Button D1;
    [SerializeField]
    private Button D2;

    [Header("Canvas Components")]
    [SerializeField]
    private Text alertTxt;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text materialsNec;
    private Craftable recip;

    private PlayerInventoryManager inv;

    private void Awake()
    {
        inv = GetComponent<PlayerInventoryManager>();
        canv.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CraftSpace") && Input.GetKeyDown(KeyCode.E))
        {
            if (JamCraft5.Player.Inventory.PlayerCarriedItemController.CarriedItemCollected)
            {

                SceneManager.LoadScene("Main Menu");
            }
            Enable();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    void Enable()
    {
        canv.enabled = true;
        mainCanv.GetComponent<Blacker>().blacker = true;
        PlayerUnlocking.playerPause = true;
        alertTxt.enabled = false;
        recip = null;
        itemName.enabled = false;
        materialsNec.enabled = false;
    }

    public void Exit()
    {
        mainCanv.GetComponent<Blacker>().blacker = false;
        PlayerUnlocking.playerPause = false;
        canv.enabled = false;

    }

    public void SelectCraft(int i)
    {
        recip = craftableList[i];
        itemName.enabled = true;
        materialsNec.enabled = true;

        itemName.text = recip.Recipe.OutputItem.ItemData.itemName;
        materialsNec.text = "";
        foreach (RecipeItem r in recip.Recipe.RequiredItems)
        {
            materialsNec.text += r.Item.ItemData.itemName + " x" + r.RequiredItemCount + System.Environment.NewLine;
        }
    }

    public void Craft()
    {
        if (recip == null) StartCoroutine(Alert());
        InventoryItem i = recip.Craft(inv);
        if (i != null)
        {
            inv.AddItem(i);
        }
        else
        {
            if(recip.Recipe.OutputItem.ItemData.itemName == "Dash1")
            {
                GetComponent<PlayerUnlocking>().Dash = true;
                D1.enabled = false;
            } else if(recip.Recipe.OutputItem.ItemData.itemName == "Dash2")
            {
                GetComponent<PlayerUnlocking>().Dash2 = true;
                D2.enabled = false;
            } else
            {
                StartCoroutine(Alert());
            }
        }
    }

    private IEnumerator Alert()
    {
        alertTxt.enabled = true;
        yield return new WaitForSeconds(1f);
        alertTxt.enabled = false;
    }
}
