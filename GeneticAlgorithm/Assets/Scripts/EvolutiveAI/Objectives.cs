using UnityEngine;
using System.Collections;


public enum Typeobjectif
{
    FOOD,
    WOOD,
    BUILD
}


public class Objectives{

	private bool longTerm;
	private Types type;
	private int researchedScore;
	private Actions finalAction;


    private Typeobjectif objectifType;

    public Typeobjectif ObjectifType
    {
        get { return objectifType; }
        set { objectifType = value; }
    }

	public bool LongTerm {
		get {
			return longTerm;
		}
		set {
			longTerm = value;
		}
	}

	public Types Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}

	public int ResearchedScore {
		get {
			return researchedScore;
		}
		set {
			researchedScore = value;
		}
	}

	public Actions FinalAction {
		get {
			return finalAction;
		}
		set {
			finalAction = value;
		}
	}

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void Create(bool IsLongTerm,Types ObjectiveType, int TheResearchedScore,Actions TheFinalAction, Typeobjectif objtype)
	{
		longTerm = IsLongTerm;
		type = ObjectiveType;
		researchedScore = TheResearchedScore;
		finalAction = TheFinalAction;
        objectifType = objtype;
	}

    public static bool operator ==(Objectives obj1, Objectives obj2)
    {
        return (obj1.LongTerm == obj2.LongTerm 
            && obj1.Type == obj2.Type  
            && obj1.ResearchedScore == obj2.ResearchedScore
            && obj1.ObjectifType == obj2.ObjectifType
            && obj1.FinalAction == obj2.FinalAction);
    }

    public static bool operator !=(Objectives obj1, Objectives obj2)
    {
        return (obj1.LongTerm != obj2.LongTerm
            && obj1.Type != obj2.Type
            && obj1.ResearchedScore != obj2.ResearchedScore
            && obj1.ObjectifType != obj2.ObjectifType
            && obj1.FinalAction != obj2.FinalAction);
    }
}
