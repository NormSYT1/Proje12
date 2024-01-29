using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;
    private float indicatorUpdateTime = 0.05f;
    private float lastUpdateTime;
    private Vector3 currentIndicatorPos;
    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacement();
        }
        if (Time.time - lastUpdateTime > indicatorUpdateTime) 
        {
            lastUpdateTime = Time.time;
            currentIndicatorPos = Selector.instance.GetCurrentTilePosition();
            if (currentlyPlacing)
            {
                placementIndicator.transform.position = currentIndicatorPos;
            }
            else if (currentlyBulldozering)
            {
                bulldozerIndicator.transform.position = currentIndicatorPos;
            }
        }
        if (Input.GetMouseButtonDown(0) && currentlyPlacing)
        {
            PlaceBuilding();
        }
        else if (Input.GetMouseButtonUp(0) && currentlyBulldozering)
        {
            Bulldozer();
        }
    }
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        if (City.instance.money < preset.cost)
        {
            return;
        }
        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);
    }
    public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }
    public void ToggleBulldozer()
    {
        currentlyBulldozering = !currentlyBulldozering;
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }
    public void PlaceBuilding()
    {
        GameObject buildingObject = Instantiate(curBuildingPreset.prefab, currentIndicatorPos, Quaternion.identity);
        City.instance.OnPlaceBuilding(buildingObject.GetComponent<Building>());
        CancelBuildingPlacement();
    }
    public void Bulldozer()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == currentIndicatorPos);
        if (buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
