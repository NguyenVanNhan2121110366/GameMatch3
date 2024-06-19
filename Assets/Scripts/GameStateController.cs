using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private static GameStateController instance;
    public static GameStateController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<GameStateController>();
            }
            return instance;
        }
    }
    [SerializeField] private GameState currentGameState;
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }
    // Start is called before the first frame update
    void Start()
    {
        this.currentGameState = GameState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.Finish)
        {
            Debug.Log("Vao Day");
            currentGameState = GameState.Swipe;
            ScoreController.instance.ResetScore();
            TurnController.instance.CheckSwitchTurn();
        }
    }
}
public enum GameState
{
    None,
    FillingDots,
    CheckKingDots,
    Attacking,
    Finish,
    Swipe
}
