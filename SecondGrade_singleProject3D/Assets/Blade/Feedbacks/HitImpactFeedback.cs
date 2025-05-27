using _01_Scripts.Entities;
using DG.Tweening;
using KJYLib.Dependencies;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

namespace Assets.Blade.Feedbacks
{
    public class HitImpactFeedback : FeedBack
    {
        [SerializeField] private PoolItemSO hitImpact;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private EntityActionData actionData;

        [Inject] private PoolManagerMono _poolManager;

        private PoolingEffect _effect;

        public override async void CreateFeedback()
        {
            PoolingEffect effect = _poolManager.Pop<PoolingEffect>(hitImpact);
            Quaternion rotation = Quaternion.LookRotation(actionData.HitNormal * -1);

            effect.PlayerVFX(actionData.HitPoint, rotation);

            await Awaitable.WaitForSecondsAsync(playDuration);
            _poolManager.Push(effect);

        }

        public override void StopFeedback()
        {

        }
    }
}