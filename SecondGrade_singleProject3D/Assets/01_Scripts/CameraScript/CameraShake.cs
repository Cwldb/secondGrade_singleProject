using System.Collections;
using _01_Scripts.Core;
using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;

namespace _01_Scripts.CameraScript
{
    public class CameraShake : MonoSingleton<CameraShake>
    {
        private CinemachineBasicMultiChannelPerlin virtualCamera;

        private void Start()
        {
            virtualCamera = GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake()
        {
            virtualCamera.AmplitudeGain = 3;
            virtualCamera.FrequencyGain = 3;
            StartCoroutine(EndShake());
        }

        private IEnumerator EndShake()
        {
            yield return new WaitForSeconds(0.1f);
            virtualCamera.AmplitudeGain = 0;
            virtualCamera.FrequencyGain = 0;
        }
    }
}