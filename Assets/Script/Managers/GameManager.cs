using ProjectArduino.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectArduino.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameState { GAMENOTSTARTED, GAMESTARTED, GAMEOVER }
        [SerializeField] private ObstacleManager obstacleManager;
        public GameState CurrentGameState = GameState.GAMENOTSTARTED;

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

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

            if (CurrentGameState == GameState.GAMEOVER)
            {
                obstacleManager.SpawnStopped(true);
                InterfaceManager.Instance.Leaderboard.gameObject.SetActive(true);
                InterfaceManager.Instance.NameInput.gameObject.SetActive(true);
                return;
            }

            if (CurrentGameState == GameState.GAMESTARTED)
            {
                obstacleManager.SpawnStopped(false);
                InterfaceManager.Instance.Leaderboard.gameObject.SetActive(false);
                return;
            }
        }

        public void ResetGame()
        {
            SceneManager.LoadScene("Main Scene");
        }
    }
}