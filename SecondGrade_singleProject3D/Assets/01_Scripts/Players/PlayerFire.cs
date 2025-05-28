using UnityEngine;

namespace _01_Scripts.Players
{
    public class PlayerFire : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        
        public void FireBullet()
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}