#ifndef OBJETS_H
#define OBJETS_H

#include <iostream>
#include <list>
#include "Action.h"

class Objets
{
    public:
        Objets();
        virtual ~Objets();
protected:
	int ObjectId;
	std::string Object;
    private:

};

#endif // OBJETS_H
