#include "monde.h"

monde::monde()
{
    //ctor
}

monde::~monde()
{
    //dtor
}
std::vector<int> monde::GetConsequencesByType(Types ExpectedType)
{
	std::vector<int> ConsequencesByType;

	for (unsigned int i = 0; i < AllInteractions.size(); ++i)
	{
		if (AllInteractions[i].GetType() == ExpectedType)
		{
			ConsequencesByType.emplace_back(AllInteractions[i].GetID());
		}
	}

	return ConsequencesByType;
}

Consequence monde::GetConsequenceByID(int id)
{
	Consequence empty;
	for (unsigned int i = 0; i < AllInteractions.size(); ++i)
	{
		if (AllInteractions[i].GetID() == id)
			return AllInteractions[i];
	}

	return empty;
}


