#pragma once
#include "Action.h"
#include "Objectifs.h"
#include <vector>



class ActionTree
{
public:

	bool SaveActionTree(); // SCript de sauvegarde
	void FuseTrees(ActionTree OtherTree); //Script de fusion de deux ActionTree (algo génétique)
	void CreateActionTree(std::vector<Action> ActionArray, int Score);
	std::vector<Action> GetActionTree();

protected:

private:
	std::vector<Action>ListOfActions;
	int TreeScore;
	Objectifs Objective;
};