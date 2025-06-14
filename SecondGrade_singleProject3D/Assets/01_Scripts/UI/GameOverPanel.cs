using System;
using _01_Scripts.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _01_Scripts.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject overPanel;
        [SerializeField] private Image fadeImage;

        private void Start()
        {
            
            GameManager.Instance.OnGameOver += HandleGameOver;
        }

        private void HandleGameOver()
        {
            Time.timeScale = 0;
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1, 0.7f).SetUpdate(true).OnComplete(() => SceneManager.LoadScene("GameOverScene"));
        }
    }
}