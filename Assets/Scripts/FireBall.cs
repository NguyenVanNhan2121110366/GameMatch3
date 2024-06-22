using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject _enemyObj;
    private Animator _animator;
    private Enemy _enemy;
    private bool isAttackEnemy;
    [SerializeField] private int isDamage;

    private void Awake()
    {
        this._enemy = FindFirstObjectByType<Enemy>();
        this._animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.isAttackEnemy = false;
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
        else if (Vector2.Distance(transform.position, this._enemy.transform.position) < 0.1)
        {
            Destroy(gameObject, 3f);
            this._animator.SetTrigger("Exploi");
            if (this.isAttackEnemy)
            {
                this._enemy.animator.SetTrigger("TakeDame");
                this._enemy.BloodCurrentScore -= this.isDamage;
                this.isAttackEnemy = false;
            }
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, this._enemy.transform.position, 8 * Time.deltaTime);
    }

    private Quaternion GetValueRotationtoEnemy(Vector3 target)
    {
        var postiontarget = target - transform.position;
        var angle = Mathf.Atan2(postiontarget.y, postiontarget.x) * Mathf.Rad2Deg;
        var ro = Quaternion.Euler(0, angle, 0);
        return ro;
    }
}
