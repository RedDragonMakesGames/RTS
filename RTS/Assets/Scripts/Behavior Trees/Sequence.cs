using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }

        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool childrenAreRunning = false;

            foreach (Node node in mChildren)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Stop:
                        state = NodeState.Stop;
                        return state;

                    case NodeState.NodeComplete:
                        continue;

                    case NodeState.Running:
                        childrenAreRunning = true;
                        continue;

                    default:
                        Debug.LogError("Bad return from evaluate");
                        state = NodeState.Stop;
                        return NodeState.Stop;
                }
            }

            if (childrenAreRunning)
            {
                state = NodeState.Running;
                return state;
            }
            else
            {
                state = NodeState.NodeComplete;
                return state;
            }
        }
    }
}
