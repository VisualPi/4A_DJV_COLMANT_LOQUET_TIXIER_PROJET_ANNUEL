using UnityEngine;
using System.Collections;

public class SquareMapScript : MonoBehaviour {

    float moveSpeedInfluence = 1.0f ;
    float temperature = 25.0f;
    int foodQuantity = 50;
    int drinkableWater = 1000;
	// Use this for initialization
	void Start () {
        consumeResources(10, 1000);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        getResources();
    }

    void setEnvironnementalConstraint(float _moveSpeedInfluence, float _temperature)
    {
        moveSpeedInfluence = _moveSpeedInfluence;
        temperature = _temperature;
        
    }

    void setResources(int _foodQuantity, int _drinkableWater)
    {
        foodQuantity = _foodQuantity;
        drinkableWater = _drinkableWater;
    }

    public void getResources()
    {
        Debug.Log("NOURRITURE : " + foodQuantity.ToString());
        Debug.Log("EAU : " + drinkableWater.ToString());
    }

    void consumeResources(int _foodQuantity, int _drinkableWater)
    {
        foodQuantity = foodQuantity - _foodQuantity;
        drinkableWater = drinkableWater - _drinkableWater;
    }

    void modifyEnvironnementalConstraint(float valueOfChangeInPercent)
    {
        moveSpeedInfluence = moveSpeedInfluence * valueOfChangeInPercent;
        temperature = temperature * valueOfChangeInPercent;

    }


}
