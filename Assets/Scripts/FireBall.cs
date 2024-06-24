using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject _enemyObj;
    private Animator _animator;
    private Enemy _enemy;
    private bool _isAttackEnemy;
    [SerializeField] private int isDamage;

    public bool isAttackEnemy { get => _isAttackEnemy; set => _isAttackEnemy = value; }

    private void Awake()
    {
        this._enemy = FindFirstObjectByType<Enemy>();
        this._animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //this._isAttackEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnController.instance.currentTurn == GameTurn.Player)
        {
            this.FireBallAttackEnemy();
        }
    }

    private void FireBallAttackEnemy()
    {
        if (this._enemy == null)
        {
            this._enemyObj = GameObject.Find("Skeleton");
            transform.rotation = this.GetValueRotationtoEnemy(this._enemy.transform.position);
        }
        if (Vector2.Distance(transform.position, this._enemy.transform.position) < 1)
        {
            Destroy(gameObject, 0.3f);
            this._animator.SetTrigger("Exploi");
            if (this._isAttackEnemy)
            {
                this._enemy.animator.SetTrigger("TakeDame");
                this.TakeDameEnemy();
                // this._enemy.BloodCurrentScore -= this.isDamage;
                this._isAttackEnemy = false;
                Debug.Log("Attack enemy");
            }
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, this._enemy.transform.position, 3 * Time.deltaTime);
    }

    private Quaternion GetValueRotationtoEnemy(Vector3 target)
    {
        var postiontarget = target - transform.position;
        var angle = Mathf.Atan2(postiontarget.y, postiontarget.x) * Mathf.Rad2Deg;
        var ro = Quaternion.Euler(0, 0, angle);
        return ro;
    }

    private void TakeDameEnemy()
    {
        if (Enemy.instance.BloodCurrentScore >= this.isDamage)
        {
            Enemy.instance.BloodCurrentScore -= this.isDamage;
        }
    }
}
