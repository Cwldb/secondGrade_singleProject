using _01_Scripts.Entities;
using _01_Scripts.Players.Bullet;
using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerFire : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform _playerFire;
        [SerializeField] private GameObject bulletPrefab;
        public StatSO attackSpeed;
        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }
        public void FireBullet(float damage)
        {
            GameObject bullet = Instantiate(bulletPrefab, _playerFire.position, Quaternion.Euler(_player.transform.rotation.eulerAngles));
            bullet.GetComponent<BulletMove>().Damage = damage;
        }

        
    }
}