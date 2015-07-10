using UnityEngine;
using System.Collections;

public class Consequences : World {

	private Actions[] actionsList;
	private Objets[] objets;
	private int consequenceId;
	private string consequenceName;
	private Types type;

	public Actions[] ActionsList {
		get {
			return actionsList;
		}
		set {
			actionsList = value;
		}
	}

	public Objets[] Objets {
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
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void Create(Actions[] linkedActions,Objets[] linkedObjects, int ID, string Name, Types ConsType)
	{
		actionsList = linkedActions;
		objets = linkedObjects;
		consequenceId = ID;
		consequenceName = Name;
		type = ConsType;

        AllInteractions[AllInteractions.Length] = this;
	}

    public virtual void Create(Actions linkedActions, Objets linkedObjects, int ID, string Name, Types ConsType)
    {
        actionsList[0] = linkedActions;
        objets[0] = linkedObjects;
        consequenceId = ID;
        consequenceName = Name;
        type = ConsType;

        AllInteractions[AllInteractions.Length] = this;
    }


}
