using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    public GameObject ground;

    private LineRenderer box = null;
    private Vector3 lastMousePos;
    private Vector3 boxStart;
    private Vector3 boxEnd;
    private bool bIsSelecting = false;
    private List<GameObject> selectedObjects = new();

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<LineRenderer>();
        box.positionCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            boxStart = Input.mousePosition;
            bIsSelecting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            boxEnd = Input.mousePosition;
            bIsSelecting = false;
            SelectContents();
        }

        if (Input.GetMouseButton(1))
            SetMoveTarget();

        if (bIsSelecting)
        {
            //Draw selection box
            Vector3[] points = { boxStart, new Vector3(boxStart.x, Input.mousePosition.y), Input.mousePosition, new Vector3(Input.mousePosition.x, boxStart.y), boxStart };
            for (int i = 0; i < 5; i++)
            {
                points[i] = Camera.main.ScreenToWorldPoint(new Vector3(points[i].x, points[i].y, Camera.main.nearClipPlane + 5));
            }
            box.SetPositions(points);
            box.enabled = true;
        }
        else
        {
            box.enabled = false;
        }

        lastMousePos = Input.mousePosition;
    }

    void SelectContents()
    {
        selectedObjects.Clear();
        //Select all selectable objects in the area
        SelectableComponent[] minions = FindObjectsOfType<SelectableComponent>();
        for (int i = 0; i < minions.Length; i++)
        {
            float x1 = Camera.main.ScreenToViewportPoint(boxStart).x;
            float x2 = Camera.main.ScreenToViewportPoint(boxEnd).x;
            float xPos = Camera.main.WorldToViewportPoint(minions[i].gameObject.transform.position).x;
            if ((x1 < xPos && xPos < x2) || (x2 < xPos && xPos < x1))
            {
                //If the object's X pos is within the box
                float y1 = Camera.main.ScreenToViewportPoint(boxStart).y;
                float y2 = Camera.main.ScreenToViewportPoint(boxEnd).y;
                float yPos = Camera.main.WorldToViewportPoint(minions[i].gameObject.transform.position).y;

                if ((y1 < yPos && yPos < y2) || (y2 < yPos && yPos < y1))
                {
                    //If the position of the object is within the selection box
                    selectedObjects.Add(minions[i].gameObject);
                    //Show the selection ring
                    minions[i].gameObject.transform.Find("Selected Ring").gameObject.SetActive(true);
                }
                else
                {
                    //Hide the selection ring
                    minions[i].gameObject.transform.Find("Selected Ring").gameObject.SetActive(false);
                }
            }
            else
            {
                //Hide the selection ring
                minions[i].gameObject.transform.Find("Selected Ring").gameObject.SetActive(false);
            }
        }
    }

    void SetMoveTarget()
    {
        if (selectedObjects.Count == 0)
            return;

        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(pos, out hit);

        if (hit.collider == null)
        {
            Debug.LogWarning("Player clicked outside the playfield");
            return;
        }

        if (hit.collider.gameObject == ground)
        {
            for (int i = 0; i < selectedObjects.Count; i++)
            {
                SoldierAITree controller = selectedObjects[i].GetComponent<SoldierAITree>();
                if (controller != null)
                    controller.SetMoveTarget(hit.point);
            }
        }
        else if (false)
        {
            //TODO: Handle clicking on other things
        }
    }
}
