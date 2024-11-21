using System.Collections.Generic;
using ProjectArduino.Utilities;
using TMPro;
using UnityEngine;

namespace ProjectArduino.Interface
{
    public class Leaderboard : MonoBehaviour
    {
        [System.Serializable]
        public class LeaderboardEntry
        {
            public string playerName;
            public int totalPoints;
        }

        [SerializeField] private EntryTemplate entryTemplate;
        [SerializeField] private Transform leaderboardContent;
        private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
        private const int maxEntries = 10;

        private void Start()
        {
            leaderboard = JSONManager.Instance.LoadLeaderboard();

            if (leaderboard.Count == 0)
            {
                AddEntry("Mafty", 6969);
                AddEntry("Bob", 200);
                AddEntry("Charlie", 150);
            }

            UpdateLeaderboardUI();
        }

        public void AddEntry(string name, int points)
        {
            LeaderboardEntry newEntry = new LeaderboardEntry { playerName = name, totalPoints = points };

            leaderboard.Add(newEntry);

            leaderboard.Sort((a, b) => b.totalPoints.CompareTo(a.totalPoints));

            if (leaderboard.Count > maxEntries)
            {
                leaderboard.RemoveAt(leaderboard.Count - 1);
            }

            JSONManager.Instance.SaveLeaderboard(leaderboard);
        }

        public void UpdateLeaderboardUI()
        {
            foreach (Transform child in leaderboardContent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < leaderboard.Count; i++)
            {
                EntryTemplate entryObject = Instantiate(entryTemplate, leaderboardContent);

                entryObject.SetEntry((i + 1).ToString(), leaderboard[i].playerName, leaderboard[i].totalPoints.ToString());
            }
        }
    }
}