using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _01_Scripts.Core;
using UnityEngine;

namespace _01_Scripts.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> EnemyPrefabs = new List<GameObject>();
        private List<Transform> enemySpawnPos = new List<Transform>();

        [SerializeField] private float xMin = -10f;
        [SerializeField] private float xMax = 10f;
        [SerializeField] private float zMin = -10f;
        [SerializeField] private float zMax = 10f;

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
                Instantiate(
                    EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)],
                    spawnPoint.position,
                    Quaternion.identity
                );
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