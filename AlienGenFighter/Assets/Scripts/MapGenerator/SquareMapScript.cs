using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Context
{
	private int _nbEntityOnCurrentSquare = 0;
	private List<EntityScript> _entityOnCurrentSquare = new List<EntityScript>();

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
}


public class SquareMapScript : MonoBehaviour
{
	private float moveSpeedInfluence = 1.0f;
	private float temperature = 25.0f;
	
	private int foodQuantity = 50;
	private int drinkableWater = 1000;

	[SerializeField] private Collider _collider;

	private Context _context;
	void Start()
	{
		//consumeResources(10, 1000);
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
		//Debug.Log("Enter on square");
		if (col.tag.Equals("entity"))
			_context.SetNbEntityOnCurrentSquare(_context.GetNbEntityOnCurrentSquare()+1);
	}
	public void OnTriggerExit(Collider col)
	{
		//Debug.Log("Exit from square");
		if(col.tag.Equals("entity"))
			_context.SetNbEntityOnCurrentSquare(_context.GetNbEntityOnCurrentSquare()+1);
		if(_context.GetNbEntityOnCurrentSquare() < 0)
			_context.SetNbEntityOnCurrentSquare(0);//TODO : au cas ou pour les tests, a enlever apres
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
		Debug.Log("NB ENTITE : " + _context.GetNbEntityOnCurrentSquare());
		Debug.Log("NOURRITURE : " + foodQuantity.ToString());
		Debug.Log("EAU : " + drinkableWater.ToString());
	}

	public Context GetContext()
	{
		return _context;
	}
	public void SetContext(Context context)
	{
		_context = context;
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
		for (int i = 0; i < _context.GetNbEntityOnCurrentSquare(); ++i)
		{
			_context.GetEntityOnCurrentSquare()[i].GetColliderScript().SetLastCol(_collider.name);
        }
	}
}
