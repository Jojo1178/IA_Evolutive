using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public struct SavedAction
{
    private Objectives objective;

    public Objectives Objective
    {
        get { return objective; }
        set { objective = value; }
    }
    private ActionTrees actionTree;

    public ActionTrees ActionTree
    {
        get { return actionTree; }
        set { actionTree = value; }
    }

    public void Create(Objectives newObjective, ActionTrees newTree)
    {
        Objective = newObjective;
        ActionTree = newTree;
    }
}

public class IAManager : MonoBehaviour
{

    [HideInInspector]
    public AI LearningAI;
    [HideInInspector]
    public World CurrentWorld;
    [HideInInspector]
    public ActionTrees OrdersChain;
    public GameManager Manager;
    public GameObject TextAIContent;
    public Font AIFont;
    public Color AIColor;
    public Text AIObjectif;
    public Text AIPool;
    public Text AIPoolPass;
    public Text AITree;

    private int CurrentOrder;
    private int PreviousOrder;
    private bool Creation = true;
    private int ObjIt = 0;
    private int LineCounter = 0;
    private List<Actions> ActionsPossible = new List<Actions>();
    private List<Objectives> ObjectivesList = new List<Objectives>();
    private List<SavedAction> savedAct = new List<SavedAction>();
    private Text AIText;
    private string[] AISentence = {
        "Je retourne à la base.",
        "Je vais chercher du bois.",
        "Je vais chercher de la nourriture.",
        "Je vais construire un bâtiment.",
        "Je vais rafiner du bois.",
        "Je vais créer un nouvel habitant.",
        "Je veux construire un bâtiment, mais il me manque du bois rafiné.",
        "Je veux construire un bâtiment, mais il me manque du bois.",
        "Je veux rafiner du bois, mais il me manque du bois.",
        "Je veux créer un nouvel habitant, mais il me manque de la nourriture."
};


    // Use this for initialization
    void Start()
    {
        CurrentOrder = 0;
        AddNewLineOfText("J'explore la carte");
    }

    // Update is called once per frame
    void Update()
    {
        if (Creation && Manager.numberOfRessources == 0)
        {
            AddNewLineOfText("La carte à été explorée");
            AddNewLineOfText("Je créé ma liste d'objectif");
            Creation = false;
            CurrentWorld = gameObject.AddComponent<World>();
            CurrentWorld.CharacterList = Manager.CharacterList;
            CurrentWorld.WoodList = Manager.pointOfInterrestScript.woodGO;
            CurrentWorld.FoodList = Manager.pointOfInterrestScript.foodGO;
            CreateObjectives();
            CreateActionTree(ObjectivesList[3], 3);
            UpdateRightCanvas(ObjectivesList[3].ObjectiveName);
            AddNewLineOfText("Mon nouvel objectif principal est de : "+ "Construire un bâtiment");
            DoAction();
            ObjIt = 1;
        }
        if (OrdersChain != null && Manager.ActionDone)
        {
            if (CurrentOrder < OrdersChain.ListOfActions.Count)
                DoAction();
            else
            {
                CurrentOrder = 0;
                switch (ObjIt)
                {
                    case 1:
                        CreateActionTree(ObjectivesList[4], 1);
                        UpdateRightCanvas(ObjectivesList[4].ObjectiveName);
                        AddNewLineOfText("Mon nouvel objectif principal est de : " + "Recolter de la nourriture");
                        ObjIt = 3;
                        break;
                    case 3:
                        CreateActionTree(ObjectivesList[3], 1);
                        UpdateRightCanvas(ObjectivesList[3].ObjectiveName);
                        AddNewLineOfText("Mon nouvel objectif principal est de : " + "Construire un bâtiment");
                        ObjIt = 1;
                        break;
                    case 6:
                        CreateActionTree(ObjectivesList[6], 1);
                        UpdateRightCanvas(ObjectivesList[6].ObjectiveName);
                        AddNewLineOfText("Mon nouvel objectif principal est de : " + "Créer un nouvel habitant");
                        ObjIt = 3;
                        break;
                }
                // CreateActionTree(ObjIt == 1 ? ObjectivesList[4] : ObjectivesList[3], ObjIt == 1 ? 1 : 3);
                DoAction();

                //ObjIt = (ObjIt == 0 ? 1 : 0);
            }
        }
    }

    private void UpdateRightCanvas(string objectiveName)
    {
        AIObjectif.text = "Objective :\n"+objectiveName;
        string poolComp = "";
        foreach (Actions act in CurrentWorld.ActionList)
        {
            poolComp += act.ActionName + ", ";
        }
        AIPool.text = "Complete Pool :\n" + poolComp;
        string poolPassComp = "";
        foreach (Actions act in CurrentWorld.ActionsPossible)
        {
            poolPassComp += act.ActionName + ", ";
        }
        AIPoolPass.text = "Pool After Pass :\n" + poolPassComp;
        string treeComp = "";
        foreach (int i in CurrentWorld.ActionTreeSelected.ListOfActions)
        {
            treeComp += CurrentWorld.GetActionById(i).ActionName + ", ";
        }
        AITree.text = "Tree :\n" + treeComp;
    }

    private void AddNewLineOfText(string text)
    {
        GameObject textAI = new GameObject("line" + LineCounter);
        AIText = textAI.AddComponent<Text>();
        AIText.color = AIColor;
        AIText.font = AIFont;
        AIText.text = text;
        AIText.alignment = TextAnchor.MiddleCenter;
        LayoutElement le = textAI.AddComponent<LayoutElement>();
        le.minHeight = 50;
        ContentSizeFitter csf = textAI.AddComponent<ContentSizeFitter>();
        csf.verticalFit = ContentSizeFitter.FitMode.MinSize;
        textAI.transform.SetParent(TextAIContent.transform);
        LineCounter++;
    }

    private void CreateActionTree(Objectives longObjective, int interation)
    {
        if (!ActionTreeAlreadyExist(longObjective))
        {
            LearningAI = gameObject.AddComponent<AI>();
            LearningAI.Create(longObjective, interation, CurrentWorld);
            OrdersChain = LearningAI.ChooseActions();

            SavedAction newSave = new SavedAction();
            newSave.Create(longObjective, OrdersChain);
            savedAct.Add(newSave);
        }
        else
        {
            OrdersChain = GetActionTreeByObjective(longObjective);
        }

        if (OrdersChain != null)
        {
            Debug.Log("Order Size : " + OrdersChain.ListOfActions.Count);
        }
    }

    private void DoAction()
    {
        int IDAction = OrdersChain.ListOfActions[CurrentOrder];
        Debug.Log("Current Action : " + IDAction);
        SendOrder(IDAction);
        CurrentOrder++;
        PreviousOrder = CurrentOrder - 1;
        Manager.ActionDone = false;
    }

    private void SendOrder(int IDAction)
    {
        Actions ActionToDo = CurrentWorld.GetActionById(IDAction);
        foreach (IAGathering gather in CurrentWorld.CharacterList)
        {
            gather.Quantity = 0;
            switch (ActionToDo.ActionName)
            {
                case "GetWood":
                    AddNewLineOfText(AISentence[1]);
                    gather.Quantity = ActionToDo.NumberRequired;
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                    CurrentWorld.CompteurWood += gather.Inventory.Length;
                    break;
                case "GetFood":
                    AddNewLineOfText(AISentence[2]);
                    gather.Quantity = ActionToDo.NumberRequired;
                    gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_FOOD;
                    CurrentWorld.CompteurFood += gather.Inventory.Length;
                    break;
                case "BuildBuliding":
                    if (CurrentWorld.CompteurRefinedWood >= 10)
                    {
                        AddNewLineOfText(AISentence[3]);
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.BUILDING;
                        CurrentWorld.CompteurRefinedWood -= 10;
                        CurrentWorld.CompteurBuild += ActionToDo.NumberRequired;
                    }
                    else if (CurrentWorld.CompteurWood >= 10)
                    {
                        AddNewLineOfText(AISentence[6] + "\n" + AISentence[4]);
                        CurrentWorld.CompteurWood -= 10;
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.REFINED_WOOD;
                        CurrentWorld.CompteurRefinedWood += gather.Inventory.Length;
                        CurrentOrder = PreviousOrder;
                    }
                    else
                    {
                        AddNewLineOfText(AISentence[7] + "\n" + AISentence[1]);
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                        CurrentWorld.CompteurWood += gather.Inventory.Length;
                        CurrentOrder = PreviousOrder;
                    }
                    break;
                case "RefinedWood":
                    if (CurrentWorld.CompteurWood >= 10)
                    {
                        AddNewLineOfText(AISentence[4]);
                        gather.Quantity = ActionToDo.NumberRequired;
                        CurrentWorld.CompteurWood -= 10;
                        gather.GatheringState = IAGathering.GATHERING_STATE.REFINED_WOOD;
                        CurrentWorld.CompteurRefinedWood += gather.Inventory.Length;
                    }
                    else
                    {
                        AddNewLineOfText(AISentence[8] + "\n" + AISentence[1]);
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_WOOD;
                        CurrentWorld.CompteurWood += gather.Inventory.Length;
                        CurrentOrder = PreviousOrder;
                    }

                    break;
                case "Reproduce":
                    if (CurrentWorld.CompteurFood >= 10)
                    {
                        AddNewLineOfText(AISentence[5]);
                        CurrentWorld.CompteurFood -= 10;
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.REPRODUCE;
                    }
                    else
                    {
                        AddNewLineOfText(AISentence[8] + "\n" + AISentence[2]);
                        gather.Quantity = ActionToDo.NumberRequired;
                        gather.GatheringState = IAGathering.GATHERING_STATE.COLLECTING_FOOD;
                        CurrentWorld.CompteurFood += gather.Inventory.Length;
                        CurrentOrder = PreviousOrder;
                    }
                    break;
                default:
                    AddNewLineOfText(AISentence[0]);
                    gather.GatheringState = IAGathering.GATHERING_STATE.BRINGTOBASE;
                    break;
            }
        }

    }

    private void CreateObjectives()
    {
        Objets foodobjet = new Objets();
        foodobjet.Create(1, "Food");
        Objets woodobjet = new Objets();
        woodobjet.Create(1, "Wood");
        Objets refinedwoodobjet = new Objets();
        refinedwoodobjet.Create(1, "RefinedWood");
        Objets baseobjet = new Objets();
        baseobjet.Create(1, "Base");
        Objets buildingobjet = new Objets();
        buildingobjet.Create(1, "Building");

        CurrentWorld.AllInteractions = new List<Consequences>();
        Actions action = new Actions();
        Actions action2 = new Actions();
        Actions action3 = new Actions();
        Actions action4 = new Actions();
        Actions action5 = new Actions();
        Actions action6 = new Actions();


        action.Create(1, "GetWood", 10, ref CurrentWorld);
        ActionsPossible.Add(action);

        action2.Create(2, "GetFood", 10, ref CurrentWorld);
        ActionsPossible.Add(action2);

        action3.Create(3, "BuildBuliding", 20, ref CurrentWorld);
        ActionsPossible.Add(action3);

        action4.Create(4, "ReturnToBase", 5, ref CurrentWorld);
        ActionsPossible.Add(action4);

        action5.Create(5, "RefinedWood", 8, ref CurrentWorld);
        ActionsPossible.Add(action5);

        action6.Create(6, "Reproduce", 15, ref CurrentWorld);
        ActionsPossible.Add(action6);

        CurrentWorld.ActionList = ActionsPossible;

        List<Actions> acts = new List<Actions>();
        List<Actions> acts2 = new List<Actions>();
        List<Actions> acts3 = new List<Actions>();
        List<Actions> acts4 = new List<Actions>();
        List<Actions> acts5 = new List<Actions>();
        List<Actions> acts6 = new List<Actions>();
        List<Actions> acts7 = new List<Actions>();
        List<Actions> acts8 = new List<Actions>();

        List<Objets> obejs = new List<Objets>();
        List<Objets> obejs2 = new List<Objets>();
        List<Objets> obejs3 = new List<Objets>();
        List<Objets> obejs4 = new List<Objets>();
        List<Objets> obejs5 = new List<Objets>();
        List<Objets> obejs6 = new List<Objets>();
        List<Objets> obejs8 = new List<Objets>();

        Objectives objs = new Objectives();
        Objectives objs2 = new Objectives();
        Objectives objs3 = new Objectives();
        Objectives objs4 = new Objectives();
        Objectives objs5 = new Objectives();
        Objectives objs6 = new Objectives();
        Objectives objs8 = new Objectives();

        Consequences csq = new Consequences();
        Consequences csq2 = new Consequences();
        Consequences csq3 = new Consequences();
        Consequences csq4 = new Consequences();
        Consequences csq5 = new Consequences();
        Consequences csq6 = new Consequences();
        Consequences csq7 = new Consequences();
        Consequences csq8 = new Consequences();

        Types t = new Types();
        Types t2 = new Types();
        Types t3 = new Types();
        Types t4 = new Types();
        Types t5 = new Types();
        Types t6 = new Types();
        Types t8 = new Types();


        t.Create(1, "GetWood", ref CurrentWorld);
        objs.Create("GetWood", false, t, 1, CurrentWorld.GetActionById(1), Typeobjectif.WOOD);
        ObjectivesList.Add(objs);
        acts.Add(ActionsPossible[0]);
        obejs.Add(woodobjet);
        csq.Create(acts, obejs, 1, "Collecting Wood", t, ref CurrentWorld);

        t2.Create(2, "GetFood", ref CurrentWorld);
        objs2.Create("GetFood", false, t2, 1, CurrentWorld.GetActionById(2), Typeobjectif.FOOD);
        ObjectivesList.Add(objs2);
        acts2.Add(ActionsPossible[1]);
        obejs2.Add(foodobjet);
        csq2.Create(acts2, obejs2, 2, "Collecting Food", t2, ref CurrentWorld);

        t3.Create(3, "BuildBuilding", ref CurrentWorld);
        objs3.Create("BuildBuilding", false, t3, 2, CurrentWorld.GetActionById(3), Typeobjectif.WOOD);
        ObjectivesList.Add(objs3);
        acts3.Add(ActionsPossible[2]);
        obejs3.Add(buildingobjet);
        csq3.Create(acts3, obejs3, 3, "Build Building", t3, ref CurrentWorld);

        t4.Create(4, "HaveBuliding", ref CurrentWorld);
        objs4.Create("Build1Buildings", true, t4, 3, CurrentWorld.GetActionById(3), Typeobjectif.WOOD);
        ObjectivesList.Add(objs4);
        acts4.Add(ActionsPossible[2]);
        obejs4.Add(buildingobjet);
        csq4.Create(acts4, obejs4, 4, "Build 1 Building", t4, ref CurrentWorld);

        t5.Create(5, "Have20FoodUnit", ref CurrentWorld);
        objs5.Create("Have20FoodUnit", true, t5, 2, CurrentWorld.GetActionById(2), Typeobjectif.FOOD);
        ObjectivesList.Add(objs5);
        acts5.Add(ActionsPossible[1]);
        obejs5.Add(foodobjet);
        csq5.Create(acts5, obejs5, 5, "Have 20 Food Unit", t5, ref CurrentWorld);

        t6.Create(6, "Reproduce", ref CurrentWorld);
        objs6.Create("Reproduce", false, t6, 5, CurrentWorld.GetActionById(5), Typeobjectif.FOOD);
        ObjectivesList.Add(objs6);
        acts6.Add(ActionsPossible[5]);
        obejs6.Add(foodobjet);
        csq6.Create(acts6, obejs6, 7, "Reproduce", t6, ref CurrentWorld);

        acts7.Add(ActionsPossible[4]);
        csq7.Create(acts7, obejs3, 6, "RefinedWood", t3, ref CurrentWorld);

        t8.Create(8, "Have2Child", ref CurrentWorld);
        objs8.Create("Have2Child", true, t8, 10, CurrentWorld.GetActionById(5), Typeobjectif.FOOD);
        ObjectivesList.Add(objs8);
        acts8.Add(ActionsPossible[5]);
        obejs8.Add(foodobjet);
        csq8.Create(acts8, obejs8, 8, "Have 2 Child", t8, ref CurrentWorld);
    }

    public void GiveOrder(GameObject character, IAGathering.GATHERING_STATE order)
    {
        IAGathering gatheringScript = character.GetComponent<IAGathering>();
        if (gatheringScript)
            gatheringScript.GatheringState = order;
    }

    public bool ActionTreeAlreadyExist(Objectives obj)
    {
        for (int i = 0; i < savedAct.Count; i++)
        {
            if (savedAct[i].Objective == obj)
            {
                return true;
            }
        }

        return false;
    }

    ActionTrees GetActionTreeByObjective(Objectives obj)
    {
        for (int i = 0; i < savedAct.Count; i++)
        {
            if (savedAct[i].Objective == obj)
            {
                return savedAct[i].ActionTree;
            }
        }
        return null;
    }
}
