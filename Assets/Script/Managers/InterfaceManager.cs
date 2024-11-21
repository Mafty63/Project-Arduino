using ProjectArduino;
using ProjectArduino.Interface;
using ProjectArduino.Utilities;
using UnityEngine;

namespace ProjectArduino.Managers
{
    public class InterfaceManager : Singleton<InterfaceManager>
    {
        public Score Score;
        public Leaderboard Leaderboard;
        public NameInput NameInput;

    }
}