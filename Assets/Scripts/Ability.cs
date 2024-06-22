using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    protected Button ability;
    protected int isConsumpMana;

    protected Player Player;
    protected Enemy Enemy;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = FindFirstObjectByType<Player>();
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
        if (TurnController.instance.currentTurn == GameTurn.Player)
            if (this.Player.PowerCurrentScore >= this.isConsumpMana)
                return true;
        if (TurnController.instance.currentTurn == GameTurn.Enemy)
            if (this.Enemy.PowerCurrentScore >= this.isConsumpMana)
                return true;
        return false;
    }

    protected virtual void UpdateMana()
    {
        if (TurnController.instance.currentTurn == GameTurn.Player)
            this.Player.PowerCurrentScore -= this.isConsumpMana;
        if (TurnController.instance.currentTurn == GameTurn.Enemy)
            this.Enemy.PowerCurrentScore -= this.isConsumpMana;
    }
}
