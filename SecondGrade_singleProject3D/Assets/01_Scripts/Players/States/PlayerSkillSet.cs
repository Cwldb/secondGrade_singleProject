using System;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.States
{
    public class PlayerSkillSet : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float distance;
        
        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity.GetComponent<Player>();
        }

        private void Awake()
        {  
            _player.PlayerInput.OnActive1Pressed += UseActive1;
        }

        public void UseActive1()
        {
            
        }
    }
}