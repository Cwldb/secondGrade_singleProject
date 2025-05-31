using _01_Scripts.Entities;
using _01_Scripts.Players.Bullet;
using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerFire : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private Transform _playerFire;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private StatSO atkSpeedStat;
        
        private Entity _entity;
        private EntityStat _statCompo;

        public float AttackSpeed;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            AttackSpeed = _statCompo.SubscribeStat(atkSpeedStat, HandleChangeAtkSpeed, 1);
        }

        private void HandleChangeAtkSpeed(StatSO stat, float currentValue, float prevValue)
        {
            AttackSpeed = prevValue;
        }

        private void OnDestroy()
        {
            _statCompo.UnSubscribeStat(atkSpeedStat, HandleChangeAtkSpeed);
        }
        
        public void FireBullet(float damage)
        {
            Debug.Log(damage);
            GameObject bullet = Instantiate(bulletPrefab, _playerFire.position, Quaternion.Euler(_entity.transform.rotation.eulerAngles));
            _particle.Play();
            bullet.GetComponent<BulletMove>().Damage = damage;
        }
    }
}