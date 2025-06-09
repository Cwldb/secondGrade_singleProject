using _01_Scripts.CameraScript;
using _01_Scripts.Combat;
using UnityEngine;

namespace _01_Scripts.Shelling
{
    public class ExplosionBomb : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private float radius;
        [SerializeField] private float damage;
        
        [SerializeField] private ParticleSystem particle;
        
        private readonly Collider[] _targets = new Collider[100];
        
        private void OnCollisionEnter(Collision collision)
        {
            Explode();
        }

        private void Explode()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, _targets, layer);
            for (int i = 0; i < hitCount; i++)
            {
                Collider target = _targets[i];
                if(target.TryGetComponent(out EntityHealth health))
                    health.ApplyDamage(damage);
            }
            var effect = Instantiate(particle, transform.position, Quaternion.Euler(0, 0, 0));
            effect.transform.parent = null;
            CameraShake.Instance.Active2Shake();
            Destroy(gameObject);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}