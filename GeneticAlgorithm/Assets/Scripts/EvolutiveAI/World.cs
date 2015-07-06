using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {


	private Consequences[] allInteractions;

    public List<GameObject> CharacterList;
    public List<GameObject> WoodList;
    public List<GameObject> FoodList;

	public Consequences[] AllInteractions {
		get {
			return allInteractions;
		}
		set {
			allInteractions = value;
		}
	}

	// Use this for initialization
	public virtual void Start () {
		allInteractions.Initialize();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public int[] GetConsequencesByType(Types type)
	{
		int[] AllConsequencesId = null;
		int ArrayCounter = 0;

		for (int i = 0; i < allInteractions.Length; i++) 
		{
			if(allInteractions[i].Type == type)
			{
				AllConsequencesId[ArrayCounter] = allInteractions[i].ConsequenceId;
				ArrayCounter++;
			}
		}

		return AllConsequencesId;
	}



	public Consequences GetConsequenceById(int Id)
	{
		for (int i = 0; i < allInteractions.Length; i++) 
		{
			if(allInteractions[i].ConsequenceId == Id)
			{
				return allInteractions[i];
			}
		}

		return null;
	}
}
