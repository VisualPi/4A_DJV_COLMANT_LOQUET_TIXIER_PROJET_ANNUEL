using UnityEngine;
using System.Collections;

public class EntityScript : MonoBehaviour
{

	private DnaScript		_dna;
	private CapacityScript	_capacities;

	public Transform _transform;
	public GameObject _gameObject;
	public Rigidbody _rigidbody;


	//public EntityScript(DnaScript dna) //inutile ?!
	//{
	//	_dna = dna;
	//
	//}

	// Use this for initialization
	void Start()
	{
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
}
