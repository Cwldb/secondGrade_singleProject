using System;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public event Action HandleChangeEnemyCount;
        public Action OnLevelUp;

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
                Debug.Log("asdasd");
                OnLevelUp?.Invoke();
                startLevelCount+=2;
            }
        }
    }
}
