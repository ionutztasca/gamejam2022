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

    public class MissionsManager : MonoBehaviour
    {
        public UIManager uIManager;
        public CircleCollider2D colliderPot;
        public ParticleSystem splashParticle;

        private PlayerStats playerStats;


        private void OnCollisionStay2D(Collision2D collision)
        {
            playerStats = collision.transform.GetComponent<PlayerStats>();
            CheckIfPlayerHasQuestItem();
        }

        private void CheckIfPlayerHasQuestItem()
        {
            if (!playerStats) return;
            if (playerStats.questFood != null)
            {
                StartCoroutine(WaitAndMove(0.1f, playerStats.questFood));
            }
        }

        IEnumerator WaitAndMove(float delayTime, Transform target)
        {
            yield return new WaitForSeconds(delayTime); // start at time X
            float startTime = Time.time; // Time.time contains current frame time, so remember starting point
            while (Time.time - startTime <= 1)
            { // until one second passed
                target.transform.position = Vector3.Lerp(target.position, transform.position, Time.time - startTime); // lerp from A to B in one second
                yield return 1; // wait for next frame
            }
            AfterQuestItemReceived();
            StopAllCoroutines();
        }
        private void AfterQuestItemReceived()
        {
            Debug.Log("Quest Item received");
            splashParticle.Play();
            Destroy(playerStats.questFood.gameObject);
            playerStats.questFood = null;
        }
    }
}
