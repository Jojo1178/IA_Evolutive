using UnityEngine;
using System.Collections;

public class ActionTrees : MonoBehaviour {

	private int[] listOfActions;
	private int treeScore;
	private Objectives objectif;

	public Objectives Objectif {
		get {
			return objectif;
		}
		set {
			objectif = value;
		}
	}

	public int TreeScore {
		get {
			return treeScore;
		}
		set {
			treeScore = value;
		}
	}

	public int[] ListOfActions {
		get {
			return listOfActions;
		}
		set {
			listOfActions = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool SaveActionTree()
	{
		return false;
	}

	void FuseTree(ActionTrees OtherTree)
	{

	}

}
