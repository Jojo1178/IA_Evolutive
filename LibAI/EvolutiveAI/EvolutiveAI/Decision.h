#ifndef DECISION_H
#define DECISION_H

#include "Objectifs.h"
#include "monde.h"
#include "ActionTree.h"

class Decision
{
    public:
        Decision();
		Decision(monde *CurWorld);
        virtual ~Decision();


		std::vector<int> CollectActionsForLongTermObjective(Objectifs LongTermObjective); // Collecte les actions pour l'objectif a long terme
		std::vector<int> CollectActionsForShortTermObjective(Objectifs ShortTermObjective); // Collecte les actions pour les objectifs a courts terme
		ActionTree CreateActionTree(Objectifs LongTermObjective, Objectifs ShortTermObjective, int MaxNumberOfActions); // Création et envoi de l'action tree grâce à l'algo d'apprentissage.

    protected:
    private:
		monde *World;
		int CurScore;

};

#endif // DECISION_H
