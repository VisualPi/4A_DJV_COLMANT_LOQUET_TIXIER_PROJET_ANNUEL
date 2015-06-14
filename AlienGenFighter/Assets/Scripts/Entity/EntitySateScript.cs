using UnityEngine;
using System.Collections;

public class EntityStateScript
{
	private int _food = 50;
	private int _water = 50;
	private EdibleScript _targetedFood;
	private EdibleScript _targetedWater;

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

	public EdibleScript GetTargetedFood()
	{
		return _targetedFood;
	}
	public void SetTargetedFood(EdibleScript value)
	{
		_targetedFood = value;
	}
	public EdibleScript GetTargetedWater()
	{
		return _targetedWater;
	}
	public void SetTargetedWater(EdibleScript value)
	{
		_targetedWater = value;
	}
}
