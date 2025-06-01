using System;
using _01_Scripts.Entities;
using KJYLib.StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01_Scripts.Players
{
    public class PlayerStat : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        public StatSO atkPowerStat, critPowerStat, critPerStat, atkSpeedStat;

        public float Damage;
        public float CritPer { get; set; }
        public float CritPower { get; set; }
        public float AttackSpeed { get; set; }
        
        private Entity _entity;
        private EntityStat _statCompo;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            Damage = _statCompo.SubscribeStat(atkPowerStat, HandleDamage, 5);
            CritPer = _statCompo.SubscribeStat(critPerStat, HandleCritPer, 0);
            CritPower = _statCompo.SubscribeStat(critPowerStat, HandleCritPower, 1);
            AttackSpeed = _statCompo.SubscribeStat(atkSpeedStat, HandleAtkSpeed, 1);
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(atkPowerStat, HandleDamage);
            _statCompo.UnSubscribeStat(critPerStat, HandleCritPer);
            _statCompo.UnSubscribeStat(critPowerStat, HandleCritPower);
            _statCompo.UnSubscribeStat(atkSpeedStat, HandleAtkSpeed);
        }

        private void HandleCritPer(StatSO stat, float currentValue, float prevValue)
        {
            CritPer = prevValue;
        }

        private void HandleCritPower(StatSO stat, float currentValue, float prevValue)
        {
            CritPower = prevValue;
        }

        private void HandleDamage(StatSO stat, float currentValue, float prevValue)
        {
            Damage = prevValue;
        }
        
        private void HandleAtkSpeed(StatSO stat, float currentValue, float prevValue)
        {
            AttackSpeed = prevValue;
        }
        
    }
}