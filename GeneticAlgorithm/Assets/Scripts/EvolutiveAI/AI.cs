using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	private Objectives longTermObjective;
	private int numberOfActionsPossible;
	private World monde;
    private Decision DecisionMaker;

	public Objectives LongTermObjective {
		get {
			return longTermObjective;
		}
		set {
			longTermObjective = value;
		}
	}

	public int NumberOfActionsPossible {
		get {
			return numberOfActionsPossible;
		}
		set {
			numberOfActionsPossible = value;
		}
	}

	public World Monde {
		get {
			return monde;
		}
		set {
			monde = value;
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public ActionTrees ChooseActions()
    {
        return DecisionMaker.CreateActionTree(LongTermObjective, NumberOfActionsPossible);
    }
	public void Create(Objectives LGObjective, int NumberOfActions, World CurWorld)
	{
		LongTermObjective = LGObjective;
		NumberOfActionsPossible = NumberOfActions;
		Monde = CurWorld;
        
        if(DecisionMaker == null)
            DecisionMaker = new Decision();

        DecisionMaker.Create(CurWorld);
	}

}
