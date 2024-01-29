using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenuAttribute(fileName = "BuildingPreset", menuName = "New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public int cost;
    public int costPernTurn;
    public GameObject prefab;

    public int population;
    public int jobs;
    public int food;
    public int wood;
}
