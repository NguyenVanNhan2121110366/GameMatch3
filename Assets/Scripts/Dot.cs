using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dot : MonoBehaviour
{
    [SerializeField] private string dotName;
    [SerializeField] private string dotType;
    [SerializeField] private int score;
    ScoreController scoreController;
    private AllDotController _alldots;
    private void Awake()
    {
        if (this._alldots == null)
            this._alldots = FindFirstObjectByType<AllDotController>();
        this.scoreController = FindFirstObjectByType<ScoreController>();
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
        }
        
        // if (_alldots != null)
        // {
        //     this._alldots.SpawnDestroyVFX(this);
        // }
        //this._alldots.SpawnDestroyVFX(this);
    }

    private void UpdateScore()
    {
        if (dotName == "Gold")
            this.scoreController.GoldScore += score;
        if (dotName == "Heart")
            this.scoreController.BloodScore += score;
        if (dotName == "Sword")
            this.scoreController.AttackScore += score;
        if (dotName == "Shield")
            this.scoreController.ShieldScore += score;
        if (dotName == "Mana")
            this.scoreController.PowerScore += score;
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
