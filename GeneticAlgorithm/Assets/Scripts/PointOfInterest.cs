using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointOfInterest : MonoBehaviour {

	public List<Vector3> pointOfInterest = new List<Vector3>();
    public Dictionary<string, Vector3> pointOfInterest2 = new Dictionary<string, Vector3>();

    public List<Vector3> wood = new List<Vector3>();
    public List<GameObject> woodGO = new List<GameObject>();

    public List<Vector3> food = new List<Vector3>();
    public List<GameObject> foodGO = new List<GameObject>();
	int size;
	public bool allDetected = false;

	public GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		size = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(size != pointOfInterest.Count)
		{
			size = pointOfInterest.Count;
		}
		else
			allDetected = false;
	}

	public int GetSize()
	{
		return size;
	}

	public void addElement(Vector3 position)
	{
		pointOfInterest.Add(position);
	}
}
