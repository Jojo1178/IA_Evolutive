using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FoodScript : MonoBehaviour {
	
	//definition des limites de la map
	float minX = -25;
	float maxX = 25;
	float minZ = -25;
	float maxZ = 25;

	Animator animator;

	//public drawFieldOfView drawFieldOfViewScript;
	//private Dictionary<string, bool> lastPosition = new Dictionary<string,bool>();
	
	//Donnees de la destination
	public Transform destination;
	//Script Pointofinterrest
	//public PointOfInterest pointOfInterestScript;
	//Script GameManager
	//public GameManager gameManagerScript;
	
	//NavMEsh Agent
	private NavMeshAgent agent;
	
	//Determine si le trajet actuel de l'IA est terminé ou pas
	[HideInInspector]
	public bool travelFinished = false;
	
	//Vector correspondant aux coordonnees de la nouvelle destination
	private Vector3 newDestination = new Vector3();
	bool agentPause = false;
	//public bool gather = false;
	
	//public string dest = "none";
	
	//public bool objectDetected = false;
	
	//public GameObject exclamation;
	bool isWaiting = false;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
		animator.SetBool("isMoving", true);
		travelFinished = false;
		agent.SetDestination(newDestination);
		
		//var x = Math.Floor(destination.position.x / 4);
		//var z = Math.Floor(destination.position.z / 4);
		
		//lastPosition.Add(x + " " + z, true);
	}
	
	// Update is called once per frame
	void Update () {

			if (travelFinished == false)
			{
				//animation.CrossFade("Horse_Walk");
				checkReachedPath();
			}
			else
			{
				//animation.Play("Horse_Idle");
				double x, z;

				newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
				travelFinished = false;
				StartCoroutine("pauseAgent");
				agent.SetDestination(newDestination);

			}

	}

	
	// Check if we've reached the destination
	void checkReachedPath()
	{
		if(!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
					travelFinished = true;
			}
		}
	}
	
	public void setAgentDestination(Vector3 newDestination)
	{
		agent.SetDestination(newDestination);
	}
	
	public IEnumerator pauseAgent(){
		agent.Stop();
		animator.SetBool("isMoving", false);
		//agent.destination = gameObject.transform.position;
		yield return new WaitForSeconds(5);
		//agent.destination = newDestination;
		//agentPause = false;
		animator.SetBool("isMoving", true);
		agent.Resume();
	}
}
