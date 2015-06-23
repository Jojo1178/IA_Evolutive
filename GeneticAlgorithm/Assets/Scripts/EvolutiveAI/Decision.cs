using UnityEngine;
using System.Collections;

public class Decision : MonoBehaviour {

	private World monde;
	private int curScore;

	public World Monde {
		get {
			return monde;
		}
		set {
			monde = value;
		}
	}

	public int CurScore {
		get {
			return curScore;
		}
		set {
			curScore = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public ActionTrees CreateActionTree(Objectives LongTermObjective,int MaximumNumberOfActions)
	{
		int[] PossibleConsequences = monde.GetConsequencesByType(LongTermObjective.Type);
		int[] SelectedActions = null;



		int TempScore = 0;
		int ArrayCounter = 0;

		ActionTrees CreatedTree;

		SelectedActions.Initialize();
		CreatedTree = null;

		for (int i = 0; i < PossibleConsequences.Length; i++) 
		{
			Consequences TempCons = monde.GetConsequenceById(PossibleConsequences[i]);

			Actions[] PossibleActions = TempCons.ActionsList;

			int ActionScore = 0;
			Actions TempSelectedAction = null;

			for(int j = 0; j < PossibleActions.Length; j++)
			{
				if(ActionScore == 0 && PossibleActions[i].Score > 0)
				{
					int Score = TempScore;
					if((Score += PossibleActions[i].Score) > TempScore)
					{
						ActionScore = PossibleActions[i].Score;
						TempSelectedAction = PossibleActions[i];
					}
				}
				else
				{
					if(PossibleActions[i].Score > ActionScore)
					{
						int Score = TempScore;
						if((Score += PossibleActions[i].Score) > TempScore)
						{
							ActionScore = PossibleActions[i].Score;
							TempSelectedAction = PossibleActions[i];
						}
					}
				}

			}

			if(ActionScore > 0)
			{
				SelectedActions[ArrayCounter] = TempSelectedAction.ActionID;
				ArrayCounter++;
			}

			TempScore += ActionScore;
			ActionScore = 0;

			if(TempScore >= LongTermObjective.ResearchedScore || SelectedActions.Length > MaximumNumberOfActions)
			{
				SelectedActions[ArrayCounter] = LongTermObjective.FinalAction.ActionID;
				CreatedTree.ListOfActions = SelectedActions;
				CreatedTree.TreeScore = TempScore;

				return CreatedTree;
			}
		}

		if(SelectedActions.Length > 0)
		{
			SelectedActions[ArrayCounter] = LongTermObjective.FinalAction.ActionID;
			CreatedTree.ListOfActions = SelectedActions;
			CreatedTree.TreeScore = TempScore;
			

		}
		return CreatedTree;
	}

	public void Create(World CurWorld)
	{
		monde = CurWorld;
		curScore = 0;
	}
}