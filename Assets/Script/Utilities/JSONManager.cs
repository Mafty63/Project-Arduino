using System.Collections.Generic;
using System.IO;
using ProjectArduino.Interface;
using UnityEngine;

namespace ProjectArduino.Utilities
{
    public class JSONManager : Singleton<JSONManager>
    {
        [System.Serializable]
        public class LeaderboardData
        {
            public List<Leaderboard.LeaderboardEntry> leaderboardEntries = new List<Leaderboard.LeaderboardEntry>();
        }

        private string filePath;

        protected override void Awake()
        {
            base.Awake();
            filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        }

        public void SaveLeaderboard(List<Leaderboard.LeaderboardEntry> leaderboard)
        {
            LeaderboardData data = new LeaderboardData { leaderboardEntries = leaderboard };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);

            Debug.Log($"Leaderboard saved to {filePath}");
        }

        public List<Leaderboard.LeaderboardEntry> LoadLeaderboard()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);

                Debug.Log($"Leaderboard loaded from {filePath}");
                return data.leaderboardEntries;
            }
            else
            {
                Debug.LogWarning("No leaderboard file found. Returning an empty list.");
                return new List<Leaderboard.LeaderboardEntry>();
            }
        }
    }
}