#include "Objets.h"

Objets::Objets()
{
    //ctor
}

Objets::~Objets()
{
    //dtor
}

std::list<Action> Objets::GetAllAction()
{
    return ActionsPossibles;
}

Action Objets::GetActionByIndex(int index)
{
     std::list<Action>::const_iterator lit (ActionsPossibles.begin()), lend(ActionsPossibles.end());
     for(;lit!=lend;++lit)
     {
         index--;
         if(index == 0)
         {
             break;
         }
     }
     return *lit;
}

void Objets::AddAction(Action action)
{
    ActionsPossibles.push_back(action);
}

void Objets::RemoveLastAction()
{
    ActionsPossibles.pop_back();
}

void Objets::RemoveActionByindex(int index)
{
    std::list<Action>::iterator lit (ActionsPossibles.begin()), lend(ActionsPossibles.end());
     for(;lit!=lend;++lit)
     {
         index--;
         if(index == 0)
         {
             ActionsPossibles.erase(lit);
             break;
         }
     }
}
