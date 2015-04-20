using UnityEngine;
using System.Collections;

public class ObjectDetectionScript : MonoBehaviour {

	private bool isDetected = false;

	public PointOfInterest script;
    public GameManager script2;

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
                        script.wood.Add(gameObject.transform.position);
                        print("A tree is added to the list, number of element = " + script.wood.Count); 
                        break;
                    case 9:
                        script.food.Add(gameObject.transform.position);
                        print("A food is added to the list, number of element = " + script.food.Count); 
                        break;
                    default:
                        print("Unknow object detected");
                        break;
                }
				//script.addElement(collider.transform.position);
                script2.SubstractRessource();
			}
		}
	}
}
