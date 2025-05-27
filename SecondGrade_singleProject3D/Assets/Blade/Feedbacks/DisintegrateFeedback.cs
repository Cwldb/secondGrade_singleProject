using Assets.Blade.Feedbacks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class DisintegrateFeedback : FeedBack
{
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private VisualEffect feedbackEffect;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private float endDissolveValue = 0f;

    private bool _isAlreadyStart = false;

    private readonly int _dissolveHeight = Shader.PropertyToID("_DissolveHeight");
    private readonly int _isDissolve = Shader.PropertyToID("_IsDissolove");

    public UnityEvent FeedbackComplete;

    public override void CreateFeedback()
    {
        if (_isAlreadyStart) return;

        _isAlreadyStart = true;
        meshRenderer.material.SetInt(_isDissolve, 1);   
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(delayTime);
        seq.AppendCallback(() => feedbackEffect.Play());
        seq.Append(meshRenderer.material.DOFloat(endDissolveValue, _dissolveHeight, 1.2f));
        seq.AppendInterval(2f);
        seq.OnComplete(() => FeedbackComplete?.Invoke());
    }

    public override void StopFeedback()
    {
        throw new System.NotImplementedException();
    }
}
