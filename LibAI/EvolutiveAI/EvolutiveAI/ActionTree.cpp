#include "ActionTree.h"


bool ActionTree::SaveActionTree()
{
	// Fonction de sauvegarde : a cr�er 
	return false;
}

void ActionTree::FuseTrees(ActionTree OtherTree)
{
	//Fonction avec algo g�n�tique pour fusionner deux arbres d'actions
}

void ActionTree::CreateActionTree(std::vector<Action> ActionArray, int Score)
{
	ListOfActions = ActionArray;
	TreeScore = Score;
}

std::vector<Action> ActionTree::GetActionTree()
{
	return ListOfActions;
}



