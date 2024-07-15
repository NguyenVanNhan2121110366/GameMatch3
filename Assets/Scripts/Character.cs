using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    #region Variable
    protected AllDotController Alldot;
    [SerializeField] private int countTurn;
    public Animator animator;
    [SerializeField] private int bloodScore;
    [SerializeField] private int powerScore;
    [SerializeField] private int attackScore;
    [SerializeField] private int shieldScore;
    [SerializeField] private int goldScore;

    [SerializeField] private float bloodCurrentScore;
    [SerializeField] private float powerCurrentScore;
    [SerializeField] private float shieldCurrentScore;
    [SerializeField] private float maxScoreBlood;
    [SerializeField] private float maxScoreShield;
    [SerializeField] private float maxScorePower;
    public Image bloodBar;
    public Image powerBar;
    public Image shieldBar;
    #endregion
    #region Public
    public int BloodScore { get => bloodScore; set => bloodScore = value; }
    public int PowerScore { get => powerScore; set => powerScore = value; }
    public int AttackScore { get => attackScore; set => attackScore = value; }
    public int ShieldScore { get => shieldScore; set => shieldScore = value; }
    public int GoldScore { get => goldScore; set => goldScore = value; }

    public float BloodCurrentScore { get => bloodCurrentScore; set => bloodCurrentScore = value; }
    public float PowerCurrentScore { get => powerCurrentScore; set => powerCurrentScore = value; }
    public float ShieldCurrentScore { get => shieldCurrentScore; set => shieldCurrentScore = value; }
    public float MaxScoreBlood { get => maxScoreBlood; set => maxScoreBlood = value; }
    public float MaxScoreShield { get => maxScoreShield; set => maxScoreShield = value; }
    public float MaxScorePower { get => maxScorePower; set => maxScorePower = value; }
    public int CountTurn { get => this.countTurn; set => countTurn = value; }

    #endregion
    protected virtual void UpdateFillBar()
    {
        this.bloodBar.fillAmount = Mathf.Lerp(this.bloodBar.fillAmount, this.bloodCurrentScore / this.maxScoreBlood, 15 * Time.deltaTime);
        this.powerBar.fillAmount = Mathf.Lerp(this.powerBar.fillAmount, this.powerCurrentScore / this.maxScorePower, 15 * Time.deltaTime);
        this.shieldBar.fillAmount = Mathf.Lerp(this.shieldBar.fillAmount, this.shieldCurrentScore / this.maxScoreShield, 15 * Time.deltaTime);

    }

    public void UpdateScoreBar()
    {
        this.bloodCurrentScore += this.bloodScore;
        this.powerCurrentScore += this.powerScore;
        this.shieldCurrentScore += this.shieldScore;

        this.bloodCurrentScore = this.bloodCurrentScore > this.maxScoreBlood ? this.maxScoreBlood : this.bloodCurrentScore;
        this.powerCurrentScore = this.powerCurrentScore > this.maxScorePower ? this.maxScorePower : this.powerCurrentScore;
        this.shieldCurrentScore = this.shieldCurrentScore > this.maxScoreShield ? this.maxScoreShield : this.shieldCurrentScore;
        this.bloodCurrentScore = this.bloodCurrentScore < 0 ? 0 : this.bloodCurrentScore;
        this.powerCurrentScore = this.powerCurrentScore < 0 ? 0 : this.powerCurrentScore;
        this.shieldCurrentScore = this.shieldCurrentScore < 0 ? 0 : this.shieldCurrentScore;
    }

    public void UpdateScore()
    {
        this.bloodScore = ScoreController.Instance.BloodScore;
        this.attackScore = ScoreController.Instance.AttackScore;
        this.powerScore = ScoreController.Instance.PowerScore;
        this.shieldScore = ScoreController.Instance.ShieldScore;
    }

    public virtual void Attacking()
    {

    }

}
