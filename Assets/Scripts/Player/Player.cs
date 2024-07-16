using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<Player>();
            }
            return _instance;
        }
    }
    #region Variable 
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isBackToBase;
    [SerializeField] private Transform posAttackEnemy;
    [SerializeField] private Vector2 posPlayer;
    [SerializeField] private Quaternion roPlayer;
    [SerializeField] private GameObject attackRate;

    private bool _isCheckAudio;
    private Enemy _enemy;
    #endregion
    #region Public
    public bool IsWalking { get => isWalking; set => isWalking = value; }
    #endregion

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this._enemy = FindFirstObjectByType<Enemy>();
        this.Alldot = FindFirstObjectByType<AllDotController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this._isCheckAudio = false;
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
        if (TurnController.Instance.currentTurn == GameTurn.Player && GameStateController.Instance.CurrentGameState == GameState.Attacking)
        {
            this.isIdle = false;
            if (this.isWalking && !this.isAttack && !this.isBackToBase)
            {
                transform.position = Vector2.Lerp(transform.position, this.posAttackEnemy.position, 15 * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, this.posAttackEnemy.position) <= 0.2f)
                {
                    transform.position = this.posAttackEnemy.position;
                    this.isWalking = false;
                    this.isAttack = true;
                    this._isCheckAudio = true;
                }
            }
            else if (this.isAttack && !this.isWalking && !this.isBackToBase)
            {
                this.attackRate.SetActive(true);

                StartCoroutine(this.DelayAttack());
                this.CheckAudio();

            }
            else if (this.isBackToBase && !this.isAttack && !this.isWalking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position = Vector2.Lerp(transform.position, this.posPlayer, 15 * Time.fixedDeltaTime);
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
    private void CheckAudio()
    {
        if (_isCheckAudio)
        {
            AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.AttackEffect, 0.3f);
            _isCheckAudio = false;
        }

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
                TurnController.Instance.gameOverObj.SetActive(true);
                TurnController.Instance.gameOver.text = "Player die :(";
                this.animator.SetTrigger("Die");
                this.Alldot.IsCheckGameOver = true;
                //TurnController.instance.currentTurn = GameTurn.None;
            }
        }
    }
}
