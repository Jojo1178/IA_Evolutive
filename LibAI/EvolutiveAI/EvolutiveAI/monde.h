#ifndef MONDE_H
#define MONDE_H

#include <iostream>
#include <list>
#include <vector>

#include "Consequence.h"
#include "Types.h"

class monde
{
    public:
        monde();
        virtual ~monde();

		std::vector<int> GetConsequencesByType(Types);
		Consequence GetConsequenceByID(int id);


    protected:
    private:
		std::vector<Consequence> AllInteractions;
		//Rassembler les différentes informations du monde dans cette classe
};

#endif // MONDE_H
