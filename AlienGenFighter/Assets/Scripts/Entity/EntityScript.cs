using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class EntityScript : MonoBehaviour
{
	[SerializeField] private Transform				_transform;
	[SerializeField] private GameObject				_gameObject;
	[SerializeField] private Rigidbody				_rigidbody;
	[SerializeField] private EntityMovementScript	_movement;
	[SerializeField] private NetworkView			_networkView;
	[SerializeField] private EntityCollisionScript	_collider;

	private DnaScript		_dna;
	private CapacityScript	_capacities;

	private bool			_isPlayable = false;
	private bool			_isAlive = true;

    private EntityRules _rules;
	private EntityStateScript _state;
	public void Init()
	{
		_dna = new DnaScript();
		_dna.SetGeneAt(ECharateristic.Height, 1);
		_capacities = new CapacityScript();
        _rules = new EntityRules();
		_state = new EntityStateScript();
		_movement.Init();
	}
	void Update()
	{
		if (_isPlayable)
		{
			//TODO: things
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

	[RPC]
	public void DisableComponents()
	{
		_movement.enabled = false;
		_networkView.enabled = false;
		_isPlayable = false;
		_movement.SetPlayable(false);
		this.enabled = false;
	}
	[RPC]
	public void EnableComponents()
	{
		this.enabled = true;
		_movement.enabled = true;
		_networkView.enabled = true;
		_isPlayable = true;
		_movement.SetPlayable(true);
		_isAlive = true;
		_transform.parent = null;
	}
	public EntityCollisionScript GetColliderScript()
	{
		return _collider;
	}
	public void SetColliderScript(EntityCollisionScript col)
	{
		_collider = col;
	}
	public Transform GetTransform()
	{
		return _transform;
	}
	public void SetTransform(Transform tr)
	{
		_transform = tr;
	}
	public EntityMovementScript GetMovement()
	{
		return _movement;
	}
	public void SetMovement(EntityMovementScript mv)
	{
		_movement = mv;
	}
    public EntityRules GetRules()
    {
        return _rules;
    }
    public void SetRules(EntityRules rules)
    {
        _rules = rules;
    }
	public EntityStateScript GetState()
	{
		return _state;
	}
	public void SetState(EntityStateScript state)
	{
		_state = state;
	}
}

