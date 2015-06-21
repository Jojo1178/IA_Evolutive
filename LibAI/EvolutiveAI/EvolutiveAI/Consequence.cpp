#include "Consequence.h"

Consequence::Consequence()
{
    //ctor
}

Consequence::~Consequence()
{
    //dtor
}

Types Consequence::GetType()
{
    return type;
}
void Consequence::SetType(Types t)
{
    type = t;
}
std::vector<Action> Consequence::GetAllActions()
{
	return actions;
}
void Consequence::AddAction(Action act)
{
	actions.emplace_back(act);
}

std::vector<Objets> Consequence::GetAllObjects()
{
	return objet;
}

void Consequence::AddObject(Objets obj)
{
	objet.emplace_back(obj);
}

int Consequence::GetID()
{
	return ConsequenceId;
}

