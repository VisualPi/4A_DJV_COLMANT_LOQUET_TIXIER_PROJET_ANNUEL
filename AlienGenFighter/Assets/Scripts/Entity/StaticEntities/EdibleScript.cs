using UnityEngine;
using System.Collections;

public class EdibleScript : MonoBehaviour, OtherTargetable
{
	[SerializeField] private Transform _transform;
	[SerializeField] private int _defaultQuantity = 100;
	[SerializeField] private Collider _collider;
	[SerializeField] private GameObject _gameObject;
	[SerializeField] private int _quantity;

	private string _lastCol = "";
	private string _type = "";
	// Use this for initialization
	public void Start()
	{
		_quantity = _defaultQuantity;
		_type = tag;
	}

	// Update is called once per frame
	void Update()
	{
		if (_quantity == _defaultQuantity/2) //TODO : a voir, fonctionnera que si on décremente la nourriture par 1 !!
		{
			_transform.localScale = tag.Equals("Water")
									? new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z / 2) 
									: new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z);
		}
		if(_quantity < 1)
		{
			MapManagerScript._SquareMaps[_lastCol].GetContext().GetStaticEntitiesByType(_type).Remove(this);
			Destroy(_gameObject);
		}
	}
	public void OnTriggerEnter(Collider col)
	{
		if(col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol))
		{
			if(!_lastCol.Equals(""))
				MapManagerScript._SquareMaps[_lastCol].GetContext().GetStaticEntitiesByType(_type).Remove(this);
			MapManagerScript._SquareMaps[col.name].GetContext().GetStaticEntitiesByType(_type).Add(this);
			Debug.Log("Now SquareMap : " + col.name + " list contains " + this.name);
			_lastCol = col.name;
		}
	}

	public Transform GetTransform()
	{
		return _transform;
	}
	public int GetQuantity()
	{
		return _quantity;
	}
	public void Take(int value)
	{
		_quantity -= value;
	}
}
