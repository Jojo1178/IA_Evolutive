using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseStacksManager : MonoBehaviour {

    //public GameManager Manager;
    public enum RESOURCES_TYPE
    {
        WOOD,
        FOOD,
        NONE
    };
    public Dictionary<RESOURCES_TYPE, int> ResourcesStacked;
    public Dictionary<RESOURCES_TYPE, int> ResourcesMaxValues;
    public List<Transform> BuildingPlacesEditor;
    public List<KeyValuePair<Vector3,bool>> BuildingPlaces;
    public int BaseMaxValue = 100;
	// Use this for initialization
	void Start () {
        ResourcesStacked = new Dictionary<RESOURCES_TYPE, int>();
        ResourcesStacked.Add(RESOURCES_TYPE.WOOD , 0);
        ResourcesStacked.Add(RESOURCES_TYPE.FOOD, 0);
        ResourcesMaxValues = new Dictionary<RESOURCES_TYPE, int>();
        ResourcesMaxValues.Add(RESOURCES_TYPE.WOOD, BaseMaxValue);
        ResourcesMaxValues.Add(RESOURCES_TYPE.FOOD, BaseMaxValue);

        BuildingPlaces = new List<KeyValuePair<Vector3, bool>>();
        foreach (Transform place in BuildingPlacesEditor)
            BuildingPlaces.Add(new KeyValuePair<Vector3, bool>(place.position,false));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddResources(RESOURCES_TYPE type, int value)
    {
        if (ResourcesStacked.ContainsKey(type))
            ResourcesStacked[type] += value;
        else
            ResourcesStacked.Add(type, value);
    }

    public void RemoveResources(RESOURCES_TYPE type, int value)
    {
        if (ResourcesStacked.ContainsKey(type))
        {
            if (ResourcesStacked[type] >= value)
                ResourcesStacked[type] -= value;
            else
                Debug.LogWarning("not enough " + type);
        }
        else
            Debug.LogWarning("Resource " + type + " not found");
    }
}
