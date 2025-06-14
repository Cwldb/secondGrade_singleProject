using Blade.Enemies;
using System;
using _01_Scripts.Enemy;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInAttack", story: "[Target] is in [Self] attackRange", category: "Enemy/Conditions", id: "114149d5f20358a12dc4d9fa3df73fb1")]
public partial class IsInAttackCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Enemy> Self;

    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
        return distance < Self.Value.attackRange - 0.4f; // Ž�� �Ÿ����� �ִ°�?
    }
}
