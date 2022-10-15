using UnityEngine;

namespace Framework.Custom
{
    public class CameraSmooth: MonoBehaviour
    {

        #region --------------------------------------- Fields ------------------------------------

        public Transform target;
        public Vector3 offset;
        public float damping;   // The max distance between Camera and desired GameObject
        private Vector3 velocity = Vector3.zero;

        #endregion --------------------------------------- Fields ------------------------------------

        #region --------------------------------------- Mono ------------------------------------

        void FixedUpdate()
        {
            Vector3 movepos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movepos, ref velocity, damping);   // Camera follows desided GameObject
        }

        #endregion --------------------------------------- Mono ------------------------------------

    }
}
