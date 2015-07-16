using UnityEngine;
using System.Collections;

public class Types {

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
	public void Start () {
	
	}
	
	// Update is called once per frame
    public void Update()
    {
	
	}

	public virtual void Create(int ID, string Name, ref World CurWorld)
	{
		typeID = ID;
		typeName = Name;
        CurWorld.AllTypes.Add(this);
	}

    public static bool operator ==(Types type1, Types type2)
    {
        return (type1.TypeID == type2.TypeID && type1.TypeName == type2.TypeName);
    }

    public static bool operator !=(Types type1, Types type2)
    {
        return (type1.TypeID != type2.TypeID && type1.TypeName != type2.TypeName);
    }
}
