using _01_Scripts.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsInBossSpinCondition", story: "[Target] is in [Self] spinRange", category: "Conditions", id: "651c2ebec16e78ba9f174f99d419c1e7")]
public partial class IsInBossSpinCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EnemyBoss> Self;

    public override bool IsTrue()
    {
        float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
        return distance < Self.Value.jumpRange;
    }
}
