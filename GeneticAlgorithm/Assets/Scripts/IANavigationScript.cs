using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IANavigationScript : MonoBehaviour {

    //definition des limites de la map
	float minX = -50;
	float maxX = 50;
	float minZ = -50;
	float maxZ = 50;

    public drawFieldOfView drawFieldOfViewScript;
    private Dictionary<string, bool> lastPosition = new Dictionary<string,bool>();

    //Donnees de la destination
	public Transform destination;
    //Script Pointofinterrest
    public PointOfInterest pointOfInterestScript;
    //Script GameManager
    public GameManager gameManagerScript;

    //NavMEsh Agent
	public NavMeshAgent agent;
    
    //Determine si le trajet actuel de l'IA est terminé ou pas
    [HideInInspector]
	public bool travelFinished = false;

    //Vector correspondant aux coordonnees de la nouvelle destination
	private Vector3 newDestination = new Vector3();

    public bool gather = false;

    public string dest = "none";

    public bool objectDetected = false;

    public GameObject exclamation;
    bool isWaiting = false;



	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(destination.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (objectDetected)
        {

        }
        else
        {
            if (!gameManagerScript.researchFinished)
            {
                if (travelFinished == false)
                {
                    checkReachedPath();
                }
                else
                {
                    double x, z;
                    if (gameManagerScript.numberOfRessources > 0)
                    {
						for(int i = 0; i < 20; i++)
						{
							newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
							if(CheckIfDestinationIsOK(newDestination) == true)
							{}
							else
								newDestination = new Vector3(UnityEngine.Random.Range(minX, maxX), 0.5f, UnityEngine.Random.Range(minZ, maxZ));
						}


                        travelFinished = false;
                        agent.SetDestination(newDestination);
                    }
                }
            }
            else
            {
                if (travelFinished == false)
                {
                    checkReachedPath();
                }
                else
                {
					//Debug.Log("Tout est trouvé !!!");
                    if (dest.Equals("base"))
                    {
                        agent.SetDestination(gameManagerScript.baseStackManagerScript.transform.position);
                        travelFinished = false;
                    }
                    else if (dest.Equals("wood"))
                    {
                        int closestIndex = 0;
                        for (int i = 0; i < pointOfInterestScript.wood.Count; i++)
                        {
                            if (Vector3.Distance(transform.position, pointOfInterestScript.wood[closestIndex]) > Vector3.Distance(transform.position, pointOfInterestScript.wood[i]))
                                closestIndex = i;
                        }
                        agent.SetDestination(pointOfInterestScript.wood[closestIndex]);
                        travelFinished = false;
                    }
                    else if (dest.Equals("food"))
                    {
                        int closestIndex = 0;
                        for (int i = 0; i < pointOfInterestScript.food.Count; i++)
                        {
                            if (Vector3.Distance(transform.position, pointOfInterestScript.food[closestIndex]) > Vector3.Distance(transform.position, pointOfInterestScript.food[i]))
                                closestIndex = i;
                        }
                        agent.SetDestination(pointOfInterestScript.food[closestIndex]);
                        travelFinished = false;
                    }
                    else if (dest.Equals("build"))
                    {
                        int closestIndex = 0;
                        for (int i = 0; i < gameManagerScript.baseStackManagerScript.BuildingPlaces.Count; i++)
                        {
                            if (Vector3.Distance(transform.position, gameManagerScript.baseStackManagerScript.BuildingPlaces[closestIndex].Key) > Vector3.Distance(transform.position, gameManagerScript.baseStackManagerScript.BuildingPlaces[i].Key))
                                closestIndex = i;
                        }
                        agent.SetDestination(gameManagerScript.baseStackManagerScript.BuildingPlaces[closestIndex].Key);
                        travelFinished = false;
                    }
                }
                //if (gameObject.name == "Capsule 1")
                //{
                //    var bPosition = gameManagerScript.baseStackManagerScript.transform;
                //    var wPosition = pointOfInterestScript.wood[0];
                //    var nPosition = pointOfInterestScript.food[0];
                //
                //    if (travelFinished == false)
                //    {
                //        checkReachedPath();
                //    }
                //    else
                //    {
                //        if (dest == "none")
                //        {
                //            agent.SetDestination(bPosition.position);
                //            dest = "base";
                //        }
                //        else if (dest == "base")
                //        {
                //            agent.SetDestination(wPosition);
                //            dest = "wood";
                //        }
                //        else if (dest == "wood" || dest == "food")
                //        {
                //            agent.SetDestination(bPosition.position);
                //            dest = "base";
                //        }
                //    }
                //}
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
			pointOfInterestScript.pointOfInterest.Add(collision.gameObject.transform.position);
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

    public IEnumerator popEventDetection(Vector3 position)
    {
        isWaiting = true;
        exclamation.SetActive(true);
        agent.Stop();
        yield return new WaitForSeconds(1);
        setAgentDestination(position);
        agent.Resume();
        exclamation.SetActive(false);
        drawFieldOfViewScript.detectionMode = false;
    }

	public bool CheckIfDestinationIsOK(Vector3 newDestination)
	{
		bool result;
		result = gameManagerScript.ResearchInAreaEnabled(newDestination);
		if(result == false)
			Debug.Log("Tout est deja decouvert dans la zone de recherche");
		return result;
	}
	
}
