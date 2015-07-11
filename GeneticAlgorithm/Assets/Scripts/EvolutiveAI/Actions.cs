using UnityEngine;
using System.Collections;

public class Actions : World {


	private int actionID;
	private string actionName;
	private int score;


	public string ActionName {
		get {
			return actionName;
		}
		set {
			actionName = value;
		}
	}

	public int ActionID {
		get {
			return actionID;
		}
		set {
			actionID = value;
		}
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
		}
	}

	// Use this for initialization
	public virtual void Start () {
	
	}

	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void UpdateScore()
	{

	}

	public virtual void Create(int ID, string Name)
	{
		actionID = ID;
		actionName = Name;
		score = 0;
        ActionList.Add(this);
	}


}
