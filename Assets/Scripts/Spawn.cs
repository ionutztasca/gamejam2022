//Script by Ionut
//Version: 0.0.1
//What does this script do?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class Spawn: MonoBehaviour
    {
        [SerializeField] List<GameObject> prefabsList;
        [SerializeField] List<BoxCollider2D> colliders;

        Vector2 cubeSize;
        Vector2 cubeCenter;
        public Vector2 intervalSpawnTime = new Vector2(1f, 5f);
        public int maxItemsPerCollider = 10;
    

        private void Start()
        {
            StartCoroutine(SpawnRandomItem());
        }

        private IEnumerator SpawnRandomItem()
        {
            BoxCollider2D collider = GetRandomCollider();
            Transform cubeTrans = collider.GetComponent<Transform>();
            //cubeCenter = cubeTrans.position;
            cubeCenter = collider.bounds.center;
            // Multiply by scale because it does affect the size of the collider
            cubeSize.x = cubeTrans.localScale.x * collider.size.x;
            cubeSize.y = cubeTrans.localScale.y * collider.size.y;
            if (collider.transform.childCount <= maxItemsPerCollider)
            {
                GameObject go = Instantiate(GetRandomPrefab(), GetRandomPosition(), Quaternion.identity);
                go.transform.SetParent(collider.transform);
            }
            
            yield return new WaitForSeconds(GetSpawnTimeInterval());
            StartCoroutine(SpawnRandomItem());
        }

        private Vector2 GetRandomPosition()
        {
            // You can also take off half the bounds of the thing you want in the box, so it doesn't extend outside.
            // Right now, the center of the prefab could be right on the extents of the box
            Vector2 randomPosition = new Vector2(Random.Range(-cubeSize.x / 2, cubeSize.x / 2), Random.Range(-cubeSize.y / 2, cubeSize.y /2 ));

            return cubeCenter + randomPosition;
        }
     
       
        private GameObject GetRandomPrefab()
        {
            return prefabsList[Random.Range(0, prefabsList.Count)];
        }
        private BoxCollider2D GetRandomCollider()
        {
            return colliders[Random.Range(0, colliders.Count)];
        }
        private float GetSpawnTimeInterval()
        {
            return Random.Range(intervalSpawnTime.x, intervalSpawnTime.y);
        }
    }
}
