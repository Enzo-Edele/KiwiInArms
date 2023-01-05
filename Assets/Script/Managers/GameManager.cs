using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    private GameStates gameState;
    public enum GameStates
    {
        InGame,
        Pause,
        MainMenu,
        GameOver,
    }

    private GameStates currentGameState;
    public GameStates CurrentGameStates
    {
        get => currentGameState;
        set
        {
            currentGameState = value;
            switch (currentGameState)
            {
                case GameStates.MainMenu:
                    break;

                case GameStates.InGame:
                    Time.timeScale = 1.0f;
                    break;

                case GameStates.Pause:
                    Time.timeScale = 0.0f;
                    break;

                case GameStates.GameOver:
                    Time.timeScale = 0.0f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    break;

            }
        }
    }
}