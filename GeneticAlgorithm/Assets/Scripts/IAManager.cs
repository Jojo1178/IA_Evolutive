using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct SavedAction
{
    private Objectives objective;

    public Objectives Objective
    {
        get { return objective; }
        set { objective = value; }
    }
    private ActionTrees actionTree;

    public ActionTrees ActionTree
    {
        get { return actionTree; }
        set { actionTree = value; }
    }

    public void Create(Objectives newObjective, ActionTrees newTree)
    {
        Objective = newObjective;
        ActionTree = newTree;
    }
}

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

    List<SavedAction> savedAct = new List<SavedAction>();


	// Use this for initialization
	void Start () {
        CurrentOrder = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (Creation && Manager.numberOfRessources == 0)
        {
            Creation = false;
            CurrentWorld = gameObject.AddComponent<World>();
            CurrentWorld.CharacterList = Manager.CharacterList;
            CurrentWorld.WoodList = Manager.pointOfInterrestScript.woodGO;
            CurrentWorld.FoodList = Manager.pointOfInterrestScript.foodGO;
            CreateObjectives();
            DoAction();
        }
        if (OrdersChain != null && Manager.ActionDone)
        {
            if (CurrentOrder < OrdersChain.ListOfActions.Count)
                DoAction();
            else
            {
                CurrentOrder = 0;
                CreateObjectives();
                DoAction();
            }
        }
    }

    private void DoAction()
    {
        int IDAction = OrdersChain.ListOfActions[CurrentOrder];
        Debug.Log("Current Action : " + IDAction);
        SendOrder(IDAction);
        CurrentOrder++;
        Manager.ActionDone = false;
    }

    private void SendOrder(int IDAction)
    {
        Actions ActionToDo = CurrentWorld.GetActionById(IDAction);
        foreach (IAGathering gather in CurrentWorld.CharacterList)
        {
            gather.Quantity = 0;
            switch (ActionToDo.ActionName)
            {
                case "GetWood":
                    gather.Quantity = ActionToDo.NumberRequired;
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                    CurrentWorld.CompteurWood += gather.Inventory.Length;
                    break;
                case "GetFood":
                    gather.Quantity = ActionToDo.NumberRequired;
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_FOOD;
                    CurrentWorld.CompteurFood += gather.Inventory.Length;
                    break;
                case "BuildBuliding":
                    if (CurrentWorld.CompteurWood > 10)
                    {
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.BUILDING;
                        CurrentWorld.CompteurBuild += ActionToDo.NumberRequired;
                    }
                    else
                    {
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                        CurrentWorld.CompteurWood += gather.Inventory.Length;
                        CurrentOrder--;
                    }
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
        Actions action2 = new Actions();
        Actions action3 = new Actions();
        Actions action4 = new Actions();


        action.Create(1, "GetWood",10,ref CurrentWorld);
        ActionsPossible.Add(action);

        action2.Create(2, "GetFood",10, ref CurrentWorld);
        ActionsPossible.Add(action2);

        action3.Create(3, "BuildBuliding",20, ref CurrentWorld);
        ActionsPossible.Add(action3);

        action4.Create(4, "ReturnToBase",5, ref CurrentWorld);
        ActionsPossible.Add(action4);

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


        Objectives objs2 = new Objectives();
        Objectives objs3 = new Objectives();
        Objectives objs4 = new Objectives();

        Consequences csq2 = new Consequences();
        Consequences csq3 = new Consequences();
        Consequences csq4 = new Consequences();


        Types t2 = new Types();
        Types t3 = new Types();
        Types t4 = new Types();


        t.Create(1, "GetWood", ref CurrentWorld);
        objs.Create(false, t, 10, CurrentWorld.GetActionById(1), Typeobjectif.WOOD);
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[0]);
        obejs.Add(woodobjet);
        csq.Create(acts, obejs, 1, "Collecting Wood", t, ref CurrentWorld);

        t2.Create(2, "GetFood", ref CurrentWorld);
        objs2.Create(false, t2, 10, CurrentWorld.GetActionById(2), Typeobjectif.FOOD);
        ObjectivesList.Add(objs2);
        acts2.Add(ActionsPossible[1]);
        obejs2.Add(foodobjet);
        csq2.Create(acts2, obejs2, 2, "Collecting Food", t2, ref CurrentWorld);

        t3.Create(3, "BuildBuilding", ref CurrentWorld);
        objs.Create(false, t3, 1, CurrentWorld.GetActionById(3), Typeobjectif.BUILD);
        ObjectivesList.Add(objs3);
        acts3.Add(ActionsPossible[2]);
        obejs3.Add(buildingobjet);
        csq3.Create(acts3, obejs3, 3, "Build Building", t3, ref CurrentWorld);

        t4.Create(4, "HaveThreeBuliding", ref CurrentWorld);
        objs4.Create(true, t4, 7, CurrentWorld.GetActionById(3), Typeobjectif.BUILD);
        ObjectivesList.Add(objs4);
        acts4.Add(ActionsPossible[2]);
        obejs4.Add(buildingobjet);
        csq4.Create(acts4, obejs4, 4, "Build 3 Building", t4, ref CurrentWorld);

        if(!ActionTreeAlreadyExist(objs))
        {
            LearningAI = gameObject.AddComponent<AI>();
            LearningAI.Create(objs, 3, CurrentWorld);
            OrdersChain = LearningAI.ChooseActions();

            SavedAction newSave = new SavedAction();
            newSave.Create(objs, OrdersChain);
            savedAct.Add(newSave);
        }
        else
        {
            OrdersChain = GetActionTreeByObjective(objs);
        }
        
        if(OrdersChain != null)
        {
            Debug.Log("Order Size : " + OrdersChain.ListOfActions.Count);
        }
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if(gatheringScript)
            gatheringScript.GatheringState = order;
    }

    public bool ActionTreeAlreadyExist(Objectives obj)
    {
        for (int i = 0; i < savedAct.Count; i++)
        {
            if (savedAct[i].Objective == obj)
            {
                return true;
            }
        }

        return false;
    }

    ActionTrees GetActionTreeByObjective(Objectives obj)
    {
        for (int i = 0; i < savedAct.Count; i++)
        {
            if (savedAct[i].Objective == obj)
            {
                return savedAct[i].ActionTree;
            }
        }
        return null;
    }
}
