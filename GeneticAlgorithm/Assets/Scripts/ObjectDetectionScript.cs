using UnityEngine;
using System.Collections;

public class ObjectDetectionScript : MonoBehaviour {

	public bool isDetected = false;
	public PointOfInterest pointOfInterrestScript;
    public GameManager gameManagerScript;
	public GameObject shield;

	void Awake()
	{
		gameManagerScript.addElementToList(gameObject.transform.position, gameObject.name);
	}

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
			Debug.Log("Collision detecté !! " + collider.gameObject.name.ToString());
			if(collider.gameObject.name.ToLower().Contains("capsule") || collider.gameObject.name.ToLower().Contains("astronaut"))
			{

				gameManagerScript.substractElementDetected(gameObject);
				shield.SetActive(true);
				//print ("collision with IA");
				//var a = gameObject.GetComponent<MeshRenderer>();
				//a.material.color = Color.red;
				isDetected = true;
                switch (gameObject.layer)
                {
                    case 8:
                        pointOfInterrestScript.pointOfInterest2.Add(gameObject.name, gameObject.transform.position);
                        print("ajout de :"+ gameObject.name);
                        pointOfInterrestScript.wood.Add(gameObject.transform.position);
                        pointOfInterrestScript.woodGO.Add(gameObject);
                        print("A tree is added to the list, number of element = " + pointOfInterrestScript.wood.Count); 
                        break;
                    case 9:
                        pointOfInterrestScript.pointOfInterest2.Add(gameObject.name, gameObject.transform.position);
                        print("ajout de :" + gameObject.name);
                        pointOfInterrestScript.food.Add(gameObject.transform.position);
                        pointOfInterrestScript.foodGO.Add(gameObject);
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
