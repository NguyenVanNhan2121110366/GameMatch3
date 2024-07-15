using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    protected Button BntAbility;
    protected int isConsumpMana;

    protected Player Player;
    protected Enemy Enemy;

    // Start is called before the first frame update
    void Start()
    {
        //this.BntAbility = GetComponent<Button>();

        this.Enemy = FindFirstObjectByType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void ExcutedAbility()
    {
        GameStateController.Instance.CurrentGameState = GameState.Ability;
    }

    protected virtual bool CheckConditionUseAbility()
    {
        if (TurnController.Instance.currentTurn == GameTurn.Player)
        {
            if (Player.Instance.PowerCurrentScore >= this.isConsumpMana)
                return true;
        }

        // if (TurnController.instance.currentTurn == GameTurn.Enemy)
        // {
        //     if (this.Enemy == null)
        //     {
        //         this.Enemy = FindFirstObjectByType<Enemy>();
        //     }
        //     if (this.Enemy.PowerCurrentScore >= this.isConsumpMana)
        //         return true;
        // }

        return false;
    }

    protected virtual void UpdateMana()
    {
        if (TurnController.Instance.currentTurn == GameTurn.Player)
            Player.Instance.PowerCurrentScore -= this.isConsumpMana;
        if (TurnController.Instance.currentTurn == GameTurn.Enemy)
            //this.Enemy.PowerCurrentScore -= this.isConsumpMana;
            Debug.Log("Enemy dame");
    }
}
