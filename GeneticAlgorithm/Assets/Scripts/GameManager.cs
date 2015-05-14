using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//int woodStock = 0;
	//int woodMinimum = 10;
	//int woodCollectQuantity = 2;

    //public Transform basePosition;
    public BaseStacksManager baseStackManagerScript;

    //Nombre de ressources remarquable sur la map
	public int numberOfRessources = 8;
    //Définies sur la recherche est terminé ou pas
    public bool researchFinished = false;
    //script PointOfInterest
	public PointOfInterest pointOfInterrestScript;

    //Variable d'etat du jeu
    private bool phase1; //Phase d'exploration de la map
    private bool phase2; //Phase de recolte des IA

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
}
