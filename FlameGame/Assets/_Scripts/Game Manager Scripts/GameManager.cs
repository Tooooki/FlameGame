using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public bool Rogas;
    public static GameManager Instance;
    int currentLevel = 0;
    GameState currentState;
    public event Action<GameState> OnStateChanged;
    void Awake()
    {
        Instance = this;
    }

    void Update() //for testing
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ChangeState(GameState.CardSelection);
            currentLevel++;
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }


    public void ChangeState(GameState newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
        HandleStateChanged();
    }

    private void HandleStateChanged()
    {
        CardManager.Instance.HandleGameStateChanged(currentState);
        switch (currentState)
        {
            case GameState.Playing:
                CardManager.Instance.HideCardSelection();
                break;
            case GameState.CardSelection:
                CardManager.Instance.ShowCardSelection();
                break;
        }
    }

    public enum GameState
    {
        Playing,
        CardSelection
    }
}
