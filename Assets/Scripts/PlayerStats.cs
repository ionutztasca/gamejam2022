//Script by Ionut
//Version: 0.0.1
//What does this script do?

using System.Collections;
using UnityEngine;


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
       
        
        public BodyType playerBodyType;


        private void Start()
        {
            playerBodyType = BodyType.Normal;
            uIManager.UpdateUIFatScore(fatScore);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckFood(collision);
            CheckEnemy(collision);
        }
        private void CheckEnemy(Collider2D collision)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy) return;

            OnEnemyEnter(enemy);
        }
        private void OnEnemyEnter(Enemy enemy)
        {
           
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

            UpdateFatInfo(fatScore);
        }
            
        private void UpdateFatInfo(float value)
        {
            uIManager.UpdateUIFatScore(value);
            UpdatePlayerFat();
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
            while(playerBodyType == BodyType.Skinny)
            {
                health -= healthDecreaseRate;
                CheckIfPlayerDead();
                yield return new WaitForSeconds(1);
            }
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
        }
    }
}
