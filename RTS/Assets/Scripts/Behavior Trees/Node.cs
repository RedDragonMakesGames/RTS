using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public enum NodeState
    {
        Running,
        NodeComplete,
        Stop
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> mChildren = new List<Node>();

        private Dictionary<string, object> mData = new Dictionary<string, object>();

        public Node() 
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach(Node child in children) 
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            mChildren.Add(node);
        }

        public void SetData(string key, object value)
        {
            mData[key] = value;
        }

        public object GetData(string key)
        {
            if (mData.ContainsKey(key))
                return mData[key];

            if (parent == null) 
                return null;

            object parentData = parent.GetData(key);
            return parentData;
        }

        public bool ClearData(string key)
        {
            bool wasFound = false;
            if (mData.ContainsKey(key))
            {
                mData.Remove(key);
                wasFound = true;
            }

            if (parent!= null)
            {
                if (parent.ClearData(key))
                    wasFound = true;
            }

            return wasFound;
        }

        public virtual NodeState Evaluate()
        {
            return NodeState.Stop;
        }
    }
}
