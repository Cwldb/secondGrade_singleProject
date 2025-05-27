using System;
using _01_Scripts.Entities;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Blade.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "WaitForTrigger", story: "Wait for [Trigger] end", category: "Enemy/Animation", id: "47077d963309538f19065133d74031ef")]
    public partial class WaitForTriggerAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> Trigger;

        private bool _isTriggered;

        protected override Status OnStart()
        {
            _isTriggered = false;
            Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return _isTriggered ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
            Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd;
        }

        private void HandleAnimationEnd() => _isTriggered = true;

    }
}