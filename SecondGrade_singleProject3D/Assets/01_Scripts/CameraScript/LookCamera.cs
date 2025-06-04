using System;
using UnityEngine;

namespace _01_Scripts.CameraScript
{
    public class LookCamera : MonoBehaviour
    {
        private Camera targetCamera; // 따라볼 카메라 (지정하지 않으면 메인 카메라)

        void Start()
        {
            targetCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (targetCamera != null)
            {
                transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                    targetCamera.transform.rotation * Vector3.up);
                transform.forward = targetCamera.transform.forward;
            }
        }
    }
}