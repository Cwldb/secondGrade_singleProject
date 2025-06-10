using Blade.Effects;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

namespace _01_Scripts.Effect
{
    public class PoolingEffect : MonoBehaviour, IPoolable
    {
        [field : SerializeField] public PoolItemSO PoolItem { get; private set; }

        public GameObject GameObject => gameObject;

        [SerializeField] private GameObject effectObject;
        private Pool _myPool;
        private ParticleSystem _playParticle;

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
            _playParticle = effectObject.GetComponent<ParticleSystem>();
            Debug.Assert(_playParticle != null, "effect object must have ParticleSystem component");
        }

        public void ResetItem()
        {
            _playParticle.Stop();
        }

        public void PlayerVFX(Vector3 position, Quaternion rotation)
        {
            _playParticle.Play();
        }

        private void OnValidate()
        {
            if (effectObject == null) return;
            _playParticle = effectObject.GetComponent<ParticleSystem>();
            if(_playParticle == null)
            {
                effectObject = null;
                Debug.LogError("effect object must have ParticleSystem component");
            }
        }
    }
}
