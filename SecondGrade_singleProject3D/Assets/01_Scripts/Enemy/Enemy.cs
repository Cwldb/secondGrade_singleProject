using _01_Scripts.Entities;
using Unity.Behavior;
using UnityEngine;

namespace _01_Scripts.Enemy
{
    public abstract class Enemy : Entity
    {
        [field : SerializeField] public EntityFinderSO PlayerFinder { get; set; }
        public BehaviorGraphAgent BtAgent { get; private set; }
        protected Animator _animator;

        #region temp
        public float attackRange = 2f;
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        
        protected override void AddComponents()
        {
            base.AddComponents();
            _animator = GetComponentInChildren<Animator>();
            BtAgent = GetComponent<BehaviorGraphAgent>();
            Debug.Assert(BtAgent != null, $"{gameObject.name} don't have BehaviorGraphAgent");
        }

        public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
        {
            if(BtAgent.GetVariable(key, out BlackboardVariable<T> result))
                return result;
            return default;
        }
    }
}