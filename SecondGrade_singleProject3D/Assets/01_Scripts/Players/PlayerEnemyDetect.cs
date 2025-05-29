using System;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerEnemyDetect : MonoBehaviour, IEntityComponent
    {
        public StatSO atkPower, critPer, critPower;
        
        public float radius = 0f;
        public LayerMask layer;
        public Collider[] Colliders { get; set; }
        public Collider ShortEnemy { get; set; }

        private Entity _entity;
        private CharacterMovement _movement;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _movement = entity.GetCompo<CharacterMovement>();
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