using Framework.Custom;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    #region --------------------------------------- Fields ------------------------------------

    [SerializeField] private GameObject _enemy, _player;
    private bool _escapingPlayer = false;

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
            //if(!_escapingPlayer) if(_player.transform.localScale.x > _enemy.transform.localScale.x && _player.transform.localScale.y > _enemy.transform.localScale.y)
            //    {
            //        _enemy.GetComponent<Enemy>()._escapePlayer = true;
            //        _escapingPlayer = true;
            //    }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if ((collision.gameObject.GetComponent("PlayerController") as PlayerController) != null)
        //{
        //    _enemy.GetComponent<Enemy>()._moveEnemy = false;
        //    if (_escapingPlayer) if (_player.transform.localScale.x < _enemy.transform.localScale.x && _player.transform.localScale.y < _enemy.transform.localScale.y)
        //        {
        //            _enemy.GetComponent<Enemy>()._escapePlayer = false;
        //            _escapingPlayer = false;
        //        }
        //}
    }

    #endregion ------------------------------------ Mono ------------------------------------

}
