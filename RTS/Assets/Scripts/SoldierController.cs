using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public Vector3 moveTarget = Vector3.zero;
    public float crowdingDistance = 2.0f;
    public float moveFinishedDistance = 2.0f;
    private bool bMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (bMoving)
        {
            Quaternion rot = Quaternion.LookRotation(moveTarget - transform.position);
            this.transform.rotation = rot;
            if ((moveTarget - this.transform.position).magnitude > moveFinishedDistance)
            {
                this.transform.position += (moveTarget - this.transform.position).normalized * moveSpeed;
            }
            else
            {
                bMoving = false;
            }
        }
        KeepDistance();
    }

    private void KeepDistance()
    {
        float minDistance = crowdingDistance;
        Vector3 closestUnit = Vector3.zero;
        Collider closestCollider = null;
        Collider[] objects = Physics.OverlapSphere(transform.position, crowdingDistance);
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject == this.gameObject)
                break;
            if (objects[i].gameObject.GetComponent<SoldierController>() != null)
            {
                if (Vector3.Magnitude(transform.position - objects[i].transform.position) < minDistance)
                {
                    minDistance = Vector3.Magnitude(transform.position - objects[i].transform.position);
                    closestUnit = objects[i].transform.position;
                    closestCollider = objects[i];
                }
            }
        }

        //Move away from the nearest unit
        if (closestUnit != Vector3.zero)
        {
            Vector3 difference = (closestUnit - this.transform.position).normalized;
            difference = new Vector3(difference.x, 0, difference.z);
            this.transform.position -=  difference * moveSpeed * 0.6f;
            closestCollider.transform.position += difference * moveSpeed * 0.6f;
        }
    }

    public void MoveTo(Vector3 pos)
    {
        moveTarget = pos;
        bMoving = true;
    }
}
