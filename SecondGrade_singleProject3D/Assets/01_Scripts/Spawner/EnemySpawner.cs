using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _01_Scripts.Core;
using _01_Scripts.Enemy;
using KJYLib.Dependencies;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

namespace _01_Scripts.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<PoolItemSO> EnemyItems = new List<PoolItemSO>();
        private List<Transform> enemySpawnPos = new List<Transform>();

        [SerializeField] private float xMin = -10f;
        [SerializeField] private float xMax = 10f;
        [SerializeField] private float zMin = -10f;
        [SerializeField] private float zMax = 10f;
        
        [Inject] private PoolManagerMono _poolManagerMono;

        private void Start()
        {
            foreach (Transform trm in transform)
                enemySpawnPos.Add(trm);
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Transform spawnPoint = GetValidSpawnPoint();
            if (spawnPoint != null)
            {
                EnemySoldiers enemy = _poolManagerMono.Pop<EnemySoldiers>(EnemyItems[Random.Range(0, EnemyItems.Count)]);
                enemy.transform.position = spawnPoint.position;
            }

            yield return new WaitForSeconds(GameManager.Instance.enemySpawnDelay);
            StartCoroutine(SpawnEnemy());
        }

        private Transform GetValidSpawnPoint()
        {
            List<Transform> validSpawnPoints = enemySpawnPos.Where(pos =>
                pos.position.x >= xMin &&
                pos.position.x <= xMax &&
                pos.position.z >= zMin &&
                pos.position.z <= zMax
            ).ToList();

            if (validSpawnPoints.Count == 0)
                return null;

            return validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];
        }
    }
}