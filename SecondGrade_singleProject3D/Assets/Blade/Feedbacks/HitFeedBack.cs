using System;
using _01_Scripts.Core;
using _01_Scripts.Effect;
using _01_Scripts.Entities;
using Assets.Blade.Feedbacks;
using KJYLib.Dependencies;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

namespace Blade.Feedbacks
{
    public class HitFeedBack : FeedBack
    {
        [SerializeField] private PoolItemSO hitImpact;
        [SerializeField] private float playDuration = 0.5f;

        [Inject] private PoolManagerMono _poolManager;

        private PoolingEffect _effect;

        private void OnEnable()
        {
            if(_effect == null)
                Injector.InjectTo(this);
        }

        public async override void CreateFeedback()
        {
            PoolingEffect effect = _poolManager.Pop<PoolingEffect>(hitImpact);
            Quaternion rotation = Quaternion.LookRotation(transform.position - GameManager.Instance.PlayerFinder.Target.transform.position);

            effect.PlayerVFX(transform.position, rotation);

            await Awaitable.WaitForSecondsAsync(playDuration);
            _poolManager.Push(effect);
        }

        public override void StopFeedback()
        {
            
        }
    }
}