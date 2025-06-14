using System;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Action OnLevelUp;
        public Action OnActiveLevelUp;
        public Action OnEnemyCount;
        public Action OnGameOver;
        public Action OnValueChange;

        [field: SerializeField] public EntityFinderSO PlayerFinder { get; set; }

        public int startLevelCount;
        public float enemySpawnDelay;
        
        private int _enemyCount;
        private int _curLevel = 0;

        public int GetCurNeedLevel() => startLevelCount - _enemyCount;
        public int GetCurLevel() => _curLevel;

        public void AddEnemyCount()
        {
            _enemyCount++;
            if (_enemyCount == startLevelCount)
            {
                if (_enemyCount % 4 == 0)
                {
                    OnActiveLevelUp?.Invoke();
                }
                else
                    OnLevelUp?.Invoke();
                _enemyCount = 0;
                _curLevel++;
                startLevelCount += 10;
            }
            OnEnemyCount?.Invoke();
        }
    }
}
