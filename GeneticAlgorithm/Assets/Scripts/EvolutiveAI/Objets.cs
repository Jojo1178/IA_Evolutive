using UnityEngine;
using System.Collections;

public class Objets : MonoBehaviour {

	private int objectID;
	private string objectName;

	public int ObjectID {
		get {
			return objectID;
		}
		set {
			objectID = value;
		}
	}

	public string ObjectName {
		get {
			return objectName;
		}
		set {
			objectName = value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Create(int ID, string Name)
	{
		objectID = ID;
		objectName = Name;
	}
}
