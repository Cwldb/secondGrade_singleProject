using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.States
{
    public class PlayerCanAttackState : PlayerState
    {
        private PlayerEnemyDetect _detect;
        private PlayerFire _playerFire;
        private CharacterMovement _movement;
        
        private float _curTime = 0;
        private bool _isShoot = false;
        private bool _isDetected = false;
        
        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _detect = entity.GetCompo<PlayerEnemyDetect>();
            _movement = entity.GetCompo<CharacterMovement>();
            _playerFire = entity.GetCompo<PlayerFire>();
        }

        public override void Update()
        {
            if (_detect.Colliders.Length > 0)
            {
                _curTime += Time.deltaTime;
                if (_curTime >= _playerFire.attackSpeed.Value)
                {
                    _movement.RotateTarget();
                    if (_movement.CanShoot)
                    {
                        if(_curTime >= _playerFire.attackSpeed.Value + 0.2f)
                        {
                            if (!_isShoot)
                            {
                                _isShoot = true;
                                _playerFire.FireBullet();
                            }
                        }

                        if (_curTime >= _playerFire.attackSpeed.Value + 0.4f)
                        {
                            _movement.CanShoot = false;
                            _curTime = 0;
                            _isShoot = false;
                            _isDetected = false;
                        }
                            
                    }
                }
            }
            else
                _curTime = 0;
        }
    }
}