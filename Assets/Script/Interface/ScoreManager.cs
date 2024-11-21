using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectArduino
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int currentScore;

        public void AddScore()
        {
            currentScore += 1;
            scoreText.text = currentScore.ToString();
        }
    }
}
