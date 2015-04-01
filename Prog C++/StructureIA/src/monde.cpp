#include "monde.h"

monde::monde()
{
    //ctor
}

monde::~monde()
{
    //dtor
}

std::list<Objets> monde::GetAllObjets()
{
    return ObjectsInWorld;
}

Objets monde::GetObjetsByIndex(int index)
{
     std::list<Objets>::const_iterator lit (ObjectsInWorld.begin()), lend(ObjectsInWorld.end());
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

void monde::AddObjets(Objets objets)
{
    ObjectsInWorld.push_back(objets);
}

void monde::RemoveLastObjets()
{
    ObjectsInWorld.pop_back();
}

void monde::RemoveObjetsByindex(int index)
{
    std::list<Objets>::iterator lit (ObjectsInWorld.begin()), lend(ObjectsInWorld.end());
     for(;lit!=lend;++lit)
     {
         index--;
         if(index == 0)
         {
             ObjectsInWorld.erase(lit);
             break;
         }
     }
}
