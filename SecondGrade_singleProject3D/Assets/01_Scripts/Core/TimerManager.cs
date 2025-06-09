using System;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        public float Count { get; set; }
        public int Minutes { get; set; }

        private void Update()
        {
            Count +=  Time.deltaTime;
            if (Count >= 60)
            {
                Minutes++;
                Count = 0;
            }

            if (Minutes == 1)
                GameManager.Instance.enemySpawnDelay -= 0.2f;
            else if (Minutes == 2)
                GameManager.Instance.enemySpawnDelay -= 0.2f;
            else if (Minutes == 3)
                GameManager.Instance.enemySpawnDelay -= 0.1f;
            else if (Minutes == 4)
                GameManager.Instance.enemySpawnDelay -= 0.1f;
            else if (Minutes == 5)
                GameManager.Instance.enemySpawnDelay -= 0.2f;

        }
    }
}