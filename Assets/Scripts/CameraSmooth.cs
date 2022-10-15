//Script by Ionut
//Version: 0.0.1
//What does this script do?

using UnityEngine;


namespace Framework.Custom
{
    ///<summary>
    /// What does this script do?
    ///</summary>

    public class CameraSmooth: MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float damping;

        private Vector3 velocity = Vector3.zero;

        void FixedUpdate()
        {
            Vector3 movepos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movepos, ref velocity, damping);
        }
    }
}
