using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAGathering : MonoBehaviour {

    public GameManager Manager;
    public IANavigationScript NavScript;
    public GameObject Base;

    [Range(0,1)]
    public int SeedKeyPriority;
    [Range(0, 50)]
    public int CriticalStockLevel;
    public List<BaseStacksManager.RESOURCES_TYPE> Priority;
    [Range(0, 10)]
    public int Distance = 5;
    public int[] Inventory;

    private enum GATHERING_STATE
    {
        COLLECTING_WOOD,
        COLLECTING_FOOD,
        BRINGTOBASE,
        UPDATESTOCK
    }
    private GATHERING_STATE GatheringState;
    private bool Staking = false;
    private bool Travel = false;

	// Use this for initialization
    void Start()
    {
        Priority = new List<BaseStacksManager.RESOURCES_TYPE>();
        switch (SeedKeyPriority)
        {
            case 1:
                Priority.Add(BaseStacksManager.RESOURCES_TYPE.WOOD);
                Priority.Add(BaseStacksManager.RESOURCES_TYPE.FOOD);
                break;
            default:
                Priority.Add(BaseStacksManager.RESOURCES_TYPE.FOOD);
                Priority.Add(BaseStacksManager.RESOURCES_TYPE.WOOD);
                break;
        }
        Debug.Log(Priority[0]);
        GatheringState = GATHERING_STATE.UPDATESTOCK;
        Inventory = new int[10];
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Manager.numberOfRessources == 0)
        {
            switch (GatheringState)
            {
                case GATHERING_STATE.UPDATESTOCK:
                    if (Travel == false)
                    {
                        NavScript.dest = "base";
                        Debug.Log("Going to Base");
                        Travel = true;
                    }
                    if (NavScript.travelFinished)
                        GetPriority();
                    break;
                case GATHERING_STATE.COLLECTING_WOOD:
                    if (Travel == false)
                    {
                        NavScript.dest = "wood";
                        Debug.Log("Searching for wood");
                        Travel = true;
                    }
                    if (NavScript.travelFinished && Vector3.Distance(transform.position, NavScript.agent.destination) < Distance)
                        StartCoroutine(StackObject(BaseStacksManager.RESOURCES_TYPE.WOOD));
                    break;
                case GATHERING_STATE.COLLECTING_FOOD:
                    if (Travel == false)
                    {
                        NavScript.dest = "food";
                        Debug.Log("Searching for food");
                        Travel = true;
                    }
                    if (NavScript.travelFinished && Vector3.Distance(transform.position, NavScript.agent.destination) < Distance)
                        StartCoroutine(StackObject(BaseStacksManager.RESOURCES_TYPE.FOOD));
                    break;
                case GATHERING_STATE.BRINGTOBASE:
                    if (Travel == false)
                    {
                        NavScript.dest = "base";
                        Debug.Log("Going to Base");
                        Travel = true;
                    }
                    if (NavScript.travelFinished && Vector3.Distance(transform.position, Base.transform.position) < Distance)
                        StartCoroutine(DropStack());
                    break;
            }
        }
    }

    private void GetPriority()
    {
        BaseStacksManager.RESOURCES_TYPE resourcesToGet = Priority[0];
        foreach (BaseStacksManager.RESOURCES_TYPE rt in Priority)
        {
            if (Manager.baseStackManagerScript.ResourcesStacked[rt] < CriticalStockLevel)
            {
                resourcesToGet = rt;
                break;
            }
        }
        Travel = false;
        if (resourcesToGet == BaseStacksManager.RESOURCES_TYPE.WOOD)
            GatheringState = GATHERING_STATE.COLLECTING_WOOD;
        if (resourcesToGet == BaseStacksManager.RESOURCES_TYPE.FOOD)
            GatheringState = GATHERING_STATE.COLLECTING_FOOD;
    }

    private IEnumerator DropStack()
    {
        if (!Staking)
        {
            Staking = true;
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 1)
                    Manager.baseStackManagerScript.AddResources(BaseStacksManager.RESOURCES_TYPE.WOOD, 1);
                if (Inventory[i] == 2)
                    Manager.baseStackManagerScript.AddResources(BaseStacksManager.RESOURCES_TYPE.FOOD, 1);
                yield return new WaitForSeconds(0.25f);
            }
            GatheringState = GATHERING_STATE.UPDATESTOCK;
            Staking = false;
            Travel = false;
        }
    }

    private IEnumerator StackObject(BaseStacksManager.RESOURCES_TYPE ResourceType)
    {
        if (!Staking)
        {
            Staking = true;
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (ResourceType == BaseStacksManager.RESOURCES_TYPE.WOOD)
                    Inventory[i] = 1;
                if (ResourceType == BaseStacksManager.RESOURCES_TYPE.FOOD)
                    Inventory[i] = 2;
                Debug.Log(i + " resources collected");
                yield return new WaitForSeconds(1);
            }
            GatheringState = GATHERING_STATE.BRINGTOBASE;
            Staking = false;
            Travel = false;
        }
    }
}
