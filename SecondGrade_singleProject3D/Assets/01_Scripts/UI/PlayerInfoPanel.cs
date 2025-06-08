using System;
using _01_Scripts.Core;
using _01_Scripts.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _01_Scripts.UI
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text aspdText;
        [SerializeField] private TMP_Text criText;
        [SerializeField] private TMP_Text dcriText;
        
        [Header("Skills")]
        [SerializeField] private Image skill1Image;
        [SerializeField] private Image skill2Image;
        
        private PlayerStat _playerStat;
        private PlayerSkillSet _playerSkillSet;

        private void Start()
        {
            _playerStat = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerStat>();
            _playerSkillSet = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerSkillSet>();
            
            _playerStat.OnStatValueChanged += UpdateStat;
        }

        private void Update()
        {
            if(_playerSkillSet != null)
                skill1Image.fillAmount = _playerSkillSet._currentCooldown / _playerSkillSet.cooldownTime;
        }

        private void UpdateStat()
        {
            atkText.text = $"{_playerStat.Damage}";
            aspdText.text = $"{_playerStat.AttackSpeed}";
            criText.text = $"{_playerStat.CritPer * 100}";
            dcriText.text = $"{_playerStat.CritPower}";
        }
    }
}
