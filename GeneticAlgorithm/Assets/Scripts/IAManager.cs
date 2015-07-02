using UnityEngine;
using System.Collections;

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
            CurrentWorld.WoodList = Manager.pointOfInterrestScript.wood;
            CurrentWorld.FoodList = Manager.pointOfInterrestScript.food;
            CreateObjective();
        }
	}

    private void CreateObjective()
    {
        Objectives objs = new Objectives();
        Types t = new Types();
        Actions act = new Actions();
        t.Create(1, "GetWood");
        act.Create(1, "Drop Wood");
        objs.Create(true, t, 10, act);

        LearningAI.Create(objs, 3, CurrentWorld);
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if(gatheringScript)
            gatheringScript.GatheringState = order;
    }
}
