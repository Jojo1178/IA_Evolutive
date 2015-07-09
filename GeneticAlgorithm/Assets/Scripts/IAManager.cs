using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAManager : MonoBehaviour {

    public AI LearningAI;
    public World CurrentWorld;
    public ActionTrees OrdersChain;
    public GameManager Manager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.numberOfRessources == 0)
        {
            CurrentWorld.CharacterList = Manager.CharacterList;
            CurrentWorld.WoodList = Manager.pointOfInterrestScript.woodGO;
            CurrentWorld.FoodList = Manager.pointOfInterrestScript.foodGO;
            CreateObjectives();
        }
	}

    private void CreateObjectives()
    {
        Actions[] ActionsPossible = new Actions[6];
        Actions action = new Actions();

        action.Create(1, "GetWood");
        ActionsPossible[0] = action;

        action.Create(2, "GetFood");
        ActionsPossible[1] = action;

        action.Create(3, "BringStackToBase");
        ActionsPossible[2] = action;

        action.Create(4, "ReturnToBase");
        ActionsPossible[3] = action;

        action.Create(5, "BuildBuliding");
        ActionsPossible[4] = action;

        action.Create(6, "CreateNewCharacter");
        ActionsPossible[5] = action;

        CurrentWorld.ActionList = ActionsPossible;

        
        List<Objectives> ObjectivesList = new List<Objectives>();
        Objectives objs = new Objectives();
        Types t = new Types();
        
        t.Create(1, "GetWood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(1));
        ObjectivesList.Add(objs);

        t.Create(2, "GetFood");
        objs.Create(false, t, 10, CurrentWorld.GetActionById(2));
        ObjectivesList.Add(objs);

        t.Create(3, "BuildBuilding");
        objs.Create(false, t, 1, CurrentWorld.GetActionById(5));
        ObjectivesList.Add(objs);

        t.Create(4, "CreateNewCharacter");
        objs.Create(false, t, 1, CurrentWorld.GetActionById(6));
        ObjectivesList.Add(objs);

        t.Create(5, "HaveTenPeople");
        objs.Create(true, t, 10, CurrentWorld.GetActionById(6));
        ObjectivesList.Add(objs);

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
