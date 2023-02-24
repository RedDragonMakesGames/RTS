using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.Events;

public class SoldierAITree : BehaviourTree.Tree
{
    public float moveSpeed = 0.1f;
    public Vector3 moveTarget = Vector3.zero;
    public Transform soldierTransform;

    public UnityEvent<Vector3> MouseMoved = new UnityEvent<Vector3>();

    protected override Node SetUpTree()
    {
        soldierTransform = transform;
        Node root = new MoveToMouse(moveSpeed, moveTarget, soldierTransform, MouseMoved);
        return root;
    }

    public void SetMoveTarget(Vector3 pos)
    {
        MouseMoved.Invoke(pos);
    }
}
