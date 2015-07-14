using UnityEngine;
using System.Collections;

public class Actions {


	private int actionID;
	private string actionName;
	private int score;

    private int numberRequired;
    private World CurrentWorld;

	public string ActionName {
		get {
			return actionName;
		}
		set {
			actionName = value;
		}
	}

    public int NumberRequired
    {
        get
        {
            return numberRequired;
        }
        set
        {
            numberRequired = value;
        }
    }

	public int ActionID {
		get {
			return actionID;
		}
		set {
			actionID = value;
		}
	}

	public int Score {
		get {
			return score;
		}
		set {
			score = value;
		}
	}

	// Use this for initialization
	public virtual void Start () {
	
	}

	// Update is called once per frame
	public virtual void Update () {
	
	}

	public int GetUpdatedScore()
	{

        int Compteur = 0; 
        
        switch(ActionID)
        {
            case 1: Compteur = CurrentWorld.CompteurWood; break;
            case 2: Compteur = CurrentWorld.CompteurFood; break;
            case 3: Compteur = CurrentWorld.CompteurBuild; break;
            default: Compteur = 0; break;
        }

        if (NumberRequired <= Compteur)
            return Score;
        else
            return Score * -1;
	}

	public virtual void Create(int ID, string Name,int ActionsScore, ref World CurWorld,int RequiredNumber = 0)
	{
		actionID = ID;
		actionName = Name;
        score = ActionsScore;
        CurrentWorld = CurWorld;
        CurWorld.ActionList.Add(this);
        NumberRequired = RequiredNumber;
	}


}
