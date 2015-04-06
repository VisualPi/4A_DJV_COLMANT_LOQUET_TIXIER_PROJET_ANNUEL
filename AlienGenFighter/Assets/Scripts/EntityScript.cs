using UnityEngine;
using System.Collections;

public class EntityScript : MonoBehaviour
{

	private DnaScript		_dna;
	private CapacityScript	_capacities;

	public Transform _transform;
	public GameObject _gameObject;
	public Rigidbody _rigidbody;

	private Vector3 _targetPosition;

	// Use this for initialization
	void Start()
	{
		_transform.LookAt(_targetPosition);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public DnaScript GetDNA()
	{
		return _dna;
	}
	public void SetDNA(DnaScript dna)
	{
		_dna = dna;
	}
	public CapacityScript GetCapacity()
	{
		return _capacities;
	}
	public void SetCapacity(CapacityScript cap)
	{
		_capacities = cap;
	}

	public Vector3 GetTargetPosition()
	{
		return _targetPosition;
	}
	public void SetTargetPosition(Vector3 pos)
	{
		_targetPosition = pos;
	}
}
