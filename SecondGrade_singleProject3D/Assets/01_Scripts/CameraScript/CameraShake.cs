using System.Collections;
using _01_Scripts.Core;
using UnityEngine;

namespace _01_Scripts.CameraScript
{
    public class CameraShake : MonoSingleton<CameraShake>
    {
        private Vector3 originalPos;
        private Coroutine currentShake;

        public void Shake(float duration = 0.2f, float magnitude = 0.3f)
        {
            if (currentShake != null)
                StopCoroutine(currentShake);

            currentShake = StartCoroutine(ShakeCoroutine(duration, magnitude));
        }

        private IEnumerator ShakeCoroutine(float duration, float magnitude)
        {
            Transform camTransform = Camera.main.transform;
            originalPos = camTransform.position;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float offsetX = Random.Range(-1f, 1f) * magnitude;
                float offsetY = Random.Range(-1f, 1f) * magnitude;

                camTransform.position = originalPos + new Vector3(offsetX, offsetY, 0f);

                elapsed += Time.deltaTime;
                yield return null;
            }

            camTransform.position = originalPos;
        }
    }
}