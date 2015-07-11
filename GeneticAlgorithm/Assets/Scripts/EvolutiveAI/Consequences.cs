using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consequences : World {

	private List<Actions> actionsLinked = new List<Actions>();
    private List<Objets> objets = new List<Objets>();
	private int consequenceId;
	private string consequenceName;
	private Types type;

    public List<Actions> ActionsLinked
    {
		get {
            return actionsLinked;
		}
		set {
            actionsLinked = value;
		}
	}

	public List<Objets> Objets {
		get {
			return objets;
		}
		set {
			objets = value;
		}
	}

	public int ConsequenceId {
		get {
			return consequenceId;
		}
		set {
			consequenceId = value;
		}
	}

	public string ConsequenceName {
		get {
			return consequenceName;
		}
		set {
			consequenceName = value;
		}
	}

	public Types Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}

	// Use this for initialization
	public new virtual void Start () {
	
	}
	
	// Update is called once per frame
	public new  virtual void Update () {
	
	}

	public virtual void Create(List<Actions> linkedActions,List<Objets> linkedObjects, int ID, string Name, Types ConsType,ref World CurWorld)
	{
        actionsLinked = linkedActions;
		objets = linkedObjects;
		consequenceId = ID;
		consequenceName = Name;
		type = ConsType;

        CurWorld.AllInteractions.Add(this);
	}

    //public virtual void Create(Actions linkedActions, Objets linkedObjects, int ID, string Name, Types ConsType)
    //{
    //    actionsList[0] = linkedActions;
    //    objets[0] = linkedObjects;
    //    consequenceId = ID;
    //    consequenceName = Name;
    //    type = ConsType;
    //
    //    AllInteractions[AllInteractions.Length] = this;
    //}


}
