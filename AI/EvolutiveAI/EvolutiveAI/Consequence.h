#ifndef CONSEQUENCE_H
#define CONSEQUENCE_H

#include <iostream>
#include <list>
#include "Types.h"

class Consequence
{
    public:
        Consequence();
        virtual ~Consequence();

        Types GetType();
        void SetType(Types t);
    protected:
    private:
        Types type;
};

#endif // CONSEQUENCE_H
