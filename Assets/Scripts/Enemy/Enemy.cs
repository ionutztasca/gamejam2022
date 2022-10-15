using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region --------------------------------------- Fields ------------------------------------

    // Fields
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _direction;
    private Rigidbody2D _enemyRigidbody;

    // Stats
    public int health;
    [SerializeField] private float _movmentSpeed = 1f;
    public bool _moveEnemy = false, _escapePlayer = false;


    #endregion ------------------------------------ Fields ------------------------------------

    #region --------------------------------------- Mono ------------------------------------

    private void Awake()
    {
        _enemyRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_moveEnemy)
            if (!_escapePlayer) MoveEnemyToPlayer();
            else MoveEnemyEscapePlayer();
    }

    #endregion ------------------------------------ Mono ------------------------------------

    #region --------------------------------------- Methods ------------------------------------

    public void MoveEnemyToPlayer()
    {
        _direction = _player.position - this.transform.position;   // Enemy to player direction / escape player
        _enemyRigidbody.MovePosition((Vector2)this.transform.position + (_direction * _movmentSpeed * Time.deltaTime));
        SetEnemyDirection();
    }
    public void MoveEnemyEscapePlayer()
    {
        _direction = -(_player.position - this.transform.position);   // Enemy to player direction / escape player
        _enemyRigidbody.MovePosition((Vector2)this.transform.position + (_direction * _movmentSpeed * Time.deltaTime));
        SetEnemyDirection();
    }

    private void SetEnemyDirection()   // Enemy Body Rotation
    {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;
        _enemyRigidbody.rotation = angle;
    }

    #endregion ------------------------------------ Methods ------------------------------------

}
