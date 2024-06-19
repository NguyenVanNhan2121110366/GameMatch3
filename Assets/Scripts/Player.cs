using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region Variable 
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isBackToBase;
    [SerializeField] private Transform posAttackEnemy;
    [SerializeField] private Vector2 posPlayer;
    [SerializeField] private Quaternion roPlayer;
    [SerializeField] private GameObject attackRate;
    private Enemy _enemy;
    #endregion
    #region Public
    public bool IsWalking { get => isWalking; set => isWalking = value; }
    #endregion

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this._enemy = FindFirstObjectByType<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CountTurn = 1;
        this.posPlayer = transform.position;
        this.roPlayer = transform.rotation;
        this.BloodCurrentScore = this.MaxScoreBlood;
        this.PowerCurrentScore = 50f;
        this.ShieldCurrentScore = 50f;

    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateFillBar();
        //this.Attacking();
    }
    private void FixedUpdate()
    {
        this.Attacking();
    }

    public override void Attacking()
    {
        base.Attacking();
        if (TurnController.instance.currentTurn == GameTurn.Player && GameStateController.Instance.CurrentGameState == GameState.Attacking)
        {
            this.isIdle = false;
            if (this.isWalking && !this.isAttack && !this.isBackToBase)
            {
                transform.position = Vector2.Lerp(transform.position, this.posAttackEnemy.position, 3 * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, this.posAttackEnemy.position) <= 0.2f)
                {
                    transform.position = this.posAttackEnemy.position;
                    Debug.Log("Walking");
                    this.isWalking = false;
                    this.isAttack = true;
                }
            }
            else if (this.isAttack && !this.isWalking && !this.isBackToBase)
            {
                Debug.Log("Attacking");
                this.attackRate.SetActive(true);
                StartCoroutine(this.DelayAttack());

            }
            else if (this.isBackToBase && !this.isAttack && !this.isWalking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position = Vector2.Lerp(transform.position, this.posPlayer, 3 * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, this.posPlayer) < 0.1f)
                {
                    this.isBackToBase = false;
                    transform.rotation = this.roPlayer;
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                }
            }
        }
        else
        {
            this.isIdle = true;
            this.isBackToBase = false;
            this.isAttack = false;
            this.isWalking = false;
        }
        this.animator.SetBool("Idle", this.isIdle);
        this.animator.SetBool("Walking", this.isWalking);
        this.animator.SetBool("IsBackToBase", this.isBackToBase);
        this.animator.SetBool("Attacking", this.isAttack);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(1f);
        this.isAttack = false;
        this.isBackToBase = true;
        this.attackRate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackRateEnemy"))
        {
            this.animator.SetTrigger("TakeDame");
            this.BloodCurrentScore -= this._enemy.AttackScore;
            if (this.BloodCurrentScore <= 0)
            {
                Debug.Log("Player Die");
                this.animator.SetTrigger("Die");
                TurnController.instance.currentTurn = GameTurn.None;
            }
        }
    }
}
