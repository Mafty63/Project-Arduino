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
        [SerializeField] private Button confirmButton;

        private void Start()
        {
            confirmButton.onClick.AddListener(AddToLeaderBoard);
        }

        private void AddToLeaderBoard()
        {
            InterfaceManager.Instance.Leaderboard.AddEntry(inputField.text, InterfaceManager.Instance.Score.GetScore());
            GameManager.Instance.ResetGame();
        }

    }
}
