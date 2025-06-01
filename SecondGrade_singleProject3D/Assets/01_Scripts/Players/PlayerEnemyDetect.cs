using System;
using _01_Scripts.Entities;
using KJYLib.StatSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Players
{
    public class PlayerEnemyDetect : MonoBehaviour, IEntityComponent
    {
        public StatSO atkPowerStat, critPowerStat, critPerStat;
        
        public float radius = 0f;
        public LayerMask layer;
        public Collider[] Colliders { get; set; }
        public Collider ShortEnemy { get; set; }

        private Entity _entity;
        private EntityStat _statCompo;
        private CharacterMovement _movement;
        private PlayerStat _playerStat;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
            _movement = entity.GetCompo<CharacterMovement>();
            _playerStat = entity.GetCompo<PlayerStat>();
        }
        
        public float DamageCalc()
        {
            float dmg = _playerStat.Damage;
            if (Random.value < _playerStat.CritPer)
            {
                return dmg *= _playerStat.CritPower;
            }
            
            return dmg;
        }

        private void FixedUpdate()
        {
            if (_movement.CanShoot) return;
            
            ShortEnemy = null;
            Colliders = Physics.OverlapSphere(transform.position, radius, layer);

            float shortestDistance = Mathf.Infinity;

            foreach (Collider col in Colliders)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    ShortEnemy = col;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        
    }
}