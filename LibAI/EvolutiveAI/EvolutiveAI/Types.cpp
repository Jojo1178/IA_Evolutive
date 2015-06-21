#include "Types.h"

Types::Types()
{
    //ctor
}

Types::~Types()
{
    //dtor
}

int Types::GetID()
{
	return TypeID;
}
void Types::SetID(int NewID)
{
	TypeID = NewID;
}

std::string Types::GetName()
{
	return TypeName;
}
