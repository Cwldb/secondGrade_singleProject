using System;
using _01_Scripts.Core;
using UnityEngine;
using DG.Tweening;

namespace _01_Scripts.UI
{
    public class SettingPanelToggle : MonoSingleton<SettingPanelToggle>
    {
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private GameObject detailsPanel;

        [SerializeField] private GameObject clickBlockPanel;
        
        [SerializeField] private float value;

        private bool onSettingPanel = false;
        private bool ondetailsUI = false;

        private Vector3 _settingOldPos;
        private Vector3 _detailsOldPos;

        private void Awake()
        {
            settingPanel.SetActive(false);
            detailsPanel.SetActive(false);
            clickBlockPanel.SetActive(false);
        }

        private void Start()
        {
            _settingOldPos = settingPanel.transform.position;
            _detailsOldPos = detailsPanel.transform.position;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (!onSettingPanel)
                    if(!ondetailsUI) OpenSettingPanel();
                    else OpendetailsSetting();

                else
                if (!ondetailsUI) CloseSettingPanel();
                else ClosedetailsSetting();
            }
        }


        public void ExitGame() => Application.Quit();

        public void OpenSettingPanel()
        {
            onSettingPanel = true;
            settingPanel.SetActive(true);
            clickBlockPanel.SetActive(true);
            settingPanel.transform.DOMoveX(250, 0.2f);
        }

        public void OpendetailsSetting()
        {
            ondetailsUI = true;
            detailsPanel.SetActive(true);
            detailsPanel.transform.DOMoveX(425, 0.2f);
        }

        public void CloseSettingPanel()
        {
            onSettingPanel = false;
            settingPanel.transform.DOMoveX(_settingOldPos.x,0.2f)
                .OnComplete(() =>
                {
                    settingPanel.SetActive(false); 
                    clickBlockPanel.SetActive(false);
                });
        }

        public void ClosedetailsSetting()
        {
            ondetailsUI = false;
            detailsPanel.transform.DOMoveX(_detailsOldPos.x, 0.2f)
                .OnComplete(() =>
                {
                    detailsPanel.SetActive(false);
                });
        }
    }
}
