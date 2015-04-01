#include "Action.h"

Action::Action()
{
    //ctor
}

Action::~Action()
{
    //dtor
}


std::list<Consequence> Action::GetAllConsequences()
{
    return Consequences;
}

Consequence Action::GetConsequencesByIndex(int index)
{
     std::list<Consequence>::const_iterator lit (Consequences.begin()), lend(Consequences.end());
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

void Action::AddConsequence(Consequence consequence)
{
    Consequences.push_back(consequence);
}

void Action::RemoveLastConsequence()
{
    Consequences.pop_back();
}

void Action::RemoveConsequenceByindex(int index)
{
    std::list<Consequence>::iterator lit (Consequences.begin()), lend(Consequences.end());
     for(;lit!=lend;++lit)
     {
         index--;
         if(index == 0)
         {
             Consequences.erase(lit);
             break;
         }
     }
}
