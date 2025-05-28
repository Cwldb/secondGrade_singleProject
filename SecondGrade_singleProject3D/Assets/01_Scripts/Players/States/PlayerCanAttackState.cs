using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.States
{
    public class PlayerCanAttackState : PlayerState
    {
        private PlayerEnemyDetect _detect;
        private Player _player;
        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _detect = entity.GetCompo<PlayerEnemyDetect>();
        }

        public override void Update()
        {
            if (_detect != null)
            {
                if (_detect.Colliders.Length > 0)
                {
                    
                }
            }
        }
    }
}