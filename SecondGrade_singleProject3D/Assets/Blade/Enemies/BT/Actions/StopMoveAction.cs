using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Blade.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StopMove", story: "Set [Movement] isStop to [NewValue]", category: "Enemy/Move", id: "9882da6592785c94a7b208f22562269d")]
    public partial class StopMoveAction : Action
    {
        [SerializeReference] public BlackboardVariable<NavMovement> Movement;
        [SerializeReference] public BlackboardVariable<bool> NewValue;

        protected override Status OnStart()
        {
            Movement.Value.SetStop(NewValue.Value);
            return Status.Success;
        }
    }
}