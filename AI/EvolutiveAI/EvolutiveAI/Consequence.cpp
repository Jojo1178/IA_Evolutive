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
