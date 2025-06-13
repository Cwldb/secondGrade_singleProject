using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "NavAgentEnableAction", story: "Set [Self] NavAgent is [Enable]", category: "Action", id: "69819ba424ce98b1a7ee3ccd645f6202")]
public partial class NavAgentEnableAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<bool> Enable;

    protected override Status OnStart()
    {
        Self.Value.GetComponent<NavMeshAgent>().enabled = Enable.Value;
        return Status.Success;
    }
}

