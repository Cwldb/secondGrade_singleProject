using _01_Scripts.Effect;
using _01_Scripts.Entities;
using Assets.Blade.Feedbacks;
using KJYLib.Dependencies;
using KJYLib.ObjectPool.RunTime;
using UnityEngine;

public class SlashEffectFeedback : FeedBack
{
    [SerializeField] private float effectDuration;
    [SerializeField] private EntityActionData actionData;
    [SerializeField] private PoolItemSO slashEffect;

    [Inject] private PoolManagerMono _poolManager;

    public override async void CreateFeedback()
    {
        PoolingEffect effect = _poolManager.Pop<PoolingEffect>(slashEffect);
        effect.PlayerVFX(actionData.HitPoint, Quaternion.identity);

        await Awaitable.WaitForSecondsAsync(effectDuration);
        _poolManager.Push(effect);
    }

    public override void StopFeedback()
    {

    }
}
