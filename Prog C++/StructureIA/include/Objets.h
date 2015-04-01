#ifndef OBJETS_H
#define OBJETS_H

#include <iostream>
#include <list>
#include "Action.h"

class Objets
{
    public:
        Objets();
        virtual ~Objets();

        std::list<Action> GetAllAction();
        Action GetActionByIndex(int);
        void AddAction(Action);
        void RemoveLastAction();
        void RemoveActionByindex(int);
    protected:
    private:
        std::list<Action>ActionsPossibles;
};

#endif // OBJETS_H
