using UnityEngine;
using System.Collections;

public class EntityStateScript
{
	private int _food = 50;
	private int _water = 50;
	private EdibleInformations _targetedFood;
	private EdibleInformations _targetedWater;

    public int GetFood()
	{
		return _food;
	}
	public void SetFood(int food)
	{
		_food = food;
	}
	public int GetWater()
	{
		return _water;
	}
	public void SetWater(int drink)
	{
		_water = drink;
	}

	public EdibleInformations GetTargetedFood()
	{
		return _targetedFood;
	}
	public void SetTargetedFood(EdibleInformations value)
	{
		_targetedFood = value;
	}
	public EdibleInformations GetTargetedWater()
	{
		return _targetedWater;
	}
	public void SetTargetedWater(EdibleInformations value)
	{
		_targetedWater = value;
    }
}
