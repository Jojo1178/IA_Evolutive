using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAManager : MonoBehaviour {

    public AI LearningAI;
    public World CurrentWorld;
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
        if (OrdersChain)
        {
            int IDAction = OrdersChain.ListOfActions[CurrentOrder];

            if (CurrentOrder == 0 || Manager.ActionDone)
            {
                SendOrder(IDAction);
                CurrentOrder++;
                Manager.ActionDone = false;
            }
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

        Actions[] ActionsPossible = new Actions[4];
        Actions action = new Actions();

        action.Create(1, "GetWood");
        ActionsPossible[0] = action;

        action.Create(2, "GetFood");
        ActionsPossible[1] = action;

        action.Create(3, "BuildBuliding");
        ActionsPossible[2] = action;

        action.Create(4, "ReturnToBase");
        ActionsPossible[3] = action;


        CurrentWorld.ActionList = ActionsPossible;

        
        List<Objectives> ObjectivesList = new List<Objectives>();
        Objectives objs = new Objectives();
        Types t = new Types();
        Consequences csq = new Consequences();

        t.Create(1, "GetWood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(1));
        ObjectivesList.Add(objs);
        csq.Create(ActionsPossible[0], woodobjet, 1, "Collecting Wood", t);

        t.Create(2, "GetFood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(2));
        ObjectivesList.Add(objs);
        csq.Create(ActionsPossible[1], foodobjet, 2, "Collecting Food", t);

        t.Create(3, "BuildBuilding");
        objs.Create(false, t, 1, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        csq.Create(ActionsPossible[2], buildingobjet, 3, "Build Building", t);

        t.Create(4, "HaveThreeBuliding");
        objs.Create(true, t, 10, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        csq.Create(ActionsPossible[2], buildingobjet, 4, "Build 3 Building", t);

        LearningAI = gameObject.AddComponent<AI>();
        LearningAI.Create(objs, 3, CurrentWorld);
        OrdersChain = LearningAI.ChooseActions();
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if(gatheringScript)
            gatheringScript.GatheringState = order;
    }
}
