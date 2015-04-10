using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareMapScript : MonoBehaviour
{
	private float moveSpeedInfluence = 1.0f;
	private float temperature = 25.0f;
	
	private int foodQuantity = 50;
	private int drinkableWater = 1000;

	[SerializeField] private Collider _collider;
	private int _nbEntityOnCurrentSquare = 0;
	private List<EntityScript> _entityOnCurrentSquare;
	void Start()
	{
		//consumeResources(10, 1000);
		_entityOnCurrentSquare = new List<EntityScript>();
	}
	void Update()
	{
		
	}
	void OnMouseDown()
	{
		getResources();
	}
	public void OnTriggerEnter(Collider col)
	{
		if (col.tag.Equals("Entity"))
			_nbEntityOnCurrentSquare++;
	}
	public void OnTriggerExit(Collider col)
	{
		Debug.Log("Exit from square");
		if(col.tag.Equals("Entity"))
			_nbEntityOnCurrentSquare--;
	}
	void setEnvironnementalConstraint(float _moveSpeedInfluence, float _temperature)
	{
		moveSpeedInfluence = _moveSpeedInfluence;
		temperature = _temperature;

	}
	private void setResources(int _foodQuantity, int _drinkableWater)
	{
		foodQuantity = _foodQuantity;
		drinkableWater = _drinkableWater;
	}
	public void getResources()
	{
		Debug.Log("NOURRITURE : " + foodQuantity.ToString());
		Debug.Log("EAU : " + drinkableWater.ToString());
	}
	public int GetNbEntityOnCurrentSquare()
	{
		return _nbEntityOnCurrentSquare;
	}
	public void SetNbEntityOnCurrentSquare(int value)
	{
		_nbEntityOnCurrentSquare = value;
	}
	public List<EntityScript> GetEntityOnCurrentSquare()
	{
		return _entityOnCurrentSquare;
	}
	public void SetEntityOnCurrentSquare(List<EntityScript> value)
	{
		_entityOnCurrentSquare = value;
	}
	void consumeResources(int _foodQuantity, int _drinkableWater)
	{
		foodQuantity = foodQuantity - _foodQuantity;
		drinkableWater = drinkableWater - _drinkableWater;
	}
	void modifyEnvironnementalConstraint(float valueOfChangeInPercent)
	{
		moveSpeedInfluence *= valueOfChangeInPercent;
		temperature *= valueOfChangeInPercent;
	}

	void SetEntityLastMapName()
	{
		for (int i = 0; i < _nbEntityOnCurrentSquare; ++i)
		{
			_entityOnCurrentSquare[i].GetColliderScript().SetLastCol(_collider.name);
        }
	}
}
