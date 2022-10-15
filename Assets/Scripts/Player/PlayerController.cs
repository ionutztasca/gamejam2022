

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

        Vector2 movement;

        private void Update()
        { 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }


        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.velocity = movement * moveSpeed;
        }
    }
}
