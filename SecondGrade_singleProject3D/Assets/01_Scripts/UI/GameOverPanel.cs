using System;
using _01_Scripts.Core;
using UnityEngine;

namespace _01_Scripts.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject overPanel;

        private void Start()
        {
            GameManager.Instance.OnGameOver += HandleGameOver;
        }

        private void HandleGameOver()
        {
            overPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}