using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectArduino
{
    public class EntryTemplate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI point;

        public void SetEntry(string number, string playerName, string point)
        {
            this.number.text = number;
            this.playerName.text = playerName;
            this.point.text = point;
        }
    }
}
