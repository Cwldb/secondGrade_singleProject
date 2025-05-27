using Blade.Enemies.BT.Events;
using System;
using _01_Scripts.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Enemies.Skeletons
{
    public class EnemySkeleton : Enemy, IKnockBackable
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
            _stateChannel.SendEventMessage(EnemyState.DEAD);
        }

        public void KnockBack(Vector3 force, float time)
        {
            OnKnockBackInvoke?.Invoke(force, time);
        }
    }
}