using _01_Scripts.CameraScript;
using _01_Scripts.Combat;
using _01_Scripts.Effect;
using Blade.Effects;
using KJYLib.Dependencies;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

namespace _01_Scripts.Shelling
{
    public class ExplosionBomb : MonoBehaviour, IPoolable
    {
        [field : SerializeField] public PoolItemSO PoolItem { get; set; }
        [SerializeField] private LayerMask layer;
        [SerializeField] private float radius;
        [SerializeField] private float damage;
        
        [SerializeField] private PoolItemSO particle;

        [Inject] private PoolManagerMono _poolManger;
        
        public GameObject GameObject => gameObject;
        private readonly Collider[] _targets = new Collider[100];
        private Pool _pool;
        
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
            PoolingEffect effect = _poolManger.Pop<PoolingEffect>(particle);
            
            // var effect = Instantiate(particle, transform.position, Quaternion.Euler(0, 0, 0));
            // effect.transform.parent = null;
            CameraShake.Instance.Active2Shake();
            _pool.Push(this);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }

        public void ResetItem()
        {
            
        }
    }
}