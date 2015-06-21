#ifndef TYPES_H
#define TYPES_H

#include <stdlib.h>
#include <string>

class Types
{
    public:
        Types();
        virtual ~Types();

		int GetID();
		void SetID(int NewID);

		std::string GetName();



		inline bool operator==(Types type)
		{
			return (TypeID == type.GetID() && TypeName == type.GetName());
		};


    protected:
		int  TypeID;
		std::string TypeName;
    private:
};

#endif // TYPES_H
