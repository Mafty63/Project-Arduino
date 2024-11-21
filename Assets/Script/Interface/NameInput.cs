using System.Collections;
using System.Collections.Generic;
using ProjectArduino.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectArduino.Interface
{
    public class NameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AddToLeaderBoard();
            }
        }

        private void OnEnable()
        {
            inputField.Select();
        }

        private void AddToLeaderBoard()
        {
            InterfaceManager.Instance.Leaderboard.AddEntry(inputField.text, InterfaceManager.Instance.Score.GetScore());
            GameManager.Instance.ResetGame();
        }

    }
}
