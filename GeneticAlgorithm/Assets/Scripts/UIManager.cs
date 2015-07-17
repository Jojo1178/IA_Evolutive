using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public BaseStacksManager Stacks;

    public Text WoodStock;
	public Text RefinedWoodStock;
    public Text FoodStock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        WoodStock.text = "Wood : " + Stacks.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.WOOD] +"/"+ Stacks.ResourcesMaxValues[BaseStacksManager.RESOURCES_TYPE.WOOD];
        RefinedWoodStock.text = "Refined Wood : " + Stacks.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.WOOD_REFINED] +"/"+ Stacks.ResourcesMaxValues[BaseStacksManager.RESOURCES_TYPE.WOOD_REFINED];
        FoodStock.text = "Food : " + Stacks.ResourcesStacked[BaseStacksManager.RESOURCES_TYPE.FOOD] + "/" + Stacks.ResourcesMaxValues[BaseStacksManager.RESOURCES_TYPE.FOOD];
	}
}
