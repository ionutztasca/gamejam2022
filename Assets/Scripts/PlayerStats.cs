//Script by Ionut
//Version: 0.0.1
//What does this script do?

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public enum BodyType { Skinny, Normal, Fat };

    public class PlayerStats: MonoBehaviour
    {
        
        public float fatScore = 20f;
        public float health = 100;
        public float healthDecreaseRate = 1;
        public PlayerController playerController;
        public UIManager uIManager;

        public Transform questFood;
        
        public BodyType playerBodyType;

        private bool isHealthDecreasing = false;

        private void Start()
        {
            playerBodyType = BodyType.Normal;
            uIManager.UpdateUIFatScore(fatScore);
            uIManager.UpdateUIHealth(health);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckFood(collision);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CheckEnemy(collision);
        }
        private void CheckEnemy(Collision2D collision)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (!enemy) return;

            OnEnemyEnter(enemy);
        }
        private void OnEnemyEnter(Enemy enemy)
        {
            enemy.HitPlayer();

        }
        private void CheckFood(Collider2D collision)
        {
            Food food = collision.GetComponent<Food>();
            if (!food) return;

            OnFoodEnter(food);
        }
        private void OnFoodEnter(Food food)
        {
            if (food.isGoodFood)
            {
                fatScore = Mathf.Clamp(fatScore-food.value, 0, 9999999999);
            }
            else
            {
                fatScore = Mathf.Clamp(fatScore + food.value, 0, 9999999999);
            }
            health += food.healthQuant;
            uIManager.UpdateUIHealth(health);
            if (food.isQuestItem)
            {
                HandleQuestItem(food);
            }
            else
            {
                Destroy(food.gameObject);
            }
           
            UpdateFatInfo(fatScore);
            
        }
        public void TakeDamageFromEnemy(float value)
        {
            Debug.Log("tookDamage: " + (health - value));
            health = Mathf.Clamp(health - value, 0, 999999);
            uIManager.UpdateUIHealth(health);
            CheckIfPlayerDead();

        }
        private void HandleQuestItem(Food food)
        {
            questFood = food.transform;
            questFood.GetComponent<CircleCollider2D>().enabled = false;
            questFood.GetComponent<FoodLevitate>().enabled = false;
            questFood.SetParent(transform);
            
        }
        private void UpdateFatInfo(float value)
        {
            uIManager.UpdateUIFatScore(value);
            UpdatePlayerFat();
            if(isHealthDecreasing==false)
                StartCoroutine(CheckDecreaseHealth());

        }
        private void UpdatePlayerFat()
        {
            if(fatScore>0 && fatScore < 20)//Skinny
            {
                playerBodyType = BodyType.Skinny;
                
            }else if(fatScore>=20 && fatScore < 40)
            {
                playerBodyType = BodyType.Normal;
            }
            else if (fatScore >= 40)
            {
                playerBodyType = BodyType.Fat;
            }

            playerController.UpdatePlayerAppearence();
        }

        private IEnumerator CheckDecreaseHealth()
        {
            while(playerBodyType == BodyType.Skinny && health>-1)
            {
                isHealthDecreasing = true;
                health -= healthDecreaseRate;
                uIManager.UpdateUIHealth(health);
                CheckIfPlayerDead();

                yield return new WaitForSeconds(1);
            }
            isHealthDecreasing = false;
        }
        private void CheckIfPlayerDead()
        {
            if (health < 0.1f)
            {
                KillPlayer();
            }
        }
        private void KillPlayer()
        {
            Debug.Log("RAT DIED");
            playerController.SetPlayerDead();
            StartCoroutine(RestartGame());
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
