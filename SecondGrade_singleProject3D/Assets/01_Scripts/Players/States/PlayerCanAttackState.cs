using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.States
{
    public class PlayerCanAttackState : PlayerState
    {
        private PlayerEnemyDetect _detect;
        private Player _player;
        private PlayerFire _playerFire;
        
        private float _curTime = 0;
        
        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _detect = entity.GetCompo<PlayerEnemyDetect>();
            _playerFire = entity.GetCompo<PlayerFire>();
        }

        public override void Update()
        {
            if (_detect != null)
            {
                if (_detect.Colliders.Length > 0)
                {
                    _curTime += Time.deltaTime;
                    if (_curTime >= _playerFire.attackSpeed.Value)
                    {
                        _curTime = 0;
                        _playerFire.FireBullet();
                    }
                }
            }
        }
    }
}