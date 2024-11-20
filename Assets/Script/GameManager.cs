using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { GAMENOTSTARTED, GAMESTARTED, GAMEOVER }
    [SerializeField] private ObstacleManager obstacleManager;
    public GameState CurrentGameState = GameState.GAMENOTSTARTED;

    private void Start()
    {
        ChangeGameState(CurrentGameState);
    }

    public void ChangeGameState(GameState state)
    {
        CurrentGameState = state;
        if (CurrentGameState == GameState.GAMENOTSTARTED)
        {
            obstacleManager.SpawnStopped(true);
            return;
        }

        if (CurrentGameState == GameState.GAMEOVER) return;

        if (CurrentGameState == GameState.GAMESTARTED)
        {
            obstacleManager.SpawnStopped(false);
            return;
        }
    }
}