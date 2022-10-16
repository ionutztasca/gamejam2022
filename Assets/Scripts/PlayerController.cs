

using System.Collections;
using UnityEngine;

namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class PlayerController: MonoBehaviour
    {
        #region ------------------------------------------- Fields ----------------------------------------

        // Components
        public Rigidbody2D rb;
        public PlayerStats playerStats;
        public GameObject poop;

        // Movement
        public float moveSpeed = 5;
        public float rotationSpeed = 1400;
        public Vector2 limitsSpeed = new Vector2(0.1f, 5);
        public Vector2 limitsSize = new Vector2(0.5f, 6);
        private Vector2 movement;
        private bool isAlive = true;
        private bool canPoop = true;

        // Animation
        public Animator playerAnim;

        // Sounds
        private PlayerSounds _playerSoundController;

        #endregion ---------------------------------------- Fields ----------------------------------------

        #region ------------------------------------------- Mono ----------------------------------------


        private void Awake()
        {
            playerAnim = this.gameObject.GetComponent<Animator>();
            _playerSoundController = this.gameObject.GetComponent<PlayerSounds>();
            StartCoroutine(PlayRandomSound());
        }

        private void Update()
        {
            if (!isAlive) return;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if(movement.x==0 && movement.y==0)
            {
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("idle");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canPoop)
                {
                    Instantiate(poop, transform.GetChild(0).transform.position, Quaternion.identity);
                    canPoop = false;
                    if (canPoop == false)
                    {
                        StartCoroutine(CooldownPoop());
                    }
                }
                
            }
        }

        private void FixedUpdate()
        {
            Move();
            PlayerMovementDirection();
        }

        #endregion ---------------------------------------- Mono ----------------------------------------

        #region ---------------------------------------- Methods ----------------------------------------

        private IEnumerator CooldownPoop()
        {
            playerStats.uIManager.UpdatePoopCDTimer("3");
            yield return new WaitForSeconds(1);
            playerStats.uIManager.UpdatePoopCDTimer("2");
            yield return new WaitForSeconds(1);
            playerStats.uIManager.UpdatePoopCDTimer("1");
            yield return new WaitForSeconds(1);
            playerStats.uIManager.UpdatePoopCDTimer("READY");
            canPoop = true;
        }
        
        private IEnumerator PlayRandomSound()
        {
            _playerSoundController.PlayPlayerSound();
            yield return new WaitForSecondsRealtime(Random.Range(0,6));
            StartCoroutine(PlayRandomSound());
        }

        #region ---------------------------------------- Movement ----------------------------------------

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

        #endregion ---------------------------------------- Movement ----------------------------------------

        #region ------------------------------------------- Player Stats ----------------------------------------

        public void UpdatePlayerAppearence()
        {

            SetPlayerSize(playerStats.fatScore/10f);
            UpdateSpeed(playerStats.fatScore / 100f);
           
        }
        public void SetPlayerDead()
        {
            isAlive = false;
            playerAnim.ResetTrigger("run");
            playerAnim.ResetTrigger("idle");
            playerAnim.SetTrigger("dead");
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        }
        private void SetPlayerSize(float value)
        {
            transform.localScale=new Vector3(Mathf.Clamp(value-1, limitsSize.x, limitsSize.y), Mathf.Clamp(value-1, limitsSize.x, limitsSize.y), 0);
        }
        private void UpdateSpeed(float value) {
            moveSpeed = Mathf.Clamp(3 - (moveSpeed* value), limitsSpeed.x, limitsSpeed.y);
        
        }

        #endregion ------------------------------------------- Player Stats ----------------------------------------

        #endregion ---------------------------------------- Methods ----------------------------------------

    }
}
