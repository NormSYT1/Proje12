using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFoods;
    public int curWoods;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJobs;
    public TextMeshProUGUI statsText;
    public List<Building> buildings = new List<Building>();

    public static City instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UpdateStatText();
    }
    void Update()
    {
        
    }
    public void OnPlaceBuilding(Building building)
    {
        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }
    public void OnRemoveBuilding(Building building)
    {
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }
    public void UpdateStatText()
    {
        statsText.text = string.Format("Day: {0}  Money: {1}  Population: {2}/{3}  Jobs: {4}/{5}  Food: {6}  Woods: {7} ", new object[8] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFoods, curWoods });
    }
    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CaltulateWood();
        UpdateStatText();
    }
    public void CalculateMoney()
    {
        money += curJobs * incomePerJobs;
        foreach (Building building in buildings)
        {
            money -= building.preset.costPernTurn;
        }
    }
    public void CalculatePopulation()
    {
        if (curFoods >= curPopulation && curPopulation < maxPopulation)
        {
            curFoods -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFoods / 4), maxPopulation);
        }
        else if (curFoods < curPopulation)
        {
            curPopulation -= curFoods;
        } 
    }
    public void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }
    public void CalculateFood()
    {
        curFoods = 0;
        foreach (Building building in buildings)
        {
            curFoods += building.preset.food;
        }
    }
    public void CaltulateWood()
    {
        curWoods = 0;
        foreach (Building building in buildings)
        {
            curWoods += building.preset.wood;
        }
    }
}
