using System;
using _01_Scripts.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Scripts.Players
{
    public class PlayerEnemyDetect : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        public StatSO atkPowerStat, critPowerStat, critPerStat;
        
        public float radius = 0f;
        public LayerMask layer;
        public Collider[] Colliders { get; set; }
        public Collider ShortEnemy { get; set; }

        private Entity _entity;
        private EntityStat _statCompo;
        private CharacterMovement _movement;

        public float damage;
        public float critPer;
        public float critPower;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
            _movement = entity.GetCompo<CharacterMovement>();
        }

        public void AfterInitialize()
        {
            damage = _statCompo.SubscribeStat(atkPowerStat, HandleDamage, 5);
            critPer = _statCompo.SubscribeStat(critPerStat, HandleCritPer, 0);
            critPower = _statCompo.SubscribeStat(critPowerStat, HandleCritPower, 1);
        }

        private void HandleCritPer(StatSO stat, float currentValue, float prevValue)
        {
            critPer = prevValue;
        }

        private void HandleCritPower(StatSO stat, float currentValue, float prevValue)
        {
            critPower = prevValue;
        }

        private void HandleDamage(StatSO stat, float currentValue, float prevValue)
        {
            damage = currentValue + prevValue;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(atkPowerStat, HandleDamage);
            _statCompo.UnSubscribeStat(critPerStat, HandleCritPer);
            _statCompo.UnSubscribeStat(critPowerStat, HandleCritPower);
        }

        public float DamageCalc()
        {
            float dmg = damage;
            if (Random.value < critPer)
            {
                return dmg *= critPower;
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