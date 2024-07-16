using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{

    private static TurnController _instance;
    public static TurnController Instance
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
    public GameObject menu;
    public GameObject gameOverObj;
    public TMP_Text gameOver;
    private bool _checkAttime;


    [SerializeField] private GameTurn _currentTurn;
    [SerializeField] private int countTurn;
    private Player _player;
    private Enemy _enemy;
    public GameTurn currentTurn { get => _currentTurn; set => _currentTurn = value; }
    public bool CheckAtTime { get => _checkAttime; set => _checkAttime = value; }
    public int CountTurn { get => countTurn; set => countTurn = value; }
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
        this.menu.SetActive(false);
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
        yield return new WaitForSeconds(1f);
    }

    private void UpdateTime(int count)
    {
        this.addTime += count;
        this.textCountEffect.text = addTime.ToString();
    }

    private IEnumerator CheckTurnText()
    {
        this.dotSprite.SetActive(true);
        if (countTurn <= 0)
        {
            if (this.currentTurn == GameTurn.Player)
            {
                this.textTurnOf.text = "Lượt của Enemy";
            }
            else if (this.currentTurn == GameTurn.Enemy)
            {
                this.textTurnOf.text = "Lượt của Player";
            }
        }
        else
        {
            Debug.Log("Còn " + countTurn.ToString() + " lượt");
        }

        yield return new WaitForSeconds(1f);
        this.dotSprite.SetActive(false);
    }


    public IEnumerator CheckSwitchTurn()
    {
        if (!this._allDot.IsCheckGameOver)
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
        else
        {
            yield return new WaitForSeconds(0.3f);
            this.gameOverObj.SetActive(false);
            yield return null;
            this.menu.SetActive(true);
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
    public IEnumerator DisplayGetTurn()
    {
        yield return new WaitForSeconds(1f);
    }
}
public enum GameTurn
{
    None,
    Player,
    Enemy,
}
