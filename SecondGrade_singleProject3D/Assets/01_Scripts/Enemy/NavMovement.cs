using _01_Scripts.Combat;
using _01_Scripts.Entities;
using DG.Tweening;
using KJYLib.StatSystem;
using UnityEngine;
using UnityEngine.AI;

namespace _01_Scripts.Enemy
{
    public class NavMovement : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private StatSO moveSpeedStat;
        [SerializeField] private float stopOffset = 0.05f;
        [SerializeField] private float rotationSpeed = 10f;
        
        private Entity _entity;
        private EntityStat _statCompo;

        public bool IsArrived => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + stopOffset;

        public float RemainDistance => agent.pathPending ? -1 : agent.remainingDistance;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }


        public void AfterInitialize()
        {
            agent.speed = _statCompo.SubscribeStat(moveSpeedStat, HandleMoveSpeedChange, 1f);
        }

        private void HandleMoveSpeedChange(StatSO stat, float currentValue, float prevValue)
        {
            agent.speed = currentValue;
        }

        private void OnDestroy()
        {
            _entity.transform.DOKill();
            _statCompo.UnSubscribeStat(moveSpeedStat, HandleMoveSpeedChange);
        }

        private void Update()
        {
            if (agent.hasPath && agent.isStopped == false && agent.path.corners.Length > 0)
            {
                LookAtTarget(agent.steeringTarget, true);
            }
        }

        public void LookAtTarget(Vector3 target, bool isSmooth = true)
        {
            Vector3 direction = target - _entity.transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            if (isSmooth)
                _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            else
                _entity.transform.rotation = lookRotation;
        }

        public void SetStop(bool isStop) => agent.isStopped = isStop;
        public void SetVelocity(Vector3 velocity) => agent.velocity = velocity;
        public void SetSpeed(float speed) => agent.speed = speed;
        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

    }
}