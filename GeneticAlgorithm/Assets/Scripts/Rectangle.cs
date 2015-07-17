using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rectangle{

	Vector3 topLeft, topRight, bottomLeft, bottomRight;
	int id;
	List<Vector3> listePointInteret;
	List<Rectangle> subdivisionList = new List<Rectangle>();

	public Rectangle(GameObject TL, GameObject TR, GameObject BL, GameObject BR)
	{
		topLeft = TL.transform.position;
		topRight = TR.transform.position;
		bottomLeft = BL.transform.position;
		bottomRight = BR.transform.position;
		id = 0;
	}

	public Rectangle(Vector3 TL, Vector3 TR, Vector3 BL, Vector3 BR, int index)
	{
		topLeft = TL;
		topRight = TR;
		bottomLeft = BL;
		bottomRight = BR;
		id = index;
	}

	public void addPointInterest(Vector3 pt)
	{
		listePointInteret.Add(pt);
	}

	public bool isInRectangle(Vector3 v)
	{
		bool inX = false;
		bool inZ = false;
		if (v.x > bottomLeft.x && v.x < topRight.x)
			inX = true;
		
		if (v.z > bottomLeft.z && v.z < topRight.z)
			inZ = true;
		
		return (inX && inZ);
	}

	//On part du principe que le rectangle est un carré et que le nombre de subdivision est un nombre paire.
	public List<Rectangle> subdivideSquareBy4()
	{
		float distance = ( Mathf.Sqrt( ((topRight.x - topLeft.x)*(topRight.x - topLeft.x)) + ((topRight.z - topLeft.z)*(topRight.z - topLeft.z)) ) ) / 2;
		Vector3 newPointTL_TR = new Vector3(topLeft.x + distance, 0, topLeft.z); // nouveau point entre A et B
		GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var mr = cube1.GetComponent<MeshRenderer>();
		mr.material.color = Color.cyan;
		cube1.transform.position = newPointTL_TR;

		Vector3 newPointTL_BL = new Vector3(topLeft.x, 0, topLeft.z - distance); // nouveau point entre A et D
		GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var mr1 = cube2.GetComponent<MeshRenderer>();
		mr1.material.color = Color.cyan;
		cube2.transform.position = newPointTL_BL;

		Vector3 newPointMiddle = new Vector3(topLeft.x + distance, 0, topLeft.z - distance); // nouveau point au milieu du carre
		GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var mr2 = cube3.GetComponent<MeshRenderer>();
		mr2.material.color = Color.cyan;
		cube3.transform.position = newPointMiddle;

		Vector3 newPointTR_BR = new Vector3(topRight.x, 0, topRight.z - distance); // nouveau point entre B et C
		GameObject cube4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var mr3 = cube4.GetComponent<MeshRenderer>();
		mr3.material.color = Color.cyan;
		cube4.transform.position = newPointTR_BR;

		Vector3 newPointBL_BR = new Vector3(bottomLeft.x + distance, 0, bottomLeft.z); // nouveau point entre D et C
		GameObject cube5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var mr4 = cube5.GetComponent<MeshRenderer>();
		mr4.material.color = Color.cyan;
		cube5.transform.position = newPointBL_BR;

		Rectangle R1 = new Rectangle(topLeft, newPointTL_TR, newPointTL_BL, newPointMiddle, 1);
		subdivisionList.Add(R1);

		Rectangle R2 = new Rectangle(newPointTL_TR, topRight, newPointMiddle, newPointTL_BL, 2);
		subdivisionList.Add(R2);

		Rectangle R3 = new Rectangle(newPointTL_BL, newPointMiddle, bottomLeft, newPointBL_BR, 3);
		subdivisionList.Add(R3);

		Rectangle R4 = new Rectangle(newPointMiddle, newPointTR_BR, newPointBL_BR, bottomRight, 4);
		subdivisionList.Add(R4);

		return subdivisionList;
	}

	public int getIndex()
	{
		return id;
	}
}
