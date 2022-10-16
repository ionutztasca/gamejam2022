using Framework.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region --------------------------------------- Fields ------------------------------------

    #region --------------------------------------- GameObject ------------------------------------

    // Fields
    [SerializeField] private Transform _player;
    [SerializeField] private Vector2 _direction;
    private Rigidbody2D _enemyRigidbody;
    #endregion --------------------------------------- GameObject ------------------------------------

    #region --------------------------------------- Stats ------------------------------------

    // Stats
    public int health = 100, damage = 10;
    [SerializeField] public float _movementSpeed = 1f, _nextHitTime = 0.25f;
    private bool canHit = true;

    #endregion ------------------------------------ Stats ------------------------------------

    #region --------------------------------------- Movement ---------------------------------

    public bool _moveEnemy = false, _escapePlayer = false;

    // Randome Movement
    private Vector3 _targetPosition;
    private bool _targetPositionXReached = true, _targetPositionYReached = true;
    [SerializeField] private float _nextMovementTime = 2.0f;
    [SerializeField] private float _nextTargetDistance = 3.0f, _errorMargin = 0.5f;

    #endregion ----------------------------------- Movement ----------------------------------

    // Animations
    private Animator _enemyAnimator;

    // Enemy Sounds
    public List<AudioSource> enemyAudioSource;

    #endregion ------------------------------------ Fields ------------------------------------

    #region --------------------------------------- Mono ------------------------------------

    private void Awake()
    {
        _enemyRigidbody = this.gameObject.GetComponent<Rigidbody2D>();

        _enemyAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        damage = TimerGlobal.lastMaxDMG;
        _movementSpeed = TimerGlobal.lastMaxSpeed;
        StartCoroutine(RandomMovement());
    }

    private void FixedUpdate()
    {
        // Enemy/Player Movement
        if (_moveEnemy)
            if (!_escapePlayer) MoveEnemyToPlayer();
            //else MoveEnemyEscapePlayer();
        else _enemyAnimator.SetTrigger("idle");
        _enemyAnimator.speed = 1f;

        // Enemy Random Movement
        if ((!_targetPositionXReached || !_targetPositionYReached) && !_moveEnemy)
        {
            _direction = _targetPosition - this.transform.position;
            MoveToTargetPosition();
            SetEnemyDirection();
        }
    }

    #endregion ------------------------------------ Mono ------------------------------------

    #region --------------------------------------- Methods ------------------------------------

    #region --------------------------------------- Enemy/Player Movement ------------------------------------

    public void MoveEnemyToPlayer()
    {
        _direction = _player.position - this.transform.position;   // Enemy to player direction
        _enemyRigidbody.MovePosition((Vector2)this.transform.position + (_direction * _movementSpeed * Time.deltaTime));
        SetEnemyDirection();
        _enemyAnimator.SetTrigger("run");
        _enemyAnimator.speed = 1.5f;
    }

    public void MoveEnemyEscapePlayer()
    {
        _direction = -(_player.position - this.transform.position);   // Enemy to player direction
        _enemyRigidbody.MovePosition((Vector2)this.transform.position + (_direction * _movementSpeed * Time.deltaTime));
        SetEnemyDirection();
    }

    private void SetEnemyDirection()   // Enemy Body Rotation
    {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;
        _enemyRigidbody.rotation = angle;
    }

    #endregion --------------------------------------- Enemy/Player Movement ------------------------------------

    #region --------------------------------------- Random Movement ---------------------------------

    private IEnumerator RandomMovement()
    {
        if (_targetPositionXReached && _targetPositionYReached)
        {
            PlayEnemySound();
            _targetPosition = new Vector3(this.transform.position.x + Random.Range(-_nextTargetDistance, _nextTargetDistance), this.transform.position.y + Random.Range(-_nextTargetDistance, _nextTargetDistance), 0);
            _direction = _targetPosition - this.transform.position;
            _targetPositionXReached = false;
            _targetPositionYReached = false;
        }
        yield return new WaitForSecondsRealtime(_nextMovementTime);
        StartCoroutine(RandomMovement());
    }

    private void MoveToTargetPosition()   // Do not look --> spaghetti code sorry, not my tipe :)))))
    {
        // X movement
        if ((this.transform.position.x < _targetPosition.x - _errorMargin/2) && !_targetPositionXReached)
        {
            this.transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, 0);
        }
        else if ((this.transform.position.x > _targetPosition.x + _errorMargin / 2) && !_targetPositionXReached) this.transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, 0);
        else if (this.transform.position.x <= _targetPosition.x + _errorMargin && this.transform.position.x >= _targetPosition.x - _errorMargin) _targetPositionXReached = true;

        // Y movement
        if ((this.transform.position.y < _targetPosition.y - _errorMargin / 2) && !_targetPositionYReached)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, 0);
        }
        else if ((this.transform.position.y > _targetPosition.y + _errorMargin / 2) && !_targetPositionYReached) this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, 0);
        else if (this.transform.position.y <= _targetPosition.y + _errorMargin && this.transform.position.y >= _targetPosition.y - _errorMargin) _targetPositionYReached = true;
    }

    #endregion ------------------------------------ Random Movement ---------------------------------

    #region --------------------------------------- Hit ------------------------------------

    public void HitPlayer()   // Need ATTACK ANIMATION
    {
        if (canHit)
        {
            canHit = false;

            _enemyAnimator.speed = 2f;
            _enemyAnimator.SetTrigger("attack");
            Debug.Log("Dealt damage:" + damage + " to: " + _player);
            _player.GetComponent<PlayerStats>().TakeDamageFromEnemy(damage);
            StartCoroutine(WaitAttackAnim());
            PlayEnemySound();
        }
    }

    private IEnumerator WaitAttackAnim()
    {
        yield return new WaitForSecondsRealtime(_nextHitTime);
        _enemyAnimator.speed = 1f;
        canHit = true;
        _enemyAnimator.SetTrigger("run");

    }

    #endregion ------------------------------------ Hit ------------------------------------

    public void PlayEnemySound()
    {
        enemyAudioSource[Random.Range(0, enemyAudioSource.Count)].Play();   // 3 random sounds to play
    }

    #endregion ------------------------------------ Methods ------------------------------------

}
