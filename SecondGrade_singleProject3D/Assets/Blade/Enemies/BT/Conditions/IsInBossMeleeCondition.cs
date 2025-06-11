using _01_Scripts.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInBossMeleeCondition", story: "[Target] is in [Self] meleeRange", category: "Conditions", id: "e192019e7b62d959833773304a0c6298")]
public partial class IsInBossMeleeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyBoss> Self;

    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
        return distance < Self.Value.meleeRange;
    }
}
