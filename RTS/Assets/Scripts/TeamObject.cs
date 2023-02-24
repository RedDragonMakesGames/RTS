using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "Team", order = 1)]

public class TeamObject : ScriptableObject
{
    public string teamName;
    public Material colour;
}
