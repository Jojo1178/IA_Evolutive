using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public BaseStacksManager Stacks;

    public Text WoodStock;
    public Text FoodStock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        WoodStock.text = "Wood : " + Stacks.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.WOOD];
        FoodStock.text = "Food : " + Stacks.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.FOOD];
	}
}
