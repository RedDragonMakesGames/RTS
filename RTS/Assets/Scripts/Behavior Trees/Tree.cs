using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        protected Node mRoot = null;

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

        protected void FixedUpdate()
        {
            mRoot.Evaluate();
        }

        protected abstract Node SetUpTree(); //This is overloaded with the tree structure you want
    }
}

