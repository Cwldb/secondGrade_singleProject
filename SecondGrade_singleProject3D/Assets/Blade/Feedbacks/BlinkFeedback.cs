using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Blade.Feedbacks;
using System.Threading.Tasks;
using UnityEngine;

public class BlinkFeedback : FeedBack
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private GameObject blinkEffectParent;
    [SerializeField] private float blinkDuration = 0.15f;
    [SerializeField] private float blinkIntensity = 0.2f;

    private List<MeshRenderer> meshList = new List<MeshRenderer>();
    private readonly int _blinkHash = Shader.PropertyToID("_BlinkValue");

    private void Start()
    {
        meshList = blinkEffectParent.GetComponentsInChildren<MeshRenderer>().ToList();
    }

    public override async void CreateFeedback()
    {
        foreach (var mesh in meshList)
            mesh.material.SetFloat(_blinkHash, blinkIntensity);
        meshRenderer.material.SetFloat(_blinkHash, blinkIntensity);
        await Awaitable.WaitForSecondsAsync(blinkDuration);
        StopFeedback();
    }

    public override void StopFeedback()
    {
        if (meshList != null)
        {
            foreach (var mesh in meshList)
                mesh.material.SetFloat(_blinkHash, 0f);
        }
        
        if(meshRenderer != null)
            meshRenderer.material.SetFloat(_blinkHash, 0f);
    }
}
