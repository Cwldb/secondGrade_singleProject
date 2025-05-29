using Blade.Enemies;
using System;
using _01_Scripts.Enemy;
using Unity.Behavior;
using UnityEngine;

namespace Blade.Enemies.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "IsInDetect", story: "[Target] is in [Self] detectRange", category: "Enemy/Conditions", id: "7f893d587deb985dd84ca90d98352a15")]
    public partial class IsInDetectCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        public override bool IsTrue()
        {
            float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
            return distance < Self.Value.detectRange; // Ž�� �Ÿ����� �ִ°�?
        }
    }
}