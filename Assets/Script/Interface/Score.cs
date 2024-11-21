using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectArduino.Interface
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int currentScore;

        public void AddScore()
        {
            currentScore += 1;
            scoreText.text = currentScore.ToString();
        }
        public int GetScore() => currentScore;
    }
}
