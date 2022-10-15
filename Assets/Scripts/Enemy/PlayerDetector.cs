using Framework.Custom;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    #region --------------------------------------- Fields ------------------------------------

    [SerializeField] private GameObject _enemy;

    #endregion ------------------------------------ Fields ------------------------------------

    #region --------------------------------------- Mono ------------------------------------

    private void Awake()
    {
        _enemy = this.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent("PlayerController") as PlayerController) != null)
        {
            _enemy.GetComponent<Enemy>()._moveToPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent("PlayerController") as PlayerController) != null)
        {
            _enemy.GetComponent<Enemy>()._moveToPlayer = false;
        }
    }

    #endregion ------------------------------------ Mono ------------------------------------
}
