using UnityEngine;
using System.Collections;

public class IAManager : MonoBehaviour {

    public AI LearningAI;
    public World CurrentWorld;

	// Use this for initialization
	void Start () {

        Objectives objs = new Objectives();
        Types t = new Types();
        Actions act = new Actions();
        t.Create(1,"GetWood");
        act.Create(1,"Drop Wood");
        objs.Create(true,t,10,act);

        LearningAI.Create(objs, 3, CurrentWorld);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
