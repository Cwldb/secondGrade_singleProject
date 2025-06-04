using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _01_Scripts.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> EnemyPrefabs = new List<GameObject>();
        private List<Transform> enemySpawnPos = new List<Transform>();

        private void Start()
        {
            foreach (Transform trm in transform)
                enemySpawnPos.Add(trm);
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)], enemySpawnPos[Random.Range(0, enemySpawnPos.Count)].position, Quaternion.identity);
            yield return new WaitForSeconds(4);
            StartCoroutine(SpawnEnemy());
        }
    }
}