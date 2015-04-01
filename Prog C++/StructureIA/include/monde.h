#ifndef MONDE_H
#define MONDE_H

#include <iostream>
#include <list>
#include "Objets.h"

class monde
{
    public:
        monde();
        virtual ~monde();

        std::list<Objets> GetAllObjets();
        Objets GetObjetsByIndex(int);
        void AddObjets(Objets);
        void RemoveLastObjets();
        void RemoveObjetsByindex(int);
    protected:
    private:
        std::list<Objets>ObjectsInWorld;

};

#endif // MONDE_H
