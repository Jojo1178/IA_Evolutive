using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IAGathering : MonoBehaviour {

    public GameManager Manager;
    public IANavigationScript NavScript;
    public IAManager IAScript;
    public GameObject Base;

    [Range(0,1)]
    public int SeedKeyPriority;
    [Range(0, 50)]
    public int CriticalStockLevel;
    public List<BaseStacksManager.RESOURCES_TYPE> Priority;
    [Range(0, 10)]
    public int Distance = 5;
    public int[] Inventory;
    public int Quantity;

    public Vector3[] BuildPlace;

    public GameObject House;

    private bool Staking = false;
    private bool Travel = false;
    private bool DoingStuff = false;

    public enum GATHERING_STATE
    {
        NONE,
        COLLECTING_WOOD,
        COLLECTING_FOOD,
        BRINGTOBASE,
        BUILDING,
		REFINED_WOOD,
        REPRODUCE
    }
    public GATHERING_STATE GatheringState;
    public GATHERING_STATE PreviousGatheringState;

	// Use this for initialization
    void Start()
    {
        Staking = false;
        Travel = false;
        DoingStuff = false;
        Quantity = 0;
        if (!Manager.CharacterList.Contains(this))
            Manager.CharacterList.Add(this);
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
        //IAScript.GatheringState = IAManager.GATHERING_STATE.UPDATESTOCK;
        Inventory = new int[10];
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Manager.numberOfRessources == 0 && !DoingStuff)
        {
            DoingStuff = true;
            switch (GatheringState)
            {
              /*  case IAManager.GATHERING_STATE.UPDATESTOCK:
                    if (Travel == false)
                    {
                        NavScript.dest = "base";
                        Debug.Log("Going to Base");
                        Travel = true;
                    }
                    if (NavScript.travelFinished)
                        GetPriority();
                    break;*/
                case GATHERING_STATE.NONE:
                    DoingStuff = false;
                    break;
			case GATHERING_STATE.REFINED_WOOD:
				if (Travel == false)
				{
					NavScript.dest = "base";
					Debug.Log("Going to Base");
					Travel = true;
				}
				if (NavScript.travelFinished && Vector3.Distance(transform.position, Base.transform.position) < Distance)
					StartCoroutine(RefinedWood());
				else
					DoingStuff = false;
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
                    else
                        DoingStuff = false;
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
                    else
                        DoingStuff = false;
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
                    else
                        DoingStuff = false;
                    break;
                case GATHERING_STATE.BUILDING:
                    if (Manager.NextBuildPlace < Manager.baseStackManagerScript.BuildingPlaces.Count) { 
                    if (Travel == false)
                    {
                        NavScript.agent.SetDestination(Manager.baseStackManagerScript.BuildingPlaces[Manager.NextBuildPlace].Key);
                        NavScript.travelFinished = false;
                        Manager.NextBuildPlace++;
                        //NavScript.dest = "build";
                        Debug.Log("Going to Build stuff");
                        Travel = true;
                    }
                    if (NavScript.travelFinished && Vector3.Distance(transform.position, NavScript.agent.destination) < Distance)
                    {
                        StartCoroutine(BuildBulding(Manager.baseStackManagerScript.BuildingPlaces[Manager.NextBuildPlace].Key));
                        //if (Manager.baseStackManagerScript.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.WOOD] > 10)
                        //    StartCoroutine(BuildBulding());
                        //else
                        //    GatheringState = GATHERING_STATE.COLLECTING_WOOD;
                    }
                    else
                        DoingStuff = false;
                        }
                    break;
                case GATHERING_STATE.REPRODUCE:
                    Manager.baseStackManagerScript.RemoveResources(BaseStacksManager.RESOURCES_TYPE.FOOD, 10);
                    GameObject child = Instantiate(Manager.Player, this.transform.position + new Vector3(10, 0, 0), Quaternion.identity) as GameObject;
                    IANavigationScript childNav = child.GetComponent<IANavigationScript>();
                    childNav.destination = NavScript.destination;
                    childNav.pointOfInterestScript = Manager.pointOfInterrestScript;
                    childNav.gameManagerScript = Manager;
                    IAGathering childGather = child.GetComponent<IAGathering>();
                    drawFieldOfView field = child.GetComponentInChildren<drawFieldOfView>();
                    field.pointOfInterestScript = childNav.pointOfInterestScript;
                    childGather.Manager = Manager;
                    childGather.Base = Base;
                    childGather.IAScript = IAScript;
                    IAScript.CurrentWorld.CharacterList.Add(childGather);

                    GatheringState = GATHERING_STATE.NONE;
                    DoingStuff = false;
                    Staking = false;
                    Travel = false;
                    Manager.ActionDone = true;
                    break;

            }
        }
    }

    /*private void GetPriority()
    {
        BaseStacksManager.RESOURCES_TYPE resourcesToGet = BaseStacksManager.RESOURCES_TYPE.NONE;
        foreach (BaseStacksManager.RESOURCES_TYPE rt in Priority)
        {
            if (Manager.baseStackManagerScript.ResourcesStacked[rt] < CriticalStockLevel)
            {
                resourcesToGet = rt;
                break;

            }
            if (resourcesToGet == BaseStacksManager.RESOURCES_TYPE.NONE && Manager.baseStackManagerScript.ResourcesStacked[rt] < Manager.baseStackManagerScript.ResourcesMaxValues[rt])
                resourcesToGet = rt;
        }
        if (resourcesToGet != BaseStacksManager.RESOURCES_TYPE.NONE)
        {
            Travel = false;
            if (resourcesToGet == BaseStacksManager.RESOURCES_TYPE.WOOD)
                IAScript.GatheringState = GATHERING_STATE.COLLECTING_WOOD;
            if (resourcesToGet == BaseStacksManager.RESOURCES_TYPE.FOOD)
                IAScript.GatheringState = GATHERING_STATE.COLLECTING_FOOD;
        }
        else
            ContructBuilding();
    }

    private void ContructBuilding()
    {
        foreach (KeyValuePair<Vector3, bool> place in Manager.baseStackManagerScript.BuildingPlaces)
        {
            if (place.Value)
            {
                Travel = false;
                IAScript.GatheringState = GATHERING_STATE.BUILDING;
                break;
            }
        }
    }*/

    private IEnumerator BuildBulding(Vector3 place)
    {
        if (!Staking)
        {
            Staking = true;
            //Debug.LogWarning("Create Building animation missing");
            Instantiate(House, this.transform.position,Quaternion.identity);
            Manager.baseStackManagerScript.RemoveResources(BaseStacksManager.RESOURCES_TYPE.WOOD_REFINED, 10);
            yield return new WaitForSeconds(3);
            foreach (BaseStacksManager.RESOURCES_TYPE ent in Enum.GetValues(typeof(BaseStacksManager.RESOURCES_TYPE)))
				if (ent != BaseStacksManager.RESOURCES_TYPE.NONE)
                    Manager.baseStackManagerScript.ResourcesMaxValues[ent] += 50;
            Quantity--;
            //IAScript.GatheringState = IAManager.GATHERING_STATE.UPDATESTOCK;
            if (Quantity <= 0)
            {
                GatheringState = GATHERING_STATE.NONE;
                Manager.ActionDone = true;
            }
            else
                GatheringState = GATHERING_STATE.BUILDING;
            DoingStuff = false;
            Staking = false;
            Travel = false;
            //throw new System.NotImplementedException();
        }
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
            }
            //IAScript.GatheringState = IAManager.GATHERING_STATE.UPDATESTOCK;
            if (Quantity <= 0)
            {
                GatheringState = GATHERING_STATE.NONE;
                Manager.ActionDone = true;
            }
            else
                GatheringState = PreviousGatheringState;

            Staking = false;
            Travel = false;
            yield return null;
            DoingStuff = false;
        }
    }

	private IEnumerator RefinedWood()
	{
		if (!Staking)
		{
			Staking = true;
			for (int i = 0; i < Inventory.Length; i++)
			{
				if(Manager.baseStackManagerScript.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.WOOD] == 0)
					break;
				Manager.baseStackManagerScript.AddResources(BaseStacksManager.RESOURCES_TYPE.WOOD_REFINED, 1);
				Manager.baseStackManagerScript.RemoveResources(BaseStacksManager.RESOURCES_TYPE.WOOD, 1);
				Debug.Log(i + " wood refined");
				yield return new WaitForSeconds(1);
			}
			Quantity -= Inventory.Length;
			if (Quantity <= 0)
			{
				GatheringState = GATHERING_STATE.NONE;
				Manager.ActionDone = true;
			}
			Staking = false;
			Travel = false;
			DoingStuff = false;
		}
	}

    private IEnumerator StackObject(BaseStacksManager.RESOURCES_TYPE ResourceType)
    {
        Debug.Log("test");
        if (!Staking)
        {
            Staking = true;
            Debug.Log("test");
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (ResourceType == BaseStacksManager.RESOURCES_TYPE.WOOD)
                    Inventory[i] = 1;
                if (ResourceType == BaseStacksManager.RESOURCES_TYPE.FOOD)
                    Inventory[i] = 2;
                Debug.Log(i + " resources collected");
                yield return new WaitForSeconds(1);
            }
            PreviousGatheringState = GatheringState;
            GatheringState = GATHERING_STATE.BRINGTOBASE;
            Quantity -= Inventory.Length;
            Staking = false;
            Travel = false;
            DoingStuff = false;
        }
    }
}
