using UnityEngine;
using JamCraft5.Camera;

namespace Utility.Development
{
    public static class GameUtility
    {
        #region Variables
        private static Camera mainCam;
        private static GameObject player;
        private static Collider playerCollider;
        private static Transform playerTransform;
        private static Transform mainCamTransform;

        public static Camera MainCam
        {
            get
            {
                if (mainCam == null)
                {
                    mainCam = Camera.main;
                }
                return mainCam;
            }
        }
        public static Vector3 MousePosition
        {
            get
            {
                //Mouse position is being calculated this way because the camera is a perspective camera.
                Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                plane.Raycast(ray, out float distance);
                return ray.GetPoint(distance);
            }
        }
        public static GameObject Player
        {
            get
            {
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
                }
                return player;
            }
        }
        public static Transform PlayerTransform
        {
            get
            {
                if (playerTransform == null)
                {
                    playerTransform = Player.transform;
                }
                return playerTransform;
            }
        }
        public static Transform MainCamTransform
        {
            get
            {
                if (mainCamTransform == null)
                {
                    mainCamTransform = MainCam.transform;
                }
                return mainCamTransform;
            }
        }
        public static Collider PlayerCollider
        {
            get
            {
                if (playerCollider == null)
                {
                    playerCollider = Player.GetComponent<Collider>();
                }
                return playerCollider;
            }
        }

        public static Vector3 PlayerPosition => PlayerTransform.position;
        public static Vector3 MainCamPosition => MainCamTransform.position;
        public static LayerMask GroundedItemLayer => LayerMask.NameToLayer(GROUNDED_ITEM_LAYER_NAME);
        public static LayerMask PlayerLayer => LayerMask.NameToLayer(PLAYER_LAYER_NAME);
        public static LayerMask EnemyLayer => LayerMask.NameToLayer(ENEMY_LAYER_NAME);
        public const string PLAYER_TAG = "Player";
        public const string PLAYER_LAYER_NAME = "Player";
        public const string GROUNDED_ITEM_LAYER_NAME = "Grounded Item";
        public const string ENEMY_LAYER_NAME = "Enemy";
        public const string HAMMER_ATTACK_ANIMATION_TRIGGER_NAME = "HammerAttack";
        public const string SHOTGUN_ATTACK_ANIMATION_TRIGGER_NAME = "ShotgunAttack";
        public const string SWORD_ATTACK_ANIMATION_TRIGGER_NAME = "SwordAttack";
        public const string HALBERD_ATTACK_ANIMATION_TRIGGER_NAME = "HalberdAttack";
        public const string ENEMY_DEAD_ANIMATION_TRIGGER_NAME = "Dead";
        #endregion

        #region GetDirection
        /// <summary>
        /// Gets the normalized direction from position to to position.
        /// </summary>
        public static Vector2 GetDirection(Vector2 from, Vector2 to) => (to - from).normalized;
        /// <summary>
        /// Gets the normalized direction from position to to position.
        /// </summary>
        public static Vector3 GetDirection(Vector3 from, Vector3 to) => (to - from).normalized;
        #endregion

        #region GetDirectionToMouse
        public static Vector3 GetDirectionToMouse(Vector3 from) => GetDirection(from, MousePosition);
        #endregion

        #region GetAngleFromVector
        public static float GetAngleFromVector(Vector2 direction) => Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        #endregion

        #region BetweenCameraAndPlayer
        public static bool BetweenCameraAndPlayer(Transform transform)
        {
            Vector3 startingPosition = MainCamPosition;
            Vector3 direction = GetDirection(startingPosition, PlayerPosition);
            RaycastHit[] hitInfos = Physics.RaycastAll(startingPosition, direction, CameraFollowing.DistanceBetweenCameraAndPlayer);

            foreach (RaycastHit hitInfo in hitInfos)
            {
                if (hitInfo.transform == transform)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region BetweenCameraAndPlayerNonAlloc
        public static bool BetweenCameraAndPlayerNonAlloc(Transform transform, RaycastHit[] hitInfos)
        {
            Vector3 startingPosition = MainCamPosition;
            Vector3 direction = GetDirection(startingPosition, PlayerPosition);
            Physics.RaycastNonAlloc(startingPosition, direction, hitInfos, CameraFollowing.DistanceBetweenCameraAndPlayer);

            foreach (RaycastHit hitInfo in hitInfos)
            {
                if (hitInfo.transform == transform)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
