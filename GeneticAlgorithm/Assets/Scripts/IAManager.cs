using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IAManager : MonoBehaviour {

    public AI LearningAI;
    public World CurrentWorld;
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
        List<Objectives> ObjectivesList = new List<Objectives>();

        Objectives objs = new Objectives();
        Types t = new Types();
        Actions act = new Actions();
        
        t.Create(1, "GetWood");
        act.Create(1, "Drop Wood");
        objs.Create(false, t, 10, act);
        ObjectivesList.Add(objs);

        t.Create(2, "GetFood");
        act.Create(2, "Drop Food");
        objs.Create(false, t, 10, act);
        ObjectivesList.Add(objs);

        t.Create(3, "BuildBuilding");
        act.Create(3, "Building Builded");
        objs.Create(false, t, 1, act);
        ObjectivesList.Add(objs);

        t.Create(4, "CreateNewCharacter");
        act.Create(4, "Character Borned");
        objs.Create(false, t, 1, act);
        ObjectivesList.Add(objs);

        t.Create(5, "HaveTenPeople");
        act.Create(5, "Ten People Created");
        objs.Create(true, t, 10, act);
        ObjectivesList.Add(objs);

        LearningAI.Create(objs, 3, CurrentWorld);
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if(gatheringScript)
            gatheringScript.GatheringState = order;
    }
}
