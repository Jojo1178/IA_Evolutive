using UnityEngine;
using System.Collections;

public class Types : World {

	private int typeID;
	private string typeName;

	public int TypeID {
		get {
			return typeID;
		}
		set {
			typeID = value;
		}
	}

	public string TypeName {
		get {
			return typeName;
		}
		set {
			typeName = value;
		}
	}

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
    public virtual void Update()
    {
	
	}

	public virtual void Create(int ID, string Name)
	{
		typeID = ID;
		typeName = Name;
        AllTypes.Add(this);
	}
}
