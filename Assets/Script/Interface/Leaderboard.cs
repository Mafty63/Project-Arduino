using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int totalPoints;
    }

    [SerializeField] private GameObject entryTemplate;
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

        foreach (var entry in leaderboard)
        {
            GameObject entryObject = Instantiate(entryTemplate, leaderboardContent);
            TextMeshProUGUI[] texts = entryObject.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 2)
            {
                texts[0].text = entry.playerName;
                texts[1].text = entry.totalPoints.ToString();
            }
        }
    }
}
