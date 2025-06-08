using _01_Scripts.CameraScript;
using _01_Scripts.Combat;
using UnityEngine;

namespace _01_Scripts.Shelling
{
    public class ExplosionBomb : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private float damage;
        
        private Collider[] _targets = new Collider[100];
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter");
            if (collision.gameObject.TryGetComponent(out EntityHealth entityHealth))
            {
                Explode(entityHealth);
            }
        }

        private void Explode(EntityHealth health)
        {
            Debug.Log("Explode");
            health.ApplyDamage(100);
            CameraShake.Instance.Active2Shake();
        }
    }
}