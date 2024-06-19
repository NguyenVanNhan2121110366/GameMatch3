using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dot : MonoBehaviour
{
    [SerializeField] private string dotName;
    [SerializeField] private string dotType;
    [SerializeField] private int score;
    private AllDotController _alldots;
    private void Awake()
    {
        if (this._alldots == null)
            this._alldots = FindFirstObjectByType<AllDotController>();
    }
    // Start is called before the first frame update
    void Start()
    {

        //this.dotName = gameObject.tag;
    }

    private void OnDestroy()
    {
        this.UpdateScore();
        if (dotType.Contains("Big"))
        {

            this.DestroyBig();
            Debug.Log("Destroy Big");
        }
        this._alldots.SpawnDestroyVFX(this);
    }

    private void UpdateScore()
    {
        // var namedot = gameObject.tag;
        // switch (namedot)
        // {
        //     case "Gold":
        //         ScoreController.instance.GoldScore += score;
        //         break;
        //     case "Heart":
        //         ScoreController.instance.BloodScore += score;
        //         break;
        //     case "Sword":
        //         ScoreController.instance.AttackScore += score;
        //         break;
        //     case "Shield":
        //         ScoreController.instance.ShieldScore += score;
        //         break;
        //     case "Mana":
        //         ScoreController.instance.PowerScore += score;
        //         break;
        //     default:
        //         Debug.Log("Null");
        //         break;
        // }
        if (dotName == "Gold")
            ScoreController.instance.GoldScore += score;
        if (dotName == "Heart")
            ScoreController.instance.BloodScore += score;
        if (dotName == "Sword")
            ScoreController.instance.AttackScore += score;
        if (dotName == "Shield")
            ScoreController.instance.ShieldScore += score;
        if (dotName == "Mana")
            ScoreController.instance.PowerScore += score;


    }

    private void DestroyBig()
    {
        var dot = GetComponent<DotInteraction>();
        var dotCol = dot.Column;
        var dotRow = dot.Row;
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {

                if (dotCol + i >= 0 && dotCol + i < this._alldots.Width && dotRow + j >= 0
                && dotRow + j < this._alldots.Height
                && this._alldots.AllDots[dotCol + i, dotRow + j] != null)
                {

                    Destroy(this._alldots.AllDots[dotCol + i, dotRow + j]);
                    this._alldots.AllDots[dotCol + i, dotRow + j] = null;
                }
            }
        }
    }


}
