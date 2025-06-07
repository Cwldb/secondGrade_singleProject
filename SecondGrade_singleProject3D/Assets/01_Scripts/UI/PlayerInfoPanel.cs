using _01_Scripts.Core;
using _01_Scripts.Players;
using TMPro;
using UnityEngine;

namespace _01_Scripts.UI
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text aspdText;
        [SerializeField] private TMP_Text criText;
        [SerializeField] private TMP_Text dcriText;
        
        private PlayerStat _playerStat;

        private void Start()
        {
            _playerStat = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerStat>();
            _playerStat.OnStatValueChanged += UpdateStat;
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
