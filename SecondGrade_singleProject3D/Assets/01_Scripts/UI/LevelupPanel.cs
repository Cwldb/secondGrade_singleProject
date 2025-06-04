using System;
using System.Collections.Generic;
using _01_Scripts.Core;
using _01_Scripts.Entities;
using _01_Scripts.Players;
using KJYLib.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace _01_Scripts.UI
{
    public class LevelupPanel : MonoBehaviour
    {
        [SerializeField] private GameObject selects;
        [SerializeField] private TMP_Text levelText;
        
        private PlayerStat _playerStat;
        
        private Dictionary<string, Action> _statIncreases;
        private List<string> _selectedStats;

        private void Start()
        {
            GameManager.Instance.OnLevelUp += EnablePanels;
            GameManager.Instance.OnEnemyCount += LevelUpText;
            
            _playerStat = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerStat>();
            
            _statIncreases = new Dictionary<string, Action>
            {
                { "Damage", () => _playerStat.Damage += 1f },
                { "CritPer", () => _playerStat.CritPer += 0.05f },
                { "CritPower", () => _playerStat.CritPower += 0.1f },
                { "AttackSpeed", () => _playerStat.AttackSpeed += -0.1f },
                { "Health", () =>
                    {
                        _playerStat._healthCompo.maxHealth += 10;
                        _playerStat._healthCompo.currentHealth += 10;
                    }
                }
            };
            
            LevelUpText();
        }
        private void LevelUpText()
        {
            levelText.text = GameManager.Instance.GetCurNeedLevel().ToString();
        }
        
        private void ChooseRandomStats()
        {
            List<string> statNames = new List<string>(_statIncreases.Keys);
            _selectedStats = new List<string>();
            
            while (_selectedStats.Count < 3)
            {
                int index = Random.Range(0, statNames.Count);
                string stat = statNames[index];

                if (!_selectedStats.Contains(stat))
                    _selectedStats.Add(stat);
            }
        }
        
        private void EnablePanels()
        {
            // 나중에 DOTween 사용
            selects.SetActive(true);
            ChooseRandomStats();
            for(int i = 0; i < selects.transform.childCount; i++)
                selects.transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = _selectedStats[i];
            Time.timeScale = 0f;
        }

        private void DisablePanels()
        {
            // 나중에 DOTween 사용
            selects.SetActive(false);
            Time.timeScale = 1;
        }

        public void Select1()
        {
            ApplyStatIncrease(0);
            DisablePanels();
        }

        public void Select2()
        {
            ApplyStatIncrease(1);
            DisablePanels();
        }

        public void Select3()
        {
            ApplyStatIncrease(2);
            DisablePanels();
        }
        
        private void ApplyStatIncrease(int index)
        {
            string statName = _selectedStats[index];
            _statIncreases[statName]?.Invoke();
            selects.SetActive(false);
        }
    }
}
