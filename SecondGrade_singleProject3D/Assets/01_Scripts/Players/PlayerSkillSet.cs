using System;
using _01_Scripts.CameraScript;
using _01_Scripts.Combat;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerSkillSet : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private float radius;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem smokeParticle;
        [SerializeField] private ParticleSystem crackParticle;
        public float cooldownTime = 6f;
        
        private Player _player;
        private Collider[] _targets = new Collider[100];
        public float CurrentCooldown { get; set; } = 6f;

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
                CurrentCooldown += Time.deltaTime;
                if (CurrentCooldown >= cooldownTime)
                    CanUseActive1 = true;
            }
        }

        public void UseActive1()
        {
            CanUseActive1 = false;
            CurrentCooldown = 0;
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
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}