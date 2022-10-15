using UnityEngine;

namespace Framework.Custom
{
    public class CameraSmooth: MonoBehaviour
    {

        #region --------------------------------------- Fields ------------------------------------

        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float damping;   // The max distance between Camera and desired GameObject
        public Transform min;
        public Transform max;
        private Vector3 velocity = Vector3.zero;
        private Camera cam;
        #endregion --------------------------------------- Fields ------------------------------------

        #region --------------------------------------- Mono ------------------------------------

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            cam = transform.GetComponent<Camera>();
        }

        void FixedUpdate()
        {
            float camHeight = cam.orthographicSize;
            float camWidth = cam.orthographicSize * cam.aspect;

            Vector3 startPos = transform.position;
            Vector3 endPos = _player.transform.position + offset;
            endPos.x = Mathf.Clamp(endPos.x, min.transform.position.x, max.transform.position.x);
            endPos.y = Mathf.Clamp(endPos.y, min.transform.position.y , max.transform.transform.position.y);
            transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, damping);

           
        }

        #endregion --------------------------------------- Mono ------------------------------------

    }
}
