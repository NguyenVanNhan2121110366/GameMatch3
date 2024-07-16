using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    private static ScoreController _instance;
    public static ScoreController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<ScoreController>();
            return _instance;
        }
    }
    #region Variable
    [SerializeField] private int bloodScore;
    [SerializeField] private int powerScore;
    [SerializeField] private int attackScore;
    [SerializeField] private int shieldScore;
    [SerializeField] private int goldScore;
    private Player _player;
    private Enemy _enemy;
    #endregion
    #region Public
    public int BloodScore { get => bloodScore; set => bloodScore = value; }
    public int PowerScore { get => powerScore; set => powerScore = value; }
    public int AttackScore { get => attackScore; set => attackScore = value; }
    public int ShieldScore { get => shieldScore; set => shieldScore = value; }
    public int GoldScore { get => goldScore; set => goldScore = value; }
    #endregion

    private void Start()
    {
        this._player = FindFirstObjectByType<Player>();
        this._enemy = FindFirstObjectByType<Enemy>();
    }

    public void UpdateScore()
    {
        if (TurnController.Instance.currentTurn == GameTurn.Player)
        {
            this._player.UpdateScore();
            this._player.UpdateScoreBar();
            if (this._player.AttackScore > 0)
            {
                this._player.IsWalking = true;
                GameStateController.Instance.CurrentGameState = GameState.Attacking;
            }
        }
        if (TurnController.Instance.currentTurn == GameTurn.Enemy)
        {
            this._enemy.UpdateScore();
            this._enemy.UpdateScoreBar();
            if (this._enemy.AttackScore > 0)
            {
                this._enemy.IsWaking = true;
                GameStateController.Instance.CurrentGameState = GameState.Attacking;
            }
        }
    }
    public void ResetScore()
    {
        this.goldScore = 0;
        this.bloodScore = 0;
        this.attackScore = 0;
        this.powerScore = 0;
        this.shieldScore = 0;
    }




}
