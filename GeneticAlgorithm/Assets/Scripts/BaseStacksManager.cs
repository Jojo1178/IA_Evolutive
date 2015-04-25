using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseStacksManager : MonoBehaviour {

    //public GameManager Manager;
    public enum RESOURCES_TYPE
    {
        WOOD,
        FOOD
    };
    public Dictionary<RESOURCES_TYPE, int> ResourcesStacked;
	// Use this for initialization
	void Start () {
        ResourcesStacked = new Dictionary<RESOURCES_TYPE, int>();
        ResourcesStacked.Add(RESOURCES_TYPE.WOOD , 0);
        ResourcesStacked.Add(RESOURCES_TYPE.FOOD, 0);
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
