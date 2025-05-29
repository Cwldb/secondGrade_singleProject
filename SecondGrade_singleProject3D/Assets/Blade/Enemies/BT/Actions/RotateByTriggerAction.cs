using Blade.Enemies;
using System;
using _01_Scripts.Enemy;
using _01_Scripts.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Blade.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "RotateByTrigger", story: "[Movement] rotate [Target] by [Trigger]", category: "Enemy/Move", id: "2a90514f73e457d06675db904bfb0b7a")]
    public partial class RotateByTriggerAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;

        private bool _isRotate;

        protected override Status OnStart()
        {
            _isRotate = false;
            Trigger.Value.OnManualRotationTrigger += HandleManualRotation;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_isRotate)
            {
                Movement.Value.LookAtTarget(Target.Value.position);
            }
            return Status.Running;
        }

        protected override void OnEnd()
        {
            Trigger.Value.OnManualRotationTrigger -= HandleManualRotation;
        }

        private void HandleManualRotation(bool isRotate) => _isRotate = isRotate;
    }
}