using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera camera1;
    public static Selector instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        camera1 = Camera.main;   
    }
    void Update()
    {
        
    }
    public Vector3 GetCurrentTilePosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0, -99, 0);
        }
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = camera1.ScreenPointToRay(Input.mousePosition);

        float rayOut = 0.0f;
        if (plane.Raycast(ray, out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.05f, 0.0f, 0.05f);
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0.0f, Mathf.CeilToInt(newPos.z));
            return newPos;
        }
        return new Vector3(0, -99, 0);
    }
}
