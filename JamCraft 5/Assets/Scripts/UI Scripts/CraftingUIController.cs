using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JamCraft5.Crafting;
using JamCraft5.Player.Inventory;
using JamCraft5.Items;

public class CraftingUIController : MonoBehaviour
{
    [SerializeField]
    private Canvas canv;
    [SerializeField]
    private Canvas mainCanv;//The canvas of the life, weapons...
    [SerializeField]
    private List<Craftable> craftableList;

    [Header("Canvas Components")]
    [SerializeField]
    private Text alertTxt;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text description;
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
        description.enabled = false;
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
        description.enabled = true;
        materialsNec.enabled = true;

        itemName.text = recip.name;
        description.text = recip.recipe.ToString();
        materialsNec.text = "";
        foreach (RecipeItem r in recip.recipe.RequiredItems)
        {
            materialsNec.text += r.Item.ItemData.itemName.ToString() + " x" + r.RequiredItemCount.ToString() + ", ";
        }
    }

    public void Craft()
    {
        if (recip == null) StartCoroutine(Alert());
        InventoryItem i = recip.Craft(inv);
        if (i != null)
        {
            inv.AddItem(i);
        } else
        {
            StartCoroutine(Alert());
        }       
    }

    private IEnumerator Alert()
    {
        alertTxt.enabled = true;
        yield return new WaitForSeconds(1f);
        alertTxt.enabled = false;
    }
}
