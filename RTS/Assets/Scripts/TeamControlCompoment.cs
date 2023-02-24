using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamControlCompoment : MonoBehaviour
{
    public TeamObject startingTeam;
    public Material TeamUnselectedMaterial;

    private TeamObject currentTeam = null;
    // Start is called before the first frame update
    void Start()
    {
        SetTeam(startingTeam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeam(TeamObject team)
    {
        MeshRenderer[] meshes = transform.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            if (currentTeam == null)
            {
                if (meshes[i].sharedMaterial == TeamUnselectedMaterial)
                    meshes[i].material = team.colour;
            }
            else
            {
                if (meshes[i].sharedMaterial == currentTeam.colour)
                    meshes[i].material = team.colour;
            }
        }
        currentTeam = team;
    }
}
