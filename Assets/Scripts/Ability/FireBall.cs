using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject _enemyObj;
    private AllDotController _allDots;
    private Animator _animator;
    private Enemy _enemy;
    private bool _isAttackEnemy;
    private bool _isAttackDot;
    public GameObject _targetDot;
    [SerializeField] private int isDamage;
    [SerializeField] private int randomCol, randomRow;

    public bool IsAttackEnemy { get => _isAttackEnemy; set => _isAttackEnemy = value; }
    public bool IsAttackDot { get => _isAttackDot; set => _isAttackDot = value; }

    private void Awake()
    {
        this._allDots = FindFirstObjectByType<AllDotController>();
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
        if (TurnController.Instance.currentTurn == GameTurn.Player)
        {
            this.FireBallAttackEnemy();
            this.AttackDot();

        }
    }

    private void FireBallAttackEnemy()
    {
        if (_isAttackEnemy)
        {
            if (!this._enemyObj)
            {
                this._enemyObj = GameObject.Find("Skeleton");
                transform.rotation = this.GetValueRotationtoEnemy(this._enemyObj.transform.position);
            }
            if (Vector2.Distance(transform.position, this._enemyObj.transform.position) < 1.5)
            {
                this._animator.SetTrigger("Exploi");
                Destroy(gameObject, 0.3f);
                

                if (this._isAttackEnemy)
                {
                    this._isAttackEnemy = false;
                    this.TakeDameEnemy();
                    Enemy.Instance.animator.SetTrigger("TakeDame");
                    AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.FireBallEffect, 0.3f);
                    // this._enemy.BloodCurrentScore -= this.isDamage;
                    //GameStateController.Instance.CurrentGameState = GameState.Finish;
                    //TurnController.instance.currentTurn = GameTurn.Enemy;
                }
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, this._enemy.transform.position, 25 * Time.deltaTime);
        }
    }
    private void AttackDot()
    {
        if (_isAttackDot)
        {
            if (!this._targetDot)
            {
                this._targetDot = this.RandomDot();
                transform.rotation = this.GetValueRotationtoEnemy(this._targetDot.transform.position);
                return;
            }
            if (Vector2.Distance(transform.position, _targetDot.transform.position) < 0.1f)
            {
                Destroy(gameObject, 0.6f);
                this._animator.SetTrigger("Exploi");
                if (this._isAttackDot)
                {
                    StartCoroutine(this.OnDestroyDot());
                    this._isAttackDot = false;
                }
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, this._targetDot.transform.position, 25 * Time.deltaTime);
        }
    }

    private IEnumerator OnDestroyDot()
    {
        yield return null;
        for (var i = -1; i <= 2; i++)
        {
            for (var j = -1; j <= 2; j++)
            {
                if (this._allDots.AllDots[this.randomCol + i, this.randomRow + j])
                {
                    Destroy(this._allDots.AllDots[this.randomCol + i, this.randomRow + j]);
                    this._allDots.AllDots[this.randomCol + i, this.randomRow + j] = null;
                }

            }
        }
        GameStateController.Instance.CurrentGameState = GameState.FillingDots;
        AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.FireBallEffect, 0.3f);
        StartCoroutine(this._allDots.DestroyMatched());
    }

    private GameObject RandomDot()
    {
        this.randomCol = Random.Range(2, this._allDots.Width - 2);
        this.randomRow = Random.Range(2, this._allDots.Height - 2);
        var allRandomDot = this._allDots.AllDots[this.randomCol, this.randomRow];
        return allRandomDot;
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
        if (Enemy.Instance.BloodCurrentScore >= this.isDamage)
        {
            
            Enemy.Instance.BloodCurrentScore -= this.isDamage;
        }
    }
}
