#ifndef OBJECTIFS_H
#define OBJECTIFS_H

#include <iostream>
#include <list>
#include "Types.h"

class Objectifs
{
    public:
        Objectifs();
        virtual ~Objectifs();

        Types GetType();
        void SetType(Types t);

        bool IsLong();
        void SetIsLong(bool l);
    protected:
    private:
        bool Long;
        Types type;
};

#endif // OBJECTIFS_H
