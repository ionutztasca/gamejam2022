using UnityEngine;

namespace Framework.Custom
{
    public class CameraSmooth: MonoBehaviour
    {

        #region --------------------------------------- Fields ------------------------------------

        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float damping;   // The max distance between Camera and desired GameObject
        private Vector3 velocity = Vector3.zero;

        #endregion --------------------------------------- Fields ------------------------------------

        #region --------------------------------------- Mono ------------------------------------

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            Vector3 movepos = _player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movepos, ref velocity, damping);   // Camera follows desided GameObject
        }

        #endregion --------------------------------------- Mono ------------------------------------

    }
}
