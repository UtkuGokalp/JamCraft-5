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
    private Dropdown craftablesDrop;
    [SerializeField]
    private Text alertTxt;

    private List<Dropdown.OptionData> craftablesOptions;

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


        foreach (Craftable r in craftableList)
        {
            craftablesDrop.options.Add(new Dropdown.OptionData());
            craftablesDrop.options[craftablesDrop.options.Count - 1].text = r.Recipe.OutputItem.ItemData.itemName;
        }
    }

    public void Exit()
    {
        mainCanv.GetComponent<Blacker>().blacker = false;
        PlayerUnlocking.playerPause = false;
        canv.enabled = false;

    }

    public void Craft()
    {
        foreach (Craftable r in craftableList)
        {

            if (r.Recipe.OutputItem.ItemData.itemName == craftablesDrop.itemText.ToString())
            {
                InventoryItem i = r.Craft(inv);
                if (i != null)
                {
                    inv.AddItem(i);
                }
                else
                {
                    StartCoroutine(Alert());
                }
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
