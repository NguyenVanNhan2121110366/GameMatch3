using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDotController : MonoBehaviour
{
    #region Vaiable
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Transform objPrefab;
    [SerializeField] private GameObject girdPrefab;
    [SerializeField] private bool isCheckGameOver;
    private GameObject[,] allDots;
    private GameObject[,] allGrids;
    public GameObject[] dots = new GameObject[9];
    public DotInteraction _dotInteraction;
    [SerializeField] private GameObject[] allEffectsDestroyObj;
    #endregion

    #region Public
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public GameObject[,] AllDots { get => allDots; set => allDots = value; }
    public GameObject[,] AllGrids { get => allGrids; set => allGrids = value; }
    public bool IsCheckGameOver { get => isCheckGameOver; set => isCheckGameOver = value; }
    #endregion

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        this.isCheckGameOver = false;
        this.allDots = new GameObject[this.width, this.height];
        this.allGrids = new GameObject[this.width, this.height];
        this.AddAllDotsToList();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddAllDotsToList()
    {
        for (var i = 0; i < objPrefab.childCount; i++)
        {
            this.dots[i] = this.objPrefab.GetChild(i).gameObject;
        }
    }

    public IEnumerator CreateBoard()
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < this.width; i++)
        {
            for (var j = 0; j < this.height; j++)
            {
                var pos = new Vector2(i, j);
                var grid = Instantiate(this.girdPrefab, pos, Quaternion.identity);
                grid.transform.parent = transform;
                grid.name = "(" + i + " , " + j + ")";
                this.allGrids[i, j] = grid;
            }
        }
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < this.width; i++)
        {
            yield return null;
            for (var j = 0; j < this.height; j++)
            {
                yield return null;
                for (; ; )
                {
                    var dotToUse = this.DotToUse();
                    var dotTag = this.dots[dotToUse].tag;
                    var dotLeftTag = i - 1 >= 0 ? this.allDots[i - 1, j].tag : string.Empty;
                    var dotDowTag = j - 1 >= 0 ? this.allDots[i, j - 1].tag : string.Empty;
                    if (dotTag == dotLeftTag || dotTag == dotDowTag)
                        continue;
                    else
                    {
                        var pos = this.allGrids[i, j].transform.position;
                        var dot = Instantiate(this.dots[dotToUse], pos, Quaternion.identity);
                        dot.transform.parent = transform;
                        dot.name = "(" + i + " , " + j + ")";
                        dot.GetComponent<DotInteraction>().Column = i;
                        dot.GetComponent<DotInteraction>().Row = j;
                        dot.SetActive(true);
                        this.allDots[i, j] = dot;
                        break;
                    }
                }
            }
        }
        StartCoroutine(DestroyMatched());
    }

    #region Destroy Matched
    private void DestroyMatchedAt(int col, int ro)
    {
        if (this.AllDots[col, ro].GetComponent<DotInteraction>().IsMatched)
        {
            AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.DestroyEffect, 0.1f);
            Destroy(this.AllDots[col, ro]);
            this.AllDots[col, ro] = null;

        }
    }

    public IEnumerator DestroyMatched()
    {
        yield return new WaitForSeconds(0.3f);
        for (var i = 0; i < this.Width; i++)
            for (var j = 0; j < this.Height; j++)
            {
                if (this.AllDots[i, j] != null)
                {
                    this.DestroyMatchedAt(i, j);

                }
            }
        StartCoroutine(this.ColumnFallBellow());
    }

    public IEnumerator ColumnFallBellow()
    {
        yield return new WaitForSeconds(0.3f);
        var indexRow = 0;
        for (var i = 0; i < this.Width; i++)
        {
            for (var j = 0; j < this.Height; j++)
            {
                if (this.AllDots[i, j] == null)
                    indexRow++;
                else if (indexRow > 0)
                {
                    this.AllDots[i, j].GetComponent<DotInteraction>().Row -= indexRow;
                    this.AllDots[i, j] = null;
                }
            }
            indexRow = 0;
        }
        yield return null;
        StartCoroutine(this.RefillBoard());

    }

    private IEnumerator SpawnNull()
    {
        yield return new WaitForSeconds(0.05f);
        for (var i = 0; i < this.Width; i++)
        {
            for (var j = 0; j < this.Height; j++)
            {
                if (this.AllDots[i, j] == null)
                {
                    var pos = new Vector2(i, j + 2);
                    var dot = this.DotToUse();
                    var dotObj = Instantiate(this.dots[dot], pos, Quaternion.identity);
                    dotObj.transform.parent = transform;
                    dotObj.name = "(" + i + " , " + j + ")";
                    dotObj.SetActive(true);
                    dotObj.GetComponent<DotInteraction>().Row = j;
                    dotObj.GetComponent<DotInteraction>().Column = i;
                    this.AllDots[i, j] = dotObj;

                }
            }
        }
    }

    private bool MatchedOnBoard()
    {
        for (var i = 0; i < this.Width; i++)
        {
            for (var j = 0; j < this.Height; j++)
            {
                if (this.allDots[i, j] != null)
                    if (this.allDots[i, j].GetComponent<DotInteraction>().IsMatched)
                        return true;
            }
        }
        return false;
    }

    private IEnumerator RefillBoard()
    {
        StartCoroutine(this.SpawnNull());
        yield return new WaitForSeconds(0.5f);
        if (MatchedOnBoard())
        {
            StartCoroutine(this.DestroyMatched());
        }
        else
        {
            if (GameStateController.Instance.CurrentGameState == GameState.None)
            {
                GameStateController.Instance.CurrentGameState = GameState.Swipe;
                TurnController.Instance.currentTurn = GameTurn.Player;
            }
            if (GameStateController.Instance.CurrentGameState == GameState.FillingDots)
            {
                ScoreController.Instance.UpdateScore();
                if (GameStateController.Instance.CurrentGameState == GameState.FillingDots)
                {
                    GameStateController.Instance.CurrentGameState = GameState.Finish;
                }
            }
        }
    }
    #endregion

    #region Destroy Effect

    public void SpawnDestroyVFX(Dot dot)
    {
        var effectDot = Instantiate(this.allEffectsDestroyObj[GetEffects(dot.gameObject)], dot.transform.position, Quaternion.identity);
        Destroy(effectDot, 2f);
    }
    private int GetEffects(GameObject dot)
    {
        var index = 0;
        switch (dot.tag)
        {
            case "Gold":
                index = 4;
                break;

            case "Heart":
                index = 2;
                break;
            case "Sword":
                index = 3;
                break;
            case "Shield":
                index = 1;
                break;
            case "Mana":
                index = 0;
                break;
            default:
                break;
        }
        return index;
    }
    #endregion

    public int DotToUse()
    {
        var randomNumber = Random.Range(0, 100);
        var indexNumber = 0;
        if (randomNumber >= 0 && randomNumber < 19)
            indexNumber = 0;
        else if (randomNumber >= 19 && randomNumber < 38)
            indexNumber = 1;
        else if (randomNumber >= 38 && randomNumber < 59)
            indexNumber = 2;
        else if (randomNumber >= 59 && randomNumber < 78)
            indexNumber = 3;
        else if (randomNumber >= 78 && randomNumber < 97)
            indexNumber = 4;
        else if (randomNumber >= 97 && randomNumber < 98)
            indexNumber = 5;
        else if (randomNumber >= 98 && randomNumber < 99)
            indexNumber = 6;
        else if (randomNumber >= 99 && randomNumber < 100)
            indexNumber = 7;
        else
            indexNumber = 8;
        return indexNumber;
    }


}
