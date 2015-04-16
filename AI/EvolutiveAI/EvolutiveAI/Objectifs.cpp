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

void Objectifs::SetType(Types t)
{
    type = t;
}

bool Objectifs::IsLong()
{
    return Long;
}

void Objectifs::SetIsLong(bool l)
{
    Long = l;
}
