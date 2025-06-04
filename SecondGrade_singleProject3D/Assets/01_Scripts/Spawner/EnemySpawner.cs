using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _01_Scripts.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject Enemy1Prefab;
        private List<Transform> enemySpawnPos = new();

        private void Start()
        {
            foreach (Transform trm in transform)
                enemySpawnPos.Add(trm);
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Instantiate(Enemy1Prefab, enemySpawnPos[Random.Range(0, 4)].position, Quaternion.identity);
            yield return new WaitForSeconds(4);
            StartCoroutine(SpawnEnemy());
        }
    }
}