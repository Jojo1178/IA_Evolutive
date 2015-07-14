using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Decision{

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
		List<int> PossibleConsequences = monde.GetConsequencesByType(LongTermObjective.Type);
		List<int> SelectedActions = new List<int>();

		int TempScore = 0;
		int ArrayCounter = 0;

		ActionTrees CreatedTree = new ActionTrees();

		for (int i = 0; i < PossibleConsequences.Count; i++) 
		{
			Consequences TempCons = monde.GetConsequenceById(PossibleConsequences[i]);

            if (TempCons.Type != LongTermObjective.Type)
                continue;

            List<Actions> PossibleActions = TempCons.ActionsLinked;

			int ActionScore = 0;
			Actions TempSelectedAction = null;

			for(int j = 0; j < PossibleActions.Count; j++)
			{
				if(ActionScore == 0 && PossibleActions[j].Score > 0)
				{
					int Score = TempScore;
					if((Score += PossibleActions[j].Score) > TempScore)
					{
						ActionScore = PossibleActions[j].Score;
						TempSelectedAction = PossibleActions[j];
					}
				}
				else
				{
					if(PossibleActions[j].Score > ActionScore)
					{
						int Score = TempScore;
						if((Score += PossibleActions[j].Score) > TempScore)
						{
							ActionScore = PossibleActions[j].Score;
							TempSelectedAction = PossibleActions[j];
						}
					}
				}

			}

			if(ActionScore > 0)
			{
				SelectedActions.Add(TempSelectedAction.ActionID);
				ArrayCounter++;
			}

			TempScore += ActionScore;
			ActionScore = 0;

			if(TempScore >= LongTermObjective.ResearchedScore || SelectedActions.Count > MaximumNumberOfActions)
			{
                if (LongTermObjective.FinalAction != null)
                {
                    SelectedActions.Add(LongTermObjective.FinalAction.ActionID);
                }

				CreatedTree.ListOfActions = SelectedActions;
				CreatedTree.TreeScore = TempScore;


                if (TempScore < LongTermObjective.ResearchedScore && TempScore > 0)
                     LongTermObjective.ResearchedScore = TempScore;

				return CreatedTree;
			}
		}

		if(SelectedActions.Count > 0)
		{
            if (LongTermObjective.FinalAction != null)
            {
                SelectedActions.Add(LongTermObjective.FinalAction.ActionID);
            }
			CreatedTree.ListOfActions = SelectedActions;
			CreatedTree.TreeScore = TempScore;
		}

        if (TempScore < LongTermObjective.ResearchedScore && TempScore > 0)
            LongTermObjective.ResearchedScore = TempScore;

		return CreatedTree;
	}

	public void Create(World CurWorld)
	{
		monde = CurWorld;
		curScore = 0;
	}
}