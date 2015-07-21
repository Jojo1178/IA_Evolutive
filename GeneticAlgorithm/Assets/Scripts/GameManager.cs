using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	//int woodStock = 0;
	//int woodMinimum = 10;
	//int woodCollectQuantity = 2;

	//Test subdivision du terrain
	////////////////////////////////////////////////////////////////
	Rectangle terrain;
	
	public GameObject topLeftCorner;
	public GameObject topRightCorner;
	public GameObject BottomLeftCorner;
	public GameObject BottomRightCorner;
    public GameObject Player;
	bool limitSpecify = false;
	
	Dictionary<string, Vector3> elementList;
	Dictionary<int, int> elementInArea;
	
	List<Rectangle> divisionList;
	////////////////////////////////////////////////////////////////

    //public Transform basePosition;
    public BaseStacksManager baseStackManagerScript;

    public List<IAGathering> CharacterList;

    //Nombre de ressources remarquable sur la map
	public int numberOfRessources = 8;
    //Définies sur la recherche est terminé ou pas
    public bool researchFinished = false;
    //script PointOfInterest
	public PointOfInterest pointOfInterrestScript;
    public int NextBuildPlace;
    //Vrai quand une avtion est terminée
    public bool ActionDone = false;

	public Dictionary<string, Vector3> listOfElements = new Dictionary<string, Vector3>();

    //Variable d'etat du jeu
    private bool phase1; //Phase d'exploration de la map
    private bool phase2; //Phase de recolte des IA

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(limitSpecify == false)
		{
			if(topLeftCorner != null && topRightCorner != null && BottomLeftCorner != null && BottomRightCorner != null)
			{
				Debug.Log("Il y a " + listOfElements.Count + " dans l'environnement");
				limitSpecify = true;
				terrain = new Rectangle(topLeftCorner, topRightCorner, BottomLeftCorner, BottomRightCorner );
				divisionList = terrain.subdivideSquareBy4();
				createListAreaElement();
			}
		}

        if (numberOfRessources == 0)
        {
            researchFinished = true;
            phase1 = false;
            phase2 = true;
        }
	}

    public void SubstractRessource()
    {
        print("A ressource is substract");
        if (numberOfRessources > 0)
        {
            numberOfRessources -= 1;
            print(numberOfRessources);
        }
        if (numberOfRessources == 0)
        {
            researchFinished = true;
            phase1 = false;
            phase2 = true;
        }
    }

	public void addElementToList(Vector3 position, string name)
	{
		listOfElements.Add(name, position);
	}

	public Dictionary<string, Vector3> getDictionary()
	{
		return listOfElements;
	}

	public void substractElementDetected(GameObject go)
	{
		foreach(var rect in divisionList)
		{
			if(rect.isInRectangle(go.transform.position))
				elementInArea[rect.getIndex()] -= 1;
		}

	}

	public void createListAreaElement()
	{
		elementInArea = new Dictionary<int, int>();
		elementInArea.Add(1, 0);
		elementInArea.Add(2, 0);
		elementInArea.Add(3, 0);
		elementInArea.Add(4, 0);
		
		foreach(var elem in listOfElements)
		{	
			foreach(var rect in divisionList)
			{
				
				if(rect.isInRectangle(elem.Value))
				{
					
					int index = rect.getIndex();
					if(elementInArea.ContainsKey(index))
					{
						elementInArea[index] += 1;
					}
				}
			}
		}
	}

	public bool ResearchInAreaEnabled(Vector3 position)
	{
		foreach(var rect in divisionList)
		{
			if(rect.isInRectangle(position))
			{
				if(elementInArea[rect.getIndex()] > 0)
					return true;
				if(elementInArea[rect.getIndex()] == 0)
				{
					return false;
				}
			}
		}
		return false;
	}
}
