using Blade.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using _01_Scripts.Entities;

namespace Blade.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetComponents", story: "Get components from [Self]", category: "Enemy", id: "402600ce6910e4e5a166baf43ab0d8d5")]
    public partial class GetComponentsAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        protected override Status OnStart()
        {
            Enemy enemy = Self.Value;
            SetVariable(enemy, "Target", enemy.PlayerFinder.Target.transform);

            List<BlackboardVariable> variableList = enemy.BtAgent.BlackboardReference.Blackboard.Variables;

            foreach (var variable in variableList)
            {
                if(typeof(IEntityComponent).IsAssignableFrom(variable.Type) == false) continue;

                SetComponent(enemy, variable.Name, enemy.GetCompo(variable.Type));
            }
            return Status.Success;
        }

        private void SetComponent(Enemy enemy, string varName, IEntityComponent component)
        {
            if (enemy.BtAgent.GetVariable(varName, out BlackboardVariable variable))
            {
                variable.ObjectValue = component;
            }
        }

        private void SetVariable<T>(Enemy enemy, string varName, T component)
        {
            Debug.Assert(component != null, $"Check {varName} int {enemy.name}");
            BlackboardVariable<T> target = enemy.GetBlackboardVariable<T>(varName);
            target.Value = component;
        }
    }
}