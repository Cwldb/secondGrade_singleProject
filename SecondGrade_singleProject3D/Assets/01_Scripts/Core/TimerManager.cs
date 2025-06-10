using System;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        public float Count { get; set; }
        public int Minutes { get; set; }

        private bool _is1Minute;
        private bool _is2Minute;
        private bool _is3Minute;
        private bool _is4Minute;
        public bool _is5Minute { get; set; }

        private void Update()
        {
            Count +=  Time.deltaTime;
            if (Count >= 60)
            {
                Minutes++;
                Count = 0;
            }

            if (Minutes == 1 && !_is1Minute)
            {
                GameManager.Instance.enemySpawnDelay -= 0.2f;
                _is1Minute = true;
            }
            else if (Minutes == 2 && !_is2Minute)
            {
                GameManager.Instance.enemySpawnDelay -= 0.2f;
                _is2Minute = true;
            }
            else if (Minutes == 3  && !_is3Minute)
            {
                GameManager.Instance.enemySpawnDelay -= 0.1f;
                _is3Minute = true;
            }
            else if (Minutes == 4 && !_is4Minute)
            {
                GameManager.Instance.enemySpawnDelay -= 0.1f;
                _is4Minute = true;
            }
            else if (Minutes == 5 && !_is5Minute)
            {
                GameManager.Instance.enemySpawnDelay -= 0.2f;
                _is5Minute = true;
            }

        }
    }
}