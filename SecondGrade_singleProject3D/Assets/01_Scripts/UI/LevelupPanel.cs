using System;
using System.Collections.Generic;
using _01_Scripts.Core;
using _01_Scripts.Entities;
using _01_Scripts.Players;
using KJYLib.StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace _01_Scripts.UI
{
    public class LevelupPanel : MonoBehaviour
    {
        [SerializeField] private GameObject selects;
        
        private PlayerStat _playerStat;
        
        private Dictionary<string, Action> _statIncreases;
        private List<string> _selectedStats;

        private void Start()
        {
            GameManager.Instance.OnLevelUp += EnablePanels;
            
            _playerStat = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerStat>();
            
            _statIncreases = new Dictionary<string, Action>
            {
                { "Damage", () => _playerStat.Damage += 1f },
                { "CritPer", () => _playerStat.CritPer += 0.05f },
                { "CritPower", () => _playerStat.CritPower += 0.1f },
                { "AttackSpeed", () => _playerStat.AttackSpeed += -0.1f }
            };
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

            Debug.Log($"{_selectedStats[0]}, {_selectedStats[1]}, {_selectedStats[2]}");
        }
        
        private void EnablePanels()
        {
            // 나중에 DOTween 사용
            selects.SetActive(true);
            ChooseRandomStats();
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
