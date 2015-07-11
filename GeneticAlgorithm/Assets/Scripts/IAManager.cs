using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAManager : MonoBehaviour {

    [HideInInspector]
    public AI LearningAI;
    [HideInInspector]
    public World CurrentWorld;
    [HideInInspector]
    public ActionTrees OrdersChain;
    public GameManager Manager;

    private int CurrentOrder;
    private bool Creation = true;

	// Use this for initialization
	void Start () {
        CurrentOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Creation && Manager.numberOfRessources == 0)
        {
            Creation = false;
            CurrentWorld = gameObject.AddComponent<World>();
            CurrentWorld.CharacterList = Manager.CharacterList;
            CurrentWorld.WoodList = Manager.pointOfInterrestScript.woodGO;
            CurrentWorld.FoodList = Manager.pointOfInterrestScript.foodGO;
            CreateObjectives();
        }
        if (OrdersChain != null)
        {
            int IDAction = OrdersChain.ListOfActions[CurrentOrder];
            Debug.Log("Current Action : " + IDAction);
            if (CurrentOrder == 0 || Manager.ActionDone)
            {
                SendOrder(IDAction);
                CurrentOrder++;
                Manager.ActionDone = false;
            }
        }
        else if (!Creation)
        {
            Debug.Log(OrdersChain);
        }
	}

    private void SendOrder(int IDAction)
    {
        Actions ActionToDo = CurrentWorld.GetActionById(IDAction);
        foreach (IAGathering gather in CurrentWorld.CharacterList)
        {
            switch (ActionToDo.ActionName)
            {
                case "GetWood":
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                    break;
                case "GetFood":
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_FOOD;
                    break;
                case "BuildBuliding":
                    gather.GatheringState = IAGathering.GATHERING_STATE.BUILDING;
                    break;
                default:
                    gather.GatheringState = IAGathering.GATHERING_STATE.BRINGTOBASE;
                    break;
            }
        }

    }

    private void CreateObjectives()
    {
        Objets foodobjet = new Objets();
        foodobjet.Create(1,"Food");
        Objets woodobjet = new Objets();
        woodobjet.Create(1,"Wood");
        Objets baseobjet = new Objets();
        baseobjet.Create(1,"Base");
        Objets buildingobjet = new Objets();
        buildingobjet.Create(1, "Building");

        List<Actions> ActionsPossible = new List<Actions>();
        Actions action = new Actions();

        action.Create(1, "GetWood",10,ref CurrentWorld);
        ActionsPossible.Add(action);

        action.Create(2, "GetFood",10, ref CurrentWorld);
        ActionsPossible.Add(action);

        action.Create(3, "BuildBuliding",20, ref CurrentWorld);
        ActionsPossible.Add(action);

        action.Create(4, "ReturnToBase",15, ref CurrentWorld);
        ActionsPossible.Add(action);

        CurrentWorld.ActionList = ActionsPossible;

        
        List<Objectives> ObjectivesList = new List<Objectives>();
        Objectives objs = new Objectives();
        Types t = new Types();
        Consequences csq = new Consequences();
        List<Actions> acts = new List<Actions>();
        List<Objets> obejs = new List<Objets>();
        CurrentWorld.AllInteractions = new List<Consequences>();

        List<Actions> acts2 = new List<Actions>();
        List<Objets> obejs2 = new List<Objets>();
        List<Actions> acts3 = new List<Actions>();
        List<Objets> obejs3 = new List<Objets>();
        List<Actions> acts4 = new List<Actions>();
        List<Objets> obejs4 = new List<Objets>();


        t.Create(1, "GetWood", ref CurrentWorld);
        objs.Create(false, t, 10, CurrentWorld.GetActionById(1));
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[0]);
        obejs.Add(woodobjet);
        csq.Create(acts, obejs, 1, "Collecting Wood", t, ref CurrentWorld);

        t.Create(2, "GetFood", ref CurrentWorld);
        objs.Create(false, t, 10, CurrentWorld.GetActionById(2));
        ObjectivesList.Add(objs);
        acts2.Add(ActionsPossible[1]);
        obejs2.Add(foodobjet);
        csq.Create(acts, obejs, 2, "Collecting Food", t, ref CurrentWorld);

        t.Create(3, "BuildBuilding", ref CurrentWorld);
        objs.Create(false, t, 1, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        acts3.Add(ActionsPossible[2]);
        obejs3.Add(buildingobjet);
        csq.Create(acts, obejs, 3, "Build Building", t, ref CurrentWorld);

        t.Create(4, "HaveThreeBuliding", ref CurrentWorld);
        objs.Create(true, t, 10, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        acts4.Add(ActionsPossible[2]);
        obejs4.Add(buildingobjet);
        csq.Create(acts, obejs, 4, "Build 3 Building", t, ref CurrentWorld);

        LearningAI = gameObject.AddComponent<AI>();
        LearningAI.Create(objs, 3, CurrentWorld);
        OrdersChain = LearningAI.ChooseActions();

        if(OrdersChain != null)
        {
            int Test = 0;
        }
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if(gatheringScript)
            gatheringScript.GatheringState = order;
    }
}
