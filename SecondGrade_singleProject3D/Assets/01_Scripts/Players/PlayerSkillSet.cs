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
        [SerializeField] private float cooldownTime = 6f;
        [SerializeField] private ParticleSystem particle;
        
        private Player _player;
        private Collider[] _targets = new Collider[100];
        private float _currentCooldown = 6f;

        public bool CanUseActive1 { get; set; } = true;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void Awake()
        {  
            _player.PlayerInput.OnActive1Pressed += UseActive1;
        }

        private void Update()
        {
            if (!CanUseActive1)
            {
                _currentCooldown += Time.deltaTime;
                if (_currentCooldown >= cooldownTime)
                    CanUseActive1 = true;
            }
        }

        public void UseActive1()
        {
            CanUseActive1 = false;
            _currentCooldown = 0;
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, _targets, layer);
            for (int i = 0; i < hitCount; i++)
            {
                Collider target = _targets[i];
                if (target.TryGetComponent(out EntityHealth health))
                    health.ApplyDamage(damage);
            }
            var effect = Instantiate(particle, transform.position, Quaternion.Euler(-90, 0, 0));
            effect.transform.parent = null;
            CameraShake.Instance.ActiveShake();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}