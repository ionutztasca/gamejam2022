using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class CameraSize : MonoBehaviour
{

    #region ------------------------------------------ Fields ------------------------------------

    // Player
    [SerializeField] private Transform _player;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _minCameraSize = 2.0f;
    [SerializeField] private bool _scalingCamera = false;

    #endregion --------------------------------------- Fields ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = this.gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if ((_camera.orthographicSize < (_player.transform.localScale.x + 1.0f - 0.2f)) && !_scalingCamera)
        { 
            StartCoroutine(ScaleUPCameraSize());
            _scalingCamera = true;
        }
        if ((_camera.orthographicSize > (_player.transform.localScale.x + 1.0f + 0.2f)) && !_scalingCamera)
        {
            StartCoroutine(ScaleDOWNCameraSize());
            _scalingCamera = true;
        }
    }

    #endregion --------------------------------------- Mono ------------------------------------

    #region ------------------------------------------ Mono ------------------------------------

    private IEnumerator ScaleUPCameraSize()
    {
        yield return new WaitForSecondsRealtime(0.001f);
        if (_camera.orthographicSize < (_player.transform.localScale.x + 1.0f))
        {
            _camera.orthographicSize += 0.001f;
            StartCoroutine(ScaleUPCameraSize());
        }
        if (_camera.orthographicSize <= (_player.transform.localScale.x + 1.0f + 0.5f) && _camera.orthographicSize >= (_player.transform.localScale.x + 1.0f - 0.5f))
        {
            _scalingCamera = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator ScaleDOWNCameraSize()
    {
        yield return new WaitForSecondsRealtime(0.001f);
        if (_camera.orthographicSize > (_player.transform.localScale.x + 1.0f))
        {
            _camera.orthographicSize -= 0.001f;
            StartCoroutine(ScaleDOWNCameraSize());
        }
        if (_camera.orthographicSize <= (_player.transform.localScale.x + 1.0f + 0.5f) && _camera.orthographicSize >= (_player.transform.localScale.x + 1.0f - 0.5f))
        {
            _scalingCamera = false;
            StopAllCoroutines();
        }
    }

    #endregion --------------------------------------- Mono ------------------------------------

}
