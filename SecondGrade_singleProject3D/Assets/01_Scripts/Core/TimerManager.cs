using System;
using UnityEngine;

namespace _01_Scripts.Core
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        public float Count { get; set; }
        public int Minutes { get; set; }

        private void Update()
        {
            Count +=  Time.deltaTime;
            if (Count >= 60)
            {
                Minutes++;
                Count = 0;
            }
            
        }
    }
}