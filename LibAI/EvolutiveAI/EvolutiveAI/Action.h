#ifndef ACTION_H
#define ACTION_H

#include <iostream>
#include <list>

class Action
{
    public:
        Action();
        virtual ~Action();

		int GetId();
		
		std::string GetName();
		void SetName(std::string NewName);

		int GetScore();

    protected:
		int ActionId;
		std::string ActionName;

		int Score;
    private:
       

};

#endif // ACTION_H
