using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    private static Enemy _instance;
    public static Enemy Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<Enemy>();
            }
            return _instance;
        }
    }
    
    [SerializeField] private Vector2 posEnemy;
    [SerializeField] private Transform posAttackPlayer;
    [SerializeField] private Quaternion roEnemy;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isWaking;
    [SerializeField] private bool isBackToBase;
    [SerializeField] private bool isAttack;
    [SerializeField] private GameObject attackRate;
    
    private bool _isCheckAudio;
    public Player player;
    public bool IsWaking { get => isWaking; set => isWaking = value; }
    

    private void Awake()
    {
        this.player = FindFirstObjectByType<Player>();
        this.animator = GetComponent<Animator>();
        this.Alldot = FindFirstObjectByType<AllDotController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _isCheckAudio = false;
        this.CountTurn = 0;
        this.posEnemy = transform.position;
        this.roEnemy = transform.rotation;
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
        if (GameStateController.Instance.CurrentGameState == GameState.Attacking)
        {
            this.isIdle = false;
            if (this.isWaking)
            {
                transform.position = Vector2.Lerp(transform.position, this.posAttackPlayer.position, 15 * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, this.posAttackPlayer.position) < 0.1f)
                {
                    transform.position = this.posAttackPlayer.position;
                    this.isAttack = true;
                    this.isWaking = false;
                    _isCheckAudio = true;
                }
            }
            if (this.isAttack)
            {

                StartCoroutine(this.DelayAttack());
                this.CheckAudio();
            }
            if (this.isBackToBase)
            {
                transform.position = Vector2.Lerp(transform.position, this.posEnemy, 15 * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (Vector2.Distance(transform.position, this.posEnemy) < 0.1f)
                {
                    transform.position = this.posEnemy;
                    transform.rotation = this.roEnemy;
                    this.isBackToBase = false;
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                }
            }
        }
        else
        {
            this.SetState();
        }
        this.animator.SetBool("Idle", this.isIdle);
        this.animator.SetBool("Waking", this.isWaking);
        this.animator.SetBool("Attacking", this.isAttack);
        this.animator.SetBool("BackToBase", this.isBackToBase);
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
        yield return new WaitForSeconds(0.2f);
        this.attackRate.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        this.isBackToBase = true;
        this.isAttack = false;
        this.attackRate.SetActive(false);
    }

    private void SetState()
    {
        this.isIdle = true;
        this.isBackToBase = false;
        this.isAttack = false;
        this.isWaking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackRatePlayer"))
        {
            this.BloodCurrentScore -= this.player.AttackScore;
            this.animator.SetTrigger("TakeDame");
            if (this.BloodCurrentScore <= 0)
            {
                TurnController.Instance.gameOverObj.SetActive(true);
                TurnController.Instance.gameOver.text = "Enemy die ";
                this.animator.SetTrigger("Die");
                this.Alldot.IsCheckGameOver = true;

                //TurnController.instance.currentTurn = GameTurn.None;
            }
            //StartCoroutine(this.DelayTime());
        }
    }
    // private IEnumerator DelayTime()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     this.animator.SetBool("Idle", true);
    // }
}
