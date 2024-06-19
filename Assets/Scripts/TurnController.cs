using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    private AllDotController _allDot;
    [SerializeField] private GameObject dotSprite;
    [SerializeField] private TextMeshProUGUI textTurn;
    [SerializeField] private TextMeshProUGUI textTurnOf;
    [SerializeField] private TextMeshProUGUI textStart;
    [SerializeField] private GameObject objStart;
    [SerializeField] private GameObject objCountEffect;
    [SerializeField] private TextMeshProUGUI textCountEffect;
    [SerializeField] private GameObject objPlayerFirst;
    [SerializeField] private TMP_Text txtPlayerFirst;
    [SerializeField] private int addTime;
    private bool _checkAttime;
    private static TurnController _instance;
    public static TurnController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<TurnController>();
            }
            return _instance;
        }
    }

    [SerializeField] private GameTurn _currentTurn;
    [SerializeField] private int countTurn;
    private Player _player;
    private Enemy _enemy;
    public GameTurn currentTurn { get => _currentTurn; set => _currentTurn = value; }
    public bool CheckAtTime { get => _checkAttime; set => _checkAttime = value; }
    private void Awake()
    {
        this._allDot = FindFirstObjectByType<AllDotController>();
        this._player = FindFirstObjectByType<Player>();
        this._enemy = FindFirstObjectByType<Enemy>();
        this.dotSprite.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.addTime = 0;
        this.CheckAtTime = true;
        this._currentTurn = GameTurn.None;
        this.countTurn = 1;
        this.UpdateTime(3);
        StartCoroutine(SettingStartGame());

    }

    private void Update()
    {
        this.textTurn.text = this.currentTurn.ToString() + " Turn";

    }

    private IEnumerator SettingStartGame()
    {

        while (this._checkAttime)
        {
            this.objCountEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            this.UpdateTime(-1);
            if (this.addTime <= 0)
            {
                this.objCountEffect.SetActive(false);
                yield return new WaitForSeconds(1f);
                this.objStart.SetActive(true);
                this.textStart.text = "Bắt đầu";
                yield return new WaitForSeconds(0.5f);
                this.objStart.SetActive(false);
                this.objPlayerFirst.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                this.txtPlayerFirst.text = "Player được chơi lượt đầu tiên !!!";
                this.objPlayerFirst.SetActive(false);
                yield return null;
                StartCoroutine(this._allDot.CreateBoard());
                break;
            }

        }



        Debug.Log("Start");
        yield return new WaitForSeconds(1f);
    }

    private void UpdateTime(int count)
    {
        this.addTime += count;
        this.textCountEffect.text = addTime.ToString();
    }

    // private IEnumerator CheckTurnText()
    // {

    //     if (this._player.CountTurn <= 0 || this._enemy.CountTurn <= 0)
    //     {
    //         if (this.currentTurn == GameTurn.Player)
    //             Debug.Log("Turn of Enemy");
    //         else
    //             Debug.Log("Turn of player");
    //     }
    //     else
    //     {
    //         if (this.currentTurn == GameTurn.Player)
    //             Debug.Log("Còn " + this._player.CountTurn.ToString() + " lượt");
    //         else
    //             Debug.Log("Còn " + this._enemy.CountTurn.ToString() + " lượt");
    //         //Debug.Log("Còn " + this.countTurn.ToString() + " lượt");
    //     }
    //     yield return new WaitForSeconds(1f);
    // }

    private IEnumerator CheckTurnText()
    {
        this.dotSprite.SetActive(true);
        if (countTurn <= 0)
        {
            if (this.currentTurn == GameTurn.Player)
            {
                this.textTurnOf.text = "Lượt của Enemy";
                Debug.Log("Turn of Enemy");
            }

            else
            {
                this.textTurnOf.text = "Lượt của Player";
                Debug.Log("Turn of player");
            }
        }
        else
        {
            Debug.Log("Còn " + countTurn.ToString() + " lượt");
        }

        yield return new WaitForSeconds(1f);
        this.dotSprite.SetActive(false);
    }


    // public void CheckSwitchTurn()
    // {
    //     if (currentTurn == GameTurn.Player)
    //     {
    //         this._player.CountTurn--;
    //         if (this._player.CountTurn <= 0)
    //         {
    //             StartCoroutine(CheckTurnText());
    //             this._enemy.CountTurn = 1;
    //             currentTurn = GameTurn.Enemy;
    //             this.SetNewTurn();
    //         }
    //     }
    //     else if (currentTurn == GameTurn.Enemy)
    //     {
    //         this._enemy.CountTurn--;
    //         if (this._enemy.CountTurn <= 0)
    //         {
    //             StartCoroutine(CheckTurnText());
    //             this._player.CountTurn = 1;
    //             currentTurn = GameTurn.Player;
    //         }
    //     }
    // }

    public void CheckSwitchTurn()
    {
        this.countTurn--;
        if (countTurn <= 0)
        {
            StartCoroutine(this.CheckTurnText());
            this.countTurn = 1;
            this.SwitchTurn();
            this.SetNewTurn();
        }
        else
        {
            StartCoroutine(this.CheckTurnText());
            this.SetNewTurn();
        }
    }


    private IEnumerator DelayCountTurn()
    {
        yield return new WaitForSeconds(0.5f);
        this.countTurn = 1;
    }

    private void SetNewTurn()
    {
        //this.dotSprite.SetActive(false);
        FindFirstObjectByType<EnemyAi>().TurnOnAutoFind();
    }

    private void SwitchTurn()
    {
        if (currentTurn == GameTurn.Player)
            currentTurn = GameTurn.Enemy;
        else
            currentTurn = GameTurn.Player;

    }
}
public enum GameTurn
{
    None,
    Player,
    Enemy,
}
