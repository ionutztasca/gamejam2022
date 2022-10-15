//Script by Ionut
//Version: 0.0.1
//What does this script do?

using UnityEngine;


namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class FoodLevitate: MonoBehaviour
    {
        public float amplitude = 0.5f;
        public float frequency = 1f;

        Vector3 posOffset = new Vector3();
        Vector3 tempPos = new Vector3();

        void Start()
        {
            // Store the starting position & rotation of the object
            posOffset = transform.position;
        }
        private void Update()
        {
            

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }
}
