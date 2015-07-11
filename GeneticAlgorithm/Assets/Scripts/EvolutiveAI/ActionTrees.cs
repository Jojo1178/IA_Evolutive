using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionTrees : MonoBehaviour {

	private List<int> listOfActions = new List<int>();
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

	public List<int> ListOfActions {
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
