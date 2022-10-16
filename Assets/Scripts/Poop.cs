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

    public class Poop: MonoBehaviour
    {
        public ParticleSystem explosionps;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (!enemy) return;
            explosionps.Play();
            Transform enemyTransform = enemy.transform;
            Vector2 direction = new Vector2((float)Random.Range(-1000, 1000), (float)Random.Range(-1000, 1000));

            float force = (float)Random.Range(-1000, 1000);
            enemyTransform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            enemyTransform.GetComponent<Rigidbody2D>().AddForce(direction * force);
            transform.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(DestroyEnemy(enemyTransform));
        }

        private IEnumerator DestroyEnemy(Transform enemmy)
        {
            yield return new WaitForSeconds(1f);
            Destroy(enemmy.gameObject);
            Destroy(transform.gameObject);
        }

    }
}
