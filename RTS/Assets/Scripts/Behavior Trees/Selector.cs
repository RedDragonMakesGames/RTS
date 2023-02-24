
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : Node
    {
        public Selector() : base() { }

        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in mChildren)
            {
                switch(node.Evaluate())
                {
                    case NodeState.Stop:
                        state = NodeState.Stop;
                        continue;

                    case NodeState.NodeComplete:
                        state = NodeState.NodeComplete;
                        return NodeState.NodeComplete;

                    case NodeState.Running: 
                        state = NodeState.Running;
                        return NodeState.Running;

                    default:
                        Debug.LogError("Bad return from evaluate");
                        state = NodeState.Stop;
                        return NodeState.Stop;
                }
            }
            state = NodeState.Stop;
            return NodeState.Stop;
        }
    }
}

