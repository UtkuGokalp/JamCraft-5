using UnityEngine;
using JamCraft5.EventArguments;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Player.Weapon
{
    [RequireComponent(typeof(PlayerInventoryManager))]
    public class PlayerWeaponHoldingController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform weaponHolder;
        private GameObject[] weaponsObtained;
        private PlayerInventoryManager playerInventoryManager;
        [SerializeField]
        private Crafting.Craftable weapon;
        #endregion

        #region Awake
        private void Awake()
        {
            weaponsObtained = new GameObject[4];
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            playerInventoryManager.OnWeaponAdded += OnWeaponAdded;
            playerInventoryManager.OnWeaponRemoved += OnWeaponRemoved;
            playerInventoryManager.OnSelectedWeaponChanged += OnSelectedWeaponChanged;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            playerInventoryManager.OnWeaponAdded -= OnWeaponAdded;
            playerInventoryManager.OnWeaponRemoved -= OnWeaponRemoved;
            playerInventoryManager.OnSelectedWeaponChanged -= OnSelectedWeaponChanged;
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                var t = weapon.Craft(playerInventoryManager);
                if (t != null)
                {
                    playerInventoryManager.AddWeapon(Items.ItemConverter.ToInventoryWeapon(t));
                }
            }
        }

        #region OnWeaponAdded
        private void OnWeaponAdded(object sender, OnWeaponAddedEventArgs e)
        {
            GameObject spawnedWeapon = Instantiate(e.AddedWeapon.ItemData.holdingPrefab.gameObject, weaponHolder);
            spawnedWeapon.transform.localPosition = Vector3.zero;
            spawnedWeapon.transform.localRotation = Quaternion.identity;
            weaponsObtained[e.SlotIndex] = weaponHolder.GetChild(e.SlotIndex).gameObject;
        }
        #endregion

        private void OnWeaponRemoved(object sender,OnWeaponRemovedEventArgs e)
        {
            Destroy(weaponsObtained[e.SlotIndex]);
        }

        #region OnSelectedWeaponChanged
        private void OnSelectedWeaponChanged(object sender, OnSelectedWeaponChangedEventArgs e)
        {
            for (int i = 0; i < weaponsObtained.Length; i++)
            {
                if (weaponsObtained[i] != null)
                {
                    if (i == e.SlotIndex)
                    {
                        weaponsObtained[i].SetActive(true);
                    }
                    else
                    {
                        weaponsObtained[i].SetActive(false);
                    }
                }
            }
        }
        #endregion
    }
}
