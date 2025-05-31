using System;
using _01_Scripts.Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01_Scripts.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private PlayerEnemyDetect detect;

        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                detect.damage += 10;
        }
    }
}
