﻿using UnityEngine;
using System.Collections;

public class ObjectDetectionScript : MonoBehaviour {

	public bool isDetected = false;
	public PointOfInterest pointOfInterrestScript;
    public GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(isDetected == false)
		{
			if(collider.gameObject.name.ToLower().Contains("capsule"))
			{
				//print ("collision with IA");
				var a = gameObject.GetComponent<MeshRenderer>();
				a.material.color = Color.red;
				isDetected = true;
                switch (gameObject.layer)
                {
                    case 8:
                        pointOfInterrestScript.pointOfInterest2.Add(gameObject.name, gameObject.transform.position);
                        print("ajout de :"+ gameObject.name);
                        pointOfInterrestScript.wood.Add(gameObject.transform.position);
                        print("A tree is added to the list, number of element = " + pointOfInterrestScript.wood.Count); 
                        break;
                    case 9:
                        pointOfInterrestScript.pointOfInterest2.Add(gameObject.name, gameObject.transform.position);
                        print("ajout de :" + gameObject.name);
                        pointOfInterrestScript.food.Add(gameObject.transform.position);
                        print("A food is added to the list, number of element = " + pointOfInterrestScript.food.Count); 
                        break;
                    default:
                        pointOfInterrestScript.pointOfInterest2.Add(gameObject.name, gameObject.transform.position);
                        print("ajout de :" + gameObject.name);
                        print("Unknow object detected");
                        break;
                }
				//script.addElement(collider.transform.position);
                gameManagerScript.SubstractRessource();
			}
		}
	}
}
