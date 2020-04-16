using JamCraft5.EventArguments;
using JamCraft5.Player.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Transform slotMarker;
    [SerializeField]
    private Transform[] slots;
    private GameObject[] slotIcons;
    private PlayerInventoryManager playerInventoryManager;

    private void Awake()
    {
        playerInventoryManager = FindObjectOfType<PlayerInventoryManager>();
        slotIcons = new GameObject[slots.Length];
        for (int i = 0; i < slotIcons.Length; i++)
        {
            slotIcons[i] = slots[i].GetChild(0).GetChild(0).GetChild(0).gameObject;
            Image currentImage = slotIcons[i].GetComponentInChildren<Image>();
            if (currentImage.sprite == null)
            {
                slotIcons[i].SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        playerInventoryManager.OnWeaponAdded += OnWeaponAdded;
        playerInventoryManager.OnSelectedWeaponChanged += OnSelectedWeaponChanged;
    }

    private void OnDisable()
    {
        playerInventoryManager.OnWeaponAdded -= OnWeaponAdded;
        playerInventoryManager.OnSelectedWeaponChanged -= OnSelectedWeaponChanged;
    }

    private void OnSelectedWeaponChanged(object sender, OnSelectedWeaponChangedEventArgs e)
    {
        int slotIndex = e.SlotIndex;
        slotMarker.position = slots[slotIndex].position;
    }

    private void OnWeaponAdded(object sender, OnWeaponAddedEventArgs e)
    {
        slotIcons[e.SlotIndex].GetComponent<Image>().sprite = e.AddedWeapon.ItemData.uiIcon;
        slotIcons[e.SlotIndex].SetActive(true);
    }
}
