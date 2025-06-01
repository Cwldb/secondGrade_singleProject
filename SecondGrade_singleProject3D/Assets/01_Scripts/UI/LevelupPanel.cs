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
        [SerializeField] private PlayerStat targetStat;
        
        //private List<float> _statValues = new List<float>();
        
        private int _randIndex;

        private void Start()
        {
            RandomIndex();
        }

        private void RandomIndex()
        {
            //_randIndex = Random.Range(0, _statValues.Count);
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
