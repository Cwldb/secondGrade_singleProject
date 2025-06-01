using System;
using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public event Action HandleChangeEnemyCount;

        [SerializeField] private int startLevelCount;
        private int _enemyCount;

        private void Start()
        {
            HandleChangeEnemyCount += AddEnemyCount;
        }

        public void AddEnemyCount()
        {
            _enemyCount++;
            if (_enemyCount == startLevelCount)
            {
                startLevelCount+=2;
                
            }
            HandleChangeEnemyCount?.Invoke();
        }
    }
}
