using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IANavigationScript : MonoBehaviour {

	float minX = -25;
	float maxX = 25;
	float minZ = -25;
	float maxZ = 25;

    private Dictionary<string, bool> lastPosition = new Dictionary<string,bool>();

    //Donnees de la destination
	public Transform destination;
    //Script Pointofinterrest
    public PointOfInterest pointOfInterest;
    //Script GameManager
    public GameManager Manager;

    //NavMEsh Agent
	private NavMeshAgent agent;
    //Determine si le trajet actuel de l'IA est terminé ou pas
	private bool travelFinished = false;
    //Vector correspondant aux coordonnees de la nouvelle destination
	private Vector3 newDestination = new Vector3();

    public bool gather = false;

    public string dest = "none";

	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
		
		agent.SetDestination(destination.position);

        var x = Math.Floor(destination.position.x / 4);
        var z = Math.Floor(destination.position.z / 4);

        lastPosition.Add(x + " " + z, true);
        //lastPosition.Add(
        //lastPosition.Add(Convert.ToInt32(destination.position.x), destination.position.x + destination.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        //print(other2.researchFinished);
        if (!Manager.researchFinished)
        {
            //print(travelFinished);
            if (travelFinished == false)
            {
                checkReachedPath();
            }
            else
            {
                double x, z;
                //print(other2.numberOfRessources);
                if (Manager.numberOfRessources > 0)
                {
                    newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
                    x = Math.Floor(newDestination.x / 4);
                    z = Math.Floor(newDestination.z / 4);
                    while (lastPosition.ContainsKey(x + " " + z))
                    {
                        newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
                        x = Math.Floor(newDestination.x / 4);
                        z = Math.Floor(newDestination.z / 4);
                    }
                    travelFinished = false;
                    agent.SetDestination(newDestination);
                }
            }
        }
        else
        {
            if (gameObject.name == "Capsule 1")
            {
                var bPosition = Manager.Base.transform;
                var wPosition = pointOfInterest.wood[0];
                var nPosition = pointOfInterest.food[0];

                if (travelFinished == false)
                {
                    checkReachedPath();
                }
                else
                {
                    if (dest == "none")
                    {
                        agent.SetDestination(bPosition.position);
                        dest = "base";
                    }
                    else if (dest == "base")
                    {
                        agent.SetDestination(wPosition);
                        dest = "wood";
                    }
                    else if (dest == "wood" || dest == "food")
                    {
                        agent.SetDestination(bPosition.position);
                        dest = "base";
                    }
                }
            } 
        }
	}

	void OnCollisionEnter(Collision collision) {
		GameObject collidedGO = collision.gameObject;
		if(collidedGO.name == "FPSController")
			print ("collision with character");
		else
		{
			//print ("collision");
			pointOfInterest.pointOfInterest.Add(collision.gameObject.transform.position);
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
}
