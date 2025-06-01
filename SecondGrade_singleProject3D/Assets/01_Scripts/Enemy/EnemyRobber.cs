using _01_Scripts.Combat;
using _01_Scripts.Core;
using Blade.Enemies.BT.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _01_Scripts.Enemy
{
    public class EnemyRobber : Enemy, IKnockBackable
    {
        public UnityEvent<Vector3, float> OnKnockBackInvoke;
        private StateChange _stateChannel;
        
        protected override void Start()
        {
            base.Start();
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
        
        public void KnockBack(Vector3 force, float time)
        {
            OnKnockBackInvoke?.Invoke(force, time);
        }
    }
}