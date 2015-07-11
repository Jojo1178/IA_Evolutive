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

        List<Actions> ActionsPossible = new List<Actions>();
        Actions action = new Actions();

        action.Create(1, "GetWood");
        ActionsPossible.Add(action);

        action.Create(2, "GetFood");
        ActionsPossible.Add(action);

        action.Create(3, "BuildBuliding");
        ActionsPossible.Add(action);

        action.Create(4, "ReturnToBase");
        ActionsPossible.Add(action);

        CurrentWorld.ActionList = ActionsPossible;

        
        List<Objectives> ObjectivesList = new List<Objectives>();
        Objectives objs = new Objectives();
        Types t = new Types();
        Consequences csq = new Consequences();
        List<Actions> acts = new List<Actions>();
        List<Objets> obejs = new List<Objets>();
        CurrentWorld.AllInteractions = new List<Consequences>();


        t.Create(1, "GetWood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(1));
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[0]);
        obejs.Add(woodobjet);
        csq.Create(acts, obejs, 1, "Collecting Wood", t);
        CurrentWorld.AllInteractions.Add(csq);

        acts.Clear();
        obejs.Clear();

        t.Create(2, "GetFood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(2));
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[1]);
        obejs.Add(foodobjet);
        csq.Create(acts, obejs, 2, "Collecting Food", t);
        CurrentWorld.AllInteractions.Add(csq);

        acts.Clear();
        obejs.Clear();

        t.Create(3, "BuildBuilding");
        objs.Create(false, t, 1, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[2]);
        obejs.Add(buildingobjet);
        csq.Create(acts, obejs, 3, "Build Building", t);
        CurrentWorld.AllInteractions.Add(csq);

        acts.Clear();
        obejs.Clear();

        t.Create(4, "HaveThreeBuliding");
        objs.Create(true, t, 10, CurrentWorld.GetActionById(3));
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[2]);
        obejs.Add(buildingobjet);
        csq.Create(acts, obejs, 4, "Build 3 Building", t);
        CurrentWorld.AllInteractions.Add(csq);

        acts.Clear();
        obejs.Clear();

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
