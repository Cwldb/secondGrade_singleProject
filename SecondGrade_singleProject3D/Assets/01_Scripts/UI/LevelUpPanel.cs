using System;
using System.Collections.Generic;
using _01_Scripts.Core;
using _01_Scripts.Entities;
using _01_Scripts.Players;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _01_Scripts.UI
{
    public class LevelUpPanel : MonoBehaviour
    {
        [SerializeField] private GameObject selects;
        [SerializeField] private TMP_Text levelText;
        
        [Header("Icons")]
        [SerializeField] private Sprite[] levelUpIcons;
        
        [Header("Description")]
        [SerializeField] private string[] descriptionTexts;
        
        private PlayerStat _playerStat;
        private Entity _entity;
        
        private Dictionary<string, Action> _statIncreases;
        private List<string> _selectedStats;
        private List<int> _selectedIndices;

        private void Start()
        {
            GameManager.Instance.OnLevelUp += EnablePanels;
            GameManager.Instance.OnEnemyCount += LevelUpText;
            
            _entity = GameManager.Instance.PlayerFinder.Target;
            _playerStat = GameManager.Instance.PlayerFinder.Target.GetCompo<PlayerStat>();
            
            _statIncreases = new Dictionary<string, Action>
            {
                { "Damage", () => _playerStat.Damage *= 1.15f },
                { "AttackSpeed", () => _playerStat.AttackSpeed += -0.1f },
                { "CritPer", () => _playerStat.CritPer += 0.05f },
                { "CritPower", () => _playerStat.CritPower += 0.25f },
                { "Health", () =>
                    {
                        _playerStat._healthCompo.maxHealth += 25;
                        _playerStat._healthCompo.currentHealth += 25;
                        _entity.OnHitEvent.Invoke();
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
            _selectedIndices = new List<int>();
            
            while (_selectedStats.Count < 3)
            {
                int randIndex = Random.Range(0, statNames.Count);
                string stat = statNames[randIndex];

                if (!_selectedStats.Contains(stat))
                {
                    _selectedStats.Add(stat);
                    _selectedIndices.Add(randIndex);
                }
            }
        }
        
        private void EnablePanels()
        {
            Transform trm = selects.transform;
            Sequence seq = DOTween.Sequence();
            selects.SetActive(true);
            
            ChooseRandomStats();
            
            for (int i = 0; i < selects.transform.childCount; i++)
            {
                var child = selects.transform.GetChild(i);
                child.GetComponentInChildren<TMP_Text>().text = _selectedStats[i];

                var image = child.transform.GetChild(2).GetComponent<Image>();
                if (image != null)
                    image.sprite = levelUpIcons[_selectedIndices[i]];
                
                var description = child.transform.GetChild(1).GetComponent<TMP_Text>();
                if(description != null)
                    description.text = descriptionTexts[_selectedIndices[i]];
            }
            seq.SetUpdate(true);
            Time.timeScale = 0;
            
            seq.Append(trm.GetChild(0).transform.DOMoveY(540, 0.4f));
            seq.Append(trm.GetChild(1).transform.DOMoveY(540, 0.4f));
            seq.Append(trm.GetChild(2).transform.DOMoveY(540, 0.4f));
            seq.Play();
        }

        private void DisablePanels()
        {
            Transform trm = selects.transform;
            Sequence seq = DOTween.Sequence();

            seq.SetUpdate(true);
            seq.Append(trm.GetChild(0).transform.DOMoveY(1400, 0.4f));
            seq.Append(trm.GetChild(1).transform.DOMoveY(1400, 0.4f));
            seq.Append(trm.GetChild(2).transform.DOMoveY(1400, 0.4f));
            seq.OnComplete(() =>
            {
                selects.SetActive(false);
                Time.timeScale = 1;
            });
        }

        public void Select1()
        {
            selects.transform.GetChild(0).transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f).SetUpdate(true).OnComplete(() =>
            {
                ApplyStatIncrease(0);
                DisablePanels();
            });
        }

        public void Select2()
        {
            
            selects.transform.GetChild(1).transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f).SetUpdate(true).OnComplete(() =>
            {
                ApplyStatIncrease(1);
                DisablePanels();
            });
        }

        public void Select3()
        {
            selects.transform.GetChild(2).transform.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f).SetUpdate(true).OnComplete(() =>
            {
                ApplyStatIncrease(2);
                DisablePanels();
            });
        }
        
        private void ApplyStatIncrease(int index)
        {
            string statName = _selectedStats[index];
            _statIncreases[statName]?.Invoke();
        }
    }
}
