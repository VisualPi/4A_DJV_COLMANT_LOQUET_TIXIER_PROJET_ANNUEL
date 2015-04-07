using UnityEngine;
using System.Collections;

public class EntityScript : MonoBehaviour
{
	[SerializeField] public Transform				_transform;
	[SerializeField] public GameObject				_gameObject;
	[SerializeField] public Rigidbody				_rigidbody;
	[SerializeField] public EntityMovementScript	_movement;
	[SerializeField] public NetworkView				_networkView;

	private DnaScript		_dna;
	private CapacityScript	_capacities;

	private bool			_isPlayable = false;
	private bool			_isAlive = true;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (_isPlayable)
		{
			//TODO:
		}
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

	public bool GetPlayable()
	{
		return _isPlayable;
	}
	public bool GetAlive()
	{
		return _isAlive;
	}

	[RPC]
	public void SetPlayable(bool b)
	{
		_isPlayable = b;
	}
	[RPC]
	public void SetAlive(bool b)
	{
		_isAlive = b;
	}

	[RPC] //pas sur
	public void DisableComponents()
	{
		_movement.enabled = false;
		_networkView.enabled = false;
		_isPlayable = false;
		this.enabled = false;
	}
	[RPC] //pas sur
	public void EnableComponents()
	{
		this.enabled = true;
		_movement.enabled = true;
		_networkView.enabled = true;
		_isPlayable = true;
		_isAlive = true;
	}

}
