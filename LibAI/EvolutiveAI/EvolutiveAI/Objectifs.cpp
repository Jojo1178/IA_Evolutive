#include "Objectifs.h"

Objectifs::Objectifs()
{
    //ctor
}

Objectifs::~Objectifs()
{
    //dtor
}

Types Objectifs::GetType()
{
    return type;
}

int Objectifs::GetResearchedScore()
{
	return ResearchedScore;
}

void Objectifs::SetType(Types t)
{
    type = t;
}

bool Objectifs::IsLongTerm()
{
	return LongTerm;
}

void Objectifs::SetIsLongTerm(bool l)
{
	LongTerm = l;
}
