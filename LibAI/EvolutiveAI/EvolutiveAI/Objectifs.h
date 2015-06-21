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
		int GetResearchedScore();

        bool IsLongTerm();
        void SetIsLongTerm(bool l);
    protected:
    private:
        bool LongTerm;
        Types type;
		int ResearchedScore;
};

#endif // OBJECTIFS_H
