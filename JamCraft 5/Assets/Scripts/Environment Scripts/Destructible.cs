using UnityEngine;
using Utility.Health;
using JamCraft5.Items.Controllers;
using Utility.Development;
using JamCraft5.Player.Attack;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Environment
{
	[RequireComponent(typeof(HealthSystem))]
	[RequireComponent(typeof(GroundedItemDropController))]
	public class Destructible : MonoBehaviour
	{
		#region Variables
		private HealthSystem healthSystem;
		private PlayerInventoryManager playerInventoryManager;
		private GroundedItemDropController groundedItemDropController;
		#endregion

		#region Awake
		private void Awake()
		{
			healthSystem = GetComponent<HealthSystem>();
			groundedItemDropController = GetComponent<GroundedItemDropController>();
			playerInventoryManager = GameUtility.Player.GetComponent<PlayerInventoryManager>();
		}
		#endregion

		#region OnEnable
		private void OnEnable()
		{
			healthSystem.OnDeath += OnDestroyed;
		}
		#endregion

		#region OnDisable
		private void OnDisable()
		{
			healthSystem.OnDeath -= OnDestroyed;
		}
		#endregion

		#region OnTriggerEnter
		private void OnTriggerEnter(Collider collision)
		{
			if (collision.CompareTag(GameUtility.PLAYER_TAG) && PlayerAttack.Attacking)
			{
				healthSystem.DecreaseHealth(playerInventoryManager.SelectedWeapon.ContainedWeapon.ItemData.weaponDamage);
			}
		}
		#endregion

		#region OnDestroyed
		private void OnDestroyed()
		{
			groundedItemDropController.DropItems();
			Destroy(gameObject);
		}
		#endregion
	}
}
