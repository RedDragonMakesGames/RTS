using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node mRoot = null;

        // Start is called before the first frame update
        void Start()
        {
            mRoot = SetUpTree();
            if (mRoot == null )
            {
                Debug.Log("Error, no tree found");
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            mRoot.Evaluate();
        }

        protected abstract Node SetUpTree(); //This is overloaded with the tree structure you want
    }
}

