using _01_Scripts.Combat;
using _01_Scripts.Core;
using _01_Scripts.Entities;
using _01_Scripts.Players;
using Blade.Enemies.BT.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _01_Scripts.Enemy
{
    public class EnemyRobber : Enemy, IKnockBackable
    {
        public UnityEvent<Vector3, float> OnKnockBackInvoke;
        private StateChange _stateChannel;
        private EntityAnimatorTrigger _animatorTrigger;
        private EnemyAttackCompo _attackCompo;
        
        protected override void Start()
        {
            base.Start();
            _attackCompo = GetComponentInChildren<EnemyAttackCompo>();
            _animatorTrigger = GetComponentInChildren<EntityAnimatorTrigger>();
            if (_attackCompo == null) Debug.Log("Asdasd");
            if (_animatorTrigger == null) Debug.Log("sfsdfsdsfs");
            _animatorTrigger.OnDamageCastTrigger += HandleAttackEvent;
            OnDeadEvent.AddListener(HandleDeathEvent);
            _stateChannel = GetBlackboardVariable<StateChange>("StateChannel").Value;
        }
        
        private void HandleDeathEvent()
        {
            if (IsDead) return;
            IsDead = true;
            GameManager.Instance.AddEnemyCount();
            _stateChannel.SendEventMessage(EnemyState.DEAD);
        }

        private void HandleAttackEvent()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent(out Player player))
                {
                    if (collider.TryGetComponent(out EntityHealth entityHealth))
                    {
                        Debug.Log(_attackCompo.Damage);
                        entityHealth.ApplyDamage(_attackCompo.Damage);
                    }
                }
            }
        }
        
        public void KnockBack(Vector3 force, float time)
        {
            OnKnockBackInvoke?.Invoke(force, time);
        }
    }
}