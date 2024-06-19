using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DotInteraction : MonoBehaviour
{
    #region Variable
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private Vector2 mouseUp, mouseDow;
    [SerializeField] private GameObject targetDot;
    [SerializeField] private bool isMatched;
    [SerializeField] private int preCol, preRow;
    [SerializeField] private int targetX, targetY;
    [SerializeField] private GameObject currentDot;
    private AllDotController _alldots;
    #endregion

    #region Public
    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public bool IsMatched { get => isMatched; set => isMatched = value; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        this._alldots = FindFirstObjectByType<AllDotController>();
        this.isMatched = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.currentDot = _alldots.AllDots[column, row];
        if (!Input.GetMouseButton(0) && GameStateController.Instance.CurrentGameState == GameState.Swipe)
            transform.localScale = Vector3.one;
        this.targetX = column;
        this.targetY = row;
        this.FindMatched();
        this.MoveDot();

    }

    private void MoveDot()
    {
        if (Mathf.Abs(targetX - transform.position.x) > 0.1f)
        {
            var pos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, pos, 9 * Time.deltaTime);
            if (this._alldots.AllDots[column, row] != gameObject)
                this._alldots.AllDots[column, row] = gameObject;
        }
        else
        {
            var pos = new Vector2(targetX, transform.position.y);
            transform.position = pos;
        }

        if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
        {
            var pos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, pos, 9 * Time.deltaTime);
            if (this._alldots.AllDots[column, row] != gameObject)
                this._alldots.AllDots[column, row] = gameObject;
        }
        else
        {
            var pos = new Vector2(transform.position.x, targetY);
            transform.position = pos;
        }

    }

    private IEnumerator CheckMatched()
    {
        if (this.targetDot == null)
        {
            GameStateController.Instance.CurrentGameState = GameState.Swipe;
        }
        yield return new WaitForSeconds(0.5f);
        if (targetDot != null)
        {
            if (!targetDot.GetComponent<DotInteraction>().IsMatched && !isMatched)
            {
                this.targetDot.GetComponent<DotInteraction>().Row = row;
                this.targetDot.GetComponent<DotInteraction>().Column = column;
                column = this.preCol;
                row = this.preRow;
                targetDot = null;
                yield return new WaitForSeconds(0.5f);
                GameStateController.Instance.CurrentGameState = GameState.Swipe;
            }
            else
            {
                GameStateController.Instance.CurrentGameState = GameState.FillingDots;
                StartCoroutine(this._alldots.DestroyMatched());
                Debug.Log("Destroy");
            }
            targetDot = null;
        }

    }
    #region FindMatched
    private void FindMatched()
    {
        if (this.column - 1 >= 0 && this.column + 1 < this._alldots.Width)
        {
            var leftDot = this._alldots.AllDots[column - 1, row];
            var rightDot = this._alldots.AllDots[column + 1, row];
            if (leftDot && rightDot && leftDot != this.gameObject && rightDot != this.gameObject)
            {
                if (leftDot.tag == this.gameObject.tag && rightDot.tag == this.gameObject.tag)
                {
                    this.isMatched = true;
                    leftDot.GetComponent<DotInteraction>().IsMatched = true;
                    rightDot.GetComponent<DotInteraction>().IsMatched = true;
                }
            }
        }

        if (this.row > 0 && this.row < this._alldots.Height - 1)
        {
            var upDot = this._alldots.AllDots[this.column, this.row + 1];
            var downDot = this._alldots.AllDots[this.column, this.row - 1];
            if (upDot && downDot && this.gameObject != upDot && this.gameObject != downDot)
            {
                if (upDot.tag == this.gameObject.tag && this.gameObject.tag == downDot.tag)
                {
                    this.isMatched = true;
                    upDot.GetComponent<DotInteraction>().IsMatched = true;
                    downDot.GetComponent<DotInteraction>().IsMatched = true;
                }
            }
        }
        this.Find5Matched();
    }

    private void Find5Matched()
    {
        if (this.column - 2 >= 0 && this.column + 2 < this._alldots.Width)
        {
            var leftDot = this._alldots.AllDots[this.column - 1, this.row];
            var rightDot = this._alldots.AllDots[this.column + 1, this.row];
            var leftleftDot = this._alldots.AllDots[this.column - 2, this.row];
            var rightrightDot = this._alldots.AllDots[this.column + 2, this.row];

            if (leftDot && rightDot && leftDot.tag == rightDot.tag && rightDot.tag == this.gameObject.tag && leftDot != gameObject && rightDot != gameObject)
            {
                if (leftleftDot && rightrightDot && leftleftDot.tag == rightrightDot.tag && leftleftDot.tag == this.gameObject.tag)
                {
                    var count = 0;
                    count++;
                    Debug.Log("5 Matched " + count);
                }
            }
        }

        if (row - 2 >= 0 && this.row + 2 < this._alldots.Height)
        {
            var upDot = this._alldots.AllDots[this.column, row + 1];
            var downDot = this._alldots.AllDots[this.column, row - 1];
            var upupDot = this._alldots.AllDots[this.column, row + 2];
            var downdownDot = this._alldots.AllDots[this.column, this.row - 2];

            if (upDot && downDot && upDot.tag == downDot.tag && downDot.tag == this.gameObject.tag && upDot != gameObject && downDot != gameObject)
            {
                if (upupDot && downdownDot && upupDot.tag == downdownDot.tag && downdownDot.tag == this.gameObject.tag)
                {
                    var count = 0;
                    count++;
                    Debug.Log("5 Matched " + count);
                }
            }
        }
    }
    #endregion



    #region Input
    private string GetValueSwipe()
    {
        var mousePosition = "invalid";
        if (Vector2.Distance(this.mouseDow, this.mouseUp) < 10f)
            return mousePosition;
        var checkMouseHorizontal = mouseUp.x - mouseDow.x;
        var checkMouseVertical = mouseUp.y - mouseDow.y;
        if (Mathf.Abs(checkMouseHorizontal) > Mathf.Abs(checkMouseVertical))
        {
            if (checkMouseHorizontal > 0)
                mousePosition = "right";
            else
                mousePosition = "left";
        }
        else
        {
            if (checkMouseVertical > 0)
                mousePosition = "up";
            else
                mousePosition = "dow";
        }


        return mousePosition;
    }

    private void CheckSwipe(string inputSwipe)
    {
        if (inputSwipe == "left" && column - 1 >= 0)
        {
            this.SetValue();
            this.targetDot = this._alldots.AllDots[column - 1, row];
            this.targetDot.GetComponent<DotInteraction>().Column += 1;
            column -= 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckKingDots;
            Debug.Log("Left");
        }
        else if (inputSwipe == "right" && column + 1 < this._alldots.Width)
        {
            this.SetValue();
            this.targetDot = this._alldots.AllDots[column + 1, row];
            this.targetDot.GetComponent<DotInteraction>().Column -= 1;
            column += 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckKingDots;
            Debug.Log("Right");
        }
        else if (inputSwipe == "up" && row + 1 < this._alldots.Height)
        {
            this.SetValue();
            this.targetDot = this._alldots.AllDots[column, row + 1];
            this.targetDot.GetComponent<DotInteraction>().Row -= 1;
            row += 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckKingDots;
            Debug.Log("Up");
        }
        else if (inputSwipe == "dow" && row - 1 >= 0)
        {
            this.SetValue();
            this.targetDot = this._alldots.AllDots[column, row - 1];
            this.targetDot.GetComponent<DotInteraction>().Row += 1;
            row -= 1;
            GameStateController.Instance.CurrentGameState = GameState.CheckKingDots;
            Debug.Log("Down");
        }
        StartCoroutine(this.CheckMatched());
    }
    private void SetValue()
    {
        preCol = column;
        preRow = row;
    }
    private string Test()
    {
        var inputMouse = "invalid";
        if (Vector2.Distance(mouseUp, mouseDow) < 1f)
        {
            Debug.Log(inputMouse);
            return inputMouse;
        }
        float angle = Mathf.Atan2(mouseUp.x - mouseDow.x, mouseUp.y - mouseDow.y) * 180 / Mathf.PI;
        Debug.Log(angle);
        if (angle >= -100 && angle <= -60)
            inputMouse = "left";
        else if (angle <= 100 && angle >= 60)
            inputMouse = "right";
        else if (angle > -60 && angle < 60)
            inputMouse = "up";
        else if (angle < -100 || angle > 100)
            inputMouse = "dow";

        return inputMouse;
    }
    private Vector2 InputSwipe()
    {
        var mouseInput = Input.mousePosition;
        return mouseInput;
    }

    private void OnMouseDown()
    {
        if (GameStateController.Instance.CurrentGameState != GameState.Swipe && TurnController.instance.currentTurn != GameTurn.Player)
            return;
        this.mouseDow = InputSwipe();
    }

    private void OnMouseUp()
    {
        if (GameStateController.Instance.CurrentGameState != GameState.Swipe && TurnController.instance.currentTurn != GameTurn.Player)
            return;
        GameStateController.Instance.CurrentGameState = GameState.CheckKingDots;
        this.mouseUp = InputSwipe();
        var inputSwipe = this.GetValueSwipe();
        this.CheckSwipe(inputSwipe);
    }

    // private void OnMouseOver()
    // {
    //     if (GameStateController.Instance.CurrentGameState == GameState.Swipe)
    //         transform.localScale = Vector3.one * 1.2f;
    // }
    private void OnMouseEnter()
    {
        if (GameStateController.Instance.CurrentGameState != GameState.Swipe && TurnController.instance.currentTurn != GameTurn.Player)
            return;
        transform.localScale = Vector3.one * 1.2f;
    }

    // private void OnMouseExit()
    // {
    //     if (!Input.GetMouseButton(0) && GameStateController.Instance.CurrentGameState == GameState.Swipe)
    //         transform.localScale = Vector3.one;
    // }
    #endregion

    public void SetDot(int col, int row)
    {
        this.column = col;
        this.row = row;
    }
}
