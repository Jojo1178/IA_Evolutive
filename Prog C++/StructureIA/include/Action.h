#ifndef ACTION_H
#define ACTION_H

#include <iostream>
#include <list>
#include "Consequence.h"
class Action
{
    public:
        Action();
        virtual ~Action();

        std::list<Consequence> GetAllConsequences();
        Consequence GetConsequencesByIndex(int);
        void AddConsequence(Consequence);
        void RemoveLastConsequence();
        void RemoveConsequenceByindex(int);

    protected:
    private:
        std::list<Consequence>Consequences;

};

#endif // ACTION_H
