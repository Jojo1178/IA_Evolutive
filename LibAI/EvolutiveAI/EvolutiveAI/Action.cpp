#include "Action.h"

Action::Action()
{
    //ctor
}

Action::~Action()
{
    //dtor
}


int Action::GetId()
{
	return ActionId;
}

std::string Action::GetName()
{
	return ActionName;
}
void Action::SetName(std::string NewName)
{
	ActionName = NewName;
}

int Action::GetScore()
{
	if (true) // Si condition monde vraie (exemple : si action = construire
	{		  // si 10 bois ont étés récoltés = vraie, sinon false;
		return Score;
	}
	else
	{
		return Score * -1;
	}
}
