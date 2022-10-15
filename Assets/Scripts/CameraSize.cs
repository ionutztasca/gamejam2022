using UnityEngine;

public class CameraSize : MonoBehaviour
{

    #region ------------------------------------------ Fields ------------------------------------

    // Player
    [SerializeField] private Transform _player;
    private float _playerInitialScaleX;


    [SerializeField] private Camera _camera;
    [SerializeField] private float _minCameraSize = 2.5f;

    #endregion --------------------------------------- Fields ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = this.gameObject.GetComponent<Camera>();

        _playerInitialScaleX = _player.transform.localScale.x;
    }

    private void Update()
    {
        ScaleCameraSize();
    }

    #endregion --------------------------------------- Mono ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private void ScaleCameraSize()
    {
        _camera.orthographicSize = _minCameraSize + (_player.transform.localScale.x - _playerInitialScaleX);
    }

    #endregion --------------------------------------- Mono ------------------------------------

}
