using System;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Action OnLevelUp;

        [field : SerializeField] public EntityFinderSO PlayerFinder { get; set; }
        [SerializeField] private int startLevelCount;
        
        private int _enemyCount;

        private void Start()
        {
        }

        public void AddEnemyCount()
        {
            _enemyCount++;
            if (_enemyCount == startLevelCount)
            {   
                OnLevelUp?.Invoke();
                startLevelCount+=5;
            }
        }
    }
}
