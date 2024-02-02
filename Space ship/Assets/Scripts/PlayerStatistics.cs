using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int numkills;
        public int score;
        public int time;

        public void Reset()
        {
            numkills = 0;
            score = 0;
            time = 0;
        }
    }
}

