using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Combat
{
    public class EntityHealth : MonoBehaviour, IDamageable, IEntityComponent, IAfterInitialize
    {
        private Entity _entity;
        private EntityActionData _actionData;
        private EntityStat _statCompo;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _actionData = entity.GetCompo<EntityActionData>();
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            currentHealth = maxHealth = _statCompo.SubscribeStat(hpStat, HandleMaxHpChange, 10f);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(hpStat, HandleMaxHpChange);
        }

        private void HandleMaxHpChange(StatSO stat, float currentValue, float prevValue)
        {
            float changed = currentValue - prevValue;
            maxHealth = currentValue;
            if (changed > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
        }

        public void ApplyDamage(DamageData damageData, Vector3 hitPoint, Vector3 hitNormal, AttackDataSO attackData, Entity dealer)
        {
            _actionData.HitPoint = hitPoint;
            _actionData.HitNormal = hitNormal;

            currentHealth = Mathf.Clamp(currentHealth - damageData.damage, 0, maxHealth);
            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke();
        }

        
        
    }
}