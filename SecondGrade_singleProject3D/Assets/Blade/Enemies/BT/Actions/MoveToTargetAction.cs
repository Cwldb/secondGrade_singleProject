using Blade.Enemies;
using System;
using _01_Scripts.Enemy;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Blade.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "MoveToTarget", story: "[Movement] move to [Target]", category: "Enemy/Move", id: "455645f8abef2b7534201696def0451c")]
    public partial class MoveToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            if(!Movement.Value.Entity.IsDead)
                Movement.Value.SetDestination(Target.Value.position);
            return Status.Success;
        }
    }
}