using _01_Scripts.Entities;
using _01_Scripts.FSM;
using KJYLib.Dependencies;
using System;
using _01_Scripts.Core;
using UnityEngine;

namespace _01_Scripts.Players
{
    
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
        
        private EntityStateMachine _stateMachine;

        [Provide]
        public Player ProvidePlayer() => this;

        #region Temp region

        public float rollingVelocity = 2.2f;

        #endregion
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
        }

        protected override void Start()
        {
            base.Start();
            _stateMachine.ChangeState("IDLE");
            OnDeadEvent.AddListener(HandleDeadEvent);
        }

        private void HandleDeadEvent()
        {
            _stateMachine.ChangeState("DEAD");
            PlayerInput.OnDisable();
            GameManager.Instance.OnGameOver.Invoke();
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
        
        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
        
    }
}