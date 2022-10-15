using Framework.Custom;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    #region --------------------------------------- Fields ------------------------------------

    [SerializeField] private GameObject _enemy, _player;
    private bool _isEscaping = false;

    #endregion ------------------------------------ Fields ------------------------------------

    #region --------------------------------------- Mono ------------------------------------

    private void Awake()
    {
        _enemy = this.transform.parent.gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent("PlayerController") as PlayerController) != null)
        {
            _enemy.GetComponent<Enemy>()._moveEnemy = true;
            if (!_isEscaping)
                if (_player.gameObject.transform.localScale.x > _enemy.gameObject.transform.localScale.x && _player.gameObject.transform.localScale.y > _enemy.gameObject.transform.localScale.y)
                {
                    _enemy.GetComponent<Enemy>()._escapePlayer = true;
                    _isEscaping = true;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent("PlayerController") as PlayerController) != null)
        {
            _enemy.GetComponent<Enemy>()._moveEnemy = false;
        }
    }

    #endregion ------------------------------------ Mono ------------------------------------

}
