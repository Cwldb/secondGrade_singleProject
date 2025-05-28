using _01_Scripts.Entities;
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
        public void FireBullet()
        {
            Instantiate(bulletPrefab, _playerFire.position, Quaternion.Euler(_player.transform.rotation.eulerAngles));
        }

        
    }
}