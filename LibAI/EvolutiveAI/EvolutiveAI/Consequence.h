#ifndef CONSEQUENCE_H
#define CONSEQUENCE_H

#include <iostream>
#include <list>
#include <vector>
#include <stdlib.h>
#include "Types.h"
#include "Action.h"
#include "Objets.h"

class Consequence
{
    public:
        Consequence();
        virtual ~Consequence();

        Types GetType();
        void SetType(Types t);

		std::vector<Action> GetAllActions();
		void AddAction(Action act);

		std::vector<Objets> GetAllObjects();
		void AddObject(Objets obj);

		int GetID();


    protected:
		std::vector<Action> actions;
		std::vector<Objets> objet;
		int ConsequenceId;
		std::string ConsequenceName;
    private:
        Types type;
};

#endif // CONSEQUENCE_H
