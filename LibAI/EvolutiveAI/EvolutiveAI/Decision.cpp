#include "Decision.h"
#include <iterator>

Decision::Decision()
{
    //ctor
}

Decision::Decision(monde *CurWorld)
{
	World = CurWorld;
}

Decision::~Decision()
{
    //dtor
}


std::vector<int> Decision::CollectActionsForLongTermObjective(Objectifs LongTermObjective)
{
	return World->GetConsequencesByType(LongTermObjective.GetType());
}

std::vector<int> Decision::CollectActionsForShortTermObjective(Objectifs ShortTermObjective)
{
	return World->GetConsequencesByType(ShortTermObjective.GetType());

}
ActionTree Decision::CreateActionTree(Objectifs LongTermObjective,Objectifs ShortTermObjective, int MaxNumberOfActions)
{
	ActionTree SelectedActions;
	std::vector<int> PossibleConsequences = CollectActionsForLongTermObjective(LongTermObjective);
	std::vector<Action> Actions;
	int tempScore = 0;

	for (std::vector<int>::iterator it = PossibleConsequences.begin(); it != PossibleConsequences.end(); ++it)
	{
		Consequence tempcons = World->GetConsequenceByID(*it);

		std::vector<Action> tempAct = tempcons.GetAllActions();

		int ActionScore = 0;
		Action tempSelectedAction;

		for (std::vector<Action>::iterator Act = tempAct.begin(); Act != tempAct.end(); ++Act)
		{
			if (ActionScore == 0 && (*Act).GetScore() > 0)
			{
				ActionScore = (*Act).GetScore();
				tempSelectedAction = *Act;
			}
			else
				if ((*Act).GetScore() > ActionScore)
				{
					ActionScore = (*Act).GetScore();
					tempSelectedAction = *Act;
				}
		}

		if (ActionScore > 0)
			Actions.push_back(tempSelectedAction);

		tempScore += ActionScore;

		if (tempScore >= LongTermObjective.GetResearchedScore())
		{
			SelectedActions.CreateActionTree(Actions, tempScore);
			return SelectedActions;
		}
	}

	return SelectedActions;
}
