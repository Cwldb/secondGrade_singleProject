using System;
using System.Collections.Generic;
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
        [SerializeField] private EntityStat targetStat;
        [SerializeField] private StatSO[] statCompos;

        private int _randIndex;

        private void RandomIndex()
        {
            // StatSO의 BaseValue를 조정해서 스탯값 올리기 AddModifier는 장비같은 일정 시간동안만 막 하는 그런거 ㅇㅋ?
            _randIndex = Random.Range(0, statCompos.Length);
        }

        private void Update()
        {
            RandomIndex();
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                StatSO stat = statCompos[0];
                targetStat.SetBaseValue(stat, stat.BaseValue += 10);
            }
        }

        public void Select1()
        {
            
        }

        public void Select2()
        {
            
        }

        public void Select3()
        {
            
        }
    }
}
