using System;
using System.Threading;
using _01_Scripts.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _01_Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text endText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleGameEnd;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= HandleGameEnd;
        }

        private void HandleGameEnd(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("Game Over");
            descriptionText.text = $"{TimerManager.Instance.Minutes} : {TimerManager.Instance.Count}";
            
            endText.DOFade(1, 1f)
                .OnComplete(() =>
                {
                    descriptionText.DOFade(1, 0.7f).OnComplete(() =>
                    {
                        restartButton.gameObject.SetActive(true);
                        exitButton.gameObject.SetActive(true);
                    });
                });
        }

        public void Restart()
        {
            TimerManager.Instance.Minutes = 0;
            TimerManager.Instance.Count = 0;
            TimerManager.Instance.isDeath = false;
            TimerManager.Instance.isBossSpawn = false;
            TimerManager.Instance.is1Minute = false;
            TimerManager.Instance.is2Minute = false;
            TimerManager.Instance.is3Minute = false;
            TimerManager.Instance.is4Minute = false;
            TimerManager.Instance.is5Minute = false;
        }
    }
}
