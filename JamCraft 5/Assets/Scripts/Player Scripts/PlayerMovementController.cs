using UnityEngine;
using Utility.Development;

namespace JamCraft5.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float movementSpeed;
        private Rigidbody rb;
        private Vector2 playerInput;
        #endregion

        #region Awake
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        #endregion

        #region Update
        private void Update()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.With(playerInput.x * movementSpeed * Time.fixedDeltaTime, null, playerInput.y * movementSpeed * Time.fixedDeltaTime);
        }
        #endregion
    }
}
