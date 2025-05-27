using System;
using System.Collections.Generic;
using System.Linq;
using _01_Scripts.Entities;
using UnityEngine;

public class RagDollCompo : MonoBehaviour, IEntityComponent
{
    [SerializeField] private Transform ragDollParentTrm;
    [SerializeField] private LayerMask whatIsBody;

    private List<RagDollPart> _partList;
    private Collider[] _results;
    private RagDollPart _defaultPart;
    private EntityActionData _actionData;

    public void Initialize(Entity entity)
    {
        _actionData = entity.GetCompo<EntityActionData>();
        _results = new Collider[1];
        _partList = ragDollParentTrm.GetComponentsInChildren<RagDollPart>().ToList();
        foreach (RagDollPart part in _partList)
        {
            part.Initialize();
        }
        Debug.Assert(_partList.Count > 0, $"No ragdoll part found int {gameObject.name}");
        _defaultPart = _partList[0];

        entity.OnDeadEvent.AddListener(HandleDeadEvent);

        SetRagDollActive(false);
        SetColliderActive(false);
    }

    private void HandleDeadEvent()
    {
        SetRagDollActive(true);
        SetColliderActive(true);

        const float force = -30f;
        AddForceToRagDoll(_actionData.HitNormal * force, _actionData.HitPoint);
    }

    private void SetColliderActive(bool isActive)
    {
        foreach (RagDollPart part in _partList)
        {
            part.SetRagDollActive(isActive);
        }
    }

    private void SetRagDollActive(bool isActive)
    {
        foreach (RagDollPart part in _partList)
        {
            part.SetCollider(isActive);
        }
    }

    public void AddForceToRagDoll(Vector3 force, Vector3 point)
    {
        const float radius = 0.5f;
        int count = Physics.OverlapSphereNonAlloc(point, radius, _results, whatIsBody);
        if (count > 0)
        {
            _results[0].GetComponent<RagDollPart>().KnockBack(force, point);
        }
        else
        {
            _defaultPart.KnockBack(force, point);
        }
    }
}
