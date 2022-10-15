

using UnityEngine;

namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class PlayerController: MonoBehaviour
    {
        public float moveSpeed = 5;
        public Rigidbody2D rb;
        public float rotationSpeed = 1400;
        public PlayerStats playerStats;
        public Vector2 limitsSpeed = new Vector2(0.1f, 5);
        public Vector2 limitsSize = new Vector2(0.5f, 6);
        private Vector2 movement;

        // Animation
        public Animator playerAnim;

        private void Awake()
        {
            playerAnim = this.gameObject.GetComponent<Animator>();
        }

        private void Update()
        { 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if(movement.x==0 && movement.y==0)
            {
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("idle");
            }
        }


        private void FixedUpdate()
        {
            Move();
            PlayerMovementDirection();
        }

        private void Move()
        {
            rb.velocity = movement * moveSpeed;
            playerAnim.SetTrigger("run");
            playerAnim.ResetTrigger("idle");

        }
        private void PlayerMovementDirection()
        {
            Vector2 movementDirection = new Vector2(movement.x, movement.y);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            movementDirection.Normalize();
            transform.Translate(movementDirection * moveSpeed * inputMagnitude * Time.deltaTime, Space.World);
            if (movementDirection != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        public void UpdatePlayerAppearence()
        {

            SetPlayerSize(playerStats.fatScore/10f);
            UpdateSpeed(playerStats.fatScore / 100f);
            //switch (bodyType)
            //{
            //    case BodyType.Skinny:
            //        break;
            //    case BodyType.Normal:
            //        break;
            //    case BodyType.Fat:
            //        break;
            //    default:
            //        break;
            //}
        }
        private void SetPlayerSize(float value)
        {
            transform.localScale=new Vector3(Mathf.Clamp(value, limitsSize.x, limitsSize.y), Mathf.Clamp(value, limitsSize.x, limitsSize.y), 0);
        }
        private void UpdateSpeed(float value) {
            moveSpeed = Mathf.Clamp(3 - (moveSpeed* value), limitsSpeed.x, limitsSpeed.y);
        
        }
    }
}
