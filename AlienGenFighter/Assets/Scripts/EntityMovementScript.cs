using UnityEngine;
using System.Collections;

public class EntityMovementScript : MonoBehaviour
{
	public Transform	_transform;
	private Vector3		_startPosition;
	private Vector3		_targetPosition;
	private float		_distancePosition = 0f;

	private float		_animeStartTime = 0f;

	private float		_entitySpeed = 5f;

	// Use this for initialization
	void Start()
	{
		_startPosition = _transform.position;
		_targetPosition = _startPosition;
		//SetTargetPosition(new Vector3(0f,1f,10f));
	}

	// Update is called once per frame
	void Update()
	{
		var animPercentage = (Time.time - _animeStartTime) * _entitySpeed;
		_transform.position = Vector3.Lerp(_startPosition, _targetPosition, animPercentage / _distancePosition);
	}

	public Vector3 GetTargetPosition()
	{
		return _targetPosition;
	}
	[RPC]
	public void SetTargetPosition(Vector3 pos)
	{
		_startPosition = _transform.position;
		_animeStartTime = Time.time;
		_targetPosition = pos;
		_distancePosition = Vector3.Distance(_startPosition, _targetPosition);
	}

	[RPC]
	public void SetSpeed(float speed)
	{
		_entitySpeed = speed;
		//TODO : jouer avec le animDuration pour ça ...
	}


}
