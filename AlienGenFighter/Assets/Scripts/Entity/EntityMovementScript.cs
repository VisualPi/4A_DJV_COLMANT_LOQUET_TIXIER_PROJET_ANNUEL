using UnityEngine;
using System.Collections;
using System.Threading;

public class EntityMovementScript : MonoBehaviour
{
	[SerializeField] private Transform _transform;
	[SerializeField] private EntityScript _entity;

	private Vector3 _startPosition;
	private Vector3 _targetPosition;

	private float _distancePosition = 1f;
	private float _animeStartTime = 0f;
	private float _entitySpeed = 5f;

	private bool _isPlayable;

	void Start()
	{
		_startPosition = _transform.position;
		_targetPosition = _startPosition;
		SetTargetPosition(new Vector3(15,1,0));
	}
	void Update()
	{
		_isPlayable = true;
		if(_isPlayable)
		{
			RaycastHit hit;
			var animPercentage = (Time.time - _animeStartTime) * _entitySpeed;
			var nextPos = Vector3.Lerp(_startPosition, _targetPosition, animPercentage / _distancePosition);
			Ray ray = new Ray(new Vector3(nextPos.x, 255f, nextPos.z), Vector3.down);
			if (Physics.Raycast(ray, out hit, float.MaxValue, 1<<8))
			{
				_transform.position = new Vector3( hit.point.x, 
												   hit.point.y+ _entity.GetDNA().GetGeneAt(ECharateristic.Height), 
												   hit.point.z);
			}
        }
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
	}
	public void SetPlayable(bool b)
	{
		_isPlayable = b;
	}
	public Vector3 GetPosition()
	{
		return _transform.position;
	}
	public void SetPosition(Vector3 pos)
	{
		_transform.position = pos;
	}
}
