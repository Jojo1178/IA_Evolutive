using UnityEngine;
using System.Collections;

public class Objectives : MonoBehaviour {

	private bool longTerm;
	private Types type;
	private int researchedScore;
	private Actions finalAction;

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

	public virtual void Create(bool IsLongTerm,Types ObjectiveType, int TheResearchedScore,Actions TheFinalAction)
	{
		longTerm = IsLongTerm;
		type = ObjectiveType;
		researchedScore = TheResearchedScore;
		finalAction = TheFinalAction;
	}
}
