using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.States
{
    public class PlayerCanAttackState : PlayerState
    {
        private readonly PlayerEnemyDetect _detect;
        private readonly PlayerFire _playerFire;
        private readonly PlayerStat _playerStat;

        private float _curTime;
        private bool _hasFired;

        private const float FireDelay = 0.2f;
        private const float ResetDelay = 0.4f;

        public PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _detect = entity.GetCompo<PlayerEnemyDetect>();
            _playerFire = entity.GetCompo<PlayerFire>();
            _playerStat = entity.GetCompo<PlayerStat>();
        }

        public override void Update()
        {
            if (_detect.Colliders.Length > 0)
            {
                _curTime += Time.deltaTime;

                if (_curTime >= _playerStat.AttackSpeed)
                {
                    _movement.RotateTarget();
                    HandleAttackTiming();
                }
            }
            else
            {
                _curTime = 0;
                _hasFired = false;
            }
        }

        private void HandleAttackTiming()
        {
            if (!_movement.CanShoot) return;

            if (!_hasFired && _curTime >= _playerStat.AttackSpeed + FireDelay)
            {
                _playerFire.FireBullet(_detect.DamageCalc());
                _hasFired = true;
            }

            if (_curTime >= _playerStat.AttackSpeed + ResetDelay)
            {
                _curTime = 0;
                _movement.CanShoot = false;
                _hasFired = false;
            }
        }
    }
}