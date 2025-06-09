using System;
using System.Collections;
using _01_Scripts.CameraScript;
using _01_Scripts.Combat;
using _01_Scripts.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Players
{
    public class PlayerSkillSet : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private LayerMask layer;
        [Header("Active1")]
        [SerializeField] private float radius = 6;
        [SerializeField] private float damage = 50;
        [SerializeField] private ParticleSystem smokeParticle;
        [SerializeField] private ParticleSystem crackParticle;
        public float cooldownTime1 = 10f;
        
        [Header("Active2")]
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private float bombSpawnRange = 5f;
        [SerializeField] private int bombCount = 3;
        public float cooldownTime2 = 30f;
        
        
        private Player _player;
        private Collider[] _targets = new Collider[100];
        public float CurrentCooldown1 { get; set; } = 6f;
        public float CurrentCooldown2 { get; set; } = 6f;

        public bool CanUseActive1 { get; set; } = true;
        public bool CanUseActive2 { get; set; } = true;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void Update()
        {
            if (!CanUseActive1)
            {
                CurrentCooldown1 += Time.deltaTime;
                if (CurrentCooldown1 >= cooldownTime1)
                    CanUseActive1 = true;
            }

            if (!CanUseActive2)
            {
                CurrentCooldown2 += Time.deltaTime;
                if (CurrentCooldown2 >= cooldownTime2)
                    CanUseActive2 = true;
            }
        }

        public void UseActive1()
        {
            CanUseActive1 = false;
            CurrentCooldown1 = 0;
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, _targets, layer);
            for (int i = 0; i < hitCount; i++)
            {
                Collider target = _targets[i];
                if (target.TryGetComponent(out EntityHealth health))
                    health.ApplyDamage(damage);
            }
            var smokeEffect = Instantiate(smokeParticle, transform.position, Quaternion.Euler(-90, 0, 0));
            var crackEffect = Instantiate(crackParticle, transform.position, Quaternion.Euler(-90, 0, 0));
            smokeEffect.transform.parent = null;
            crackEffect.transform.parent = null;
            CameraShake.Instance.Active1Shake();
        }
        
        public void UseActive2()
        {
            CanUseActive2 = false;
            CurrentCooldown2 = 0;
            for (int i = 0; i < bombCount; i++)
            {
                StartCoroutine(SpawnBomb());
            }
        }

        private IEnumerator SpawnBomb()
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            Vector3 randomPos = GetRandomPositionAroundPlayer();
            Instantiate(bombPrefab, randomPos, Quaternion.identity);
        }

        private Vector3 GetRandomPositionAroundPlayer()
        {
            Vector2 circle = UnityEngine.Random.insideUnitCircle * bombSpawnRange;
            Vector3 randomPos = _player.transform.position + new Vector3(circle.x, 20f, circle.y);
            return randomPos;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}