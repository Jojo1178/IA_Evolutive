﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {


    private List<Actions> actionList = new List<Actions>();
    private List<Consequences> allInteractions = new List<Consequences>();

    public List<IAGathering> CharacterList = new List<IAGathering>();
    public List<GameObject> WoodList = new List<GameObject>();
    public List<GameObject> FoodList = new List<GameObject>();

	public List<Consequences> AllInteractions {
		get {
			return allInteractions;
		}
		set {
			allInteractions = value;
		}
	}
    public List<Actions> ActionList
    {
        get { return actionList; }
        set { actionList = value; }
    }

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {


	
	}

	public List<int> GetConsequencesByType(Types type)
	{
		List<int> AllConsequencesId = new List<int>();
		int ArrayCounter = 0;

		for (int i = 0; i < allInteractions.Count; i++) 
		{
			if(allInteractions[i].Type == type)
			{
				AllConsequencesId.Add(allInteractions[i].ConsequenceId);
				ArrayCounter++;
			}
		}

		return AllConsequencesId;
	}



	public Consequences GetConsequenceById(int Id)
	{
        for (int i = 0; i < allInteractions.Count; i++) 
		{
			if(allInteractions[i].ConsequenceId == Id)
			{
				return allInteractions[i];
			}
		}

		return null;
	}

    public Actions GetActionById(int Id)
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            if(actionList[i].ActionID == Id)
            {
                return actionList[i];
            }
        }

        return null;
    }
}
