using _01_Scripts.CameraScript;
using _01_Scripts.Entities;
using _01_Scripts.Players.Bullet;
using KJYLib.StatSystem;
using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerFire : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform _playerFire;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private StatSO atkSpeedStat;
        
        private Entity _entity;
        private EntityStat _statCompo;
        private PlayerStat _playerStat;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
            _playerStat = entity.GetCompo<PlayerStat>();
        }
        
        public void FireBullet(float damage)
        {
            GameObject bullet = Instantiate(bulletPrefab, _playerFire.position, Quaternion.Euler(_entity.transform.rotation.eulerAngles));
            _particle.Play();
            bullet.GetComponent<BulletMove>().Damage = damage;
            CameraShake.Instance.Shake();
        }
    }
}