using UnityEngine;
using System.Collections;
using System.Threading;

public class EntityMovementScript : MonoBehaviour
{
	[SerializeField] private Transform _transform;
	[SerializeField] private EntityScript _entity;

	[SerializeField]
	private Vector3 _startPosition;
	[SerializeField]
	private Vector3 _targetPosition;

	[SerializeField]
	private float _distancePosition = 1f;
	[SerializeField]
	private float _animeStartTime = 0f;
	[SerializeField]
	private float _entitySpeed = 5f;

	[SerializeField]
	private bool _isPlayable;

	public void Init()
	{
		_startPosition = _transform.position;
		_targetPosition = _startPosition;
		_animeStartTime = 0f;
	}

	public void OnMouseDown()
	{
		SetTargetPosition(new Vector3(15,1,15));
		Debug.Log("GO");
	}
	void Update()
	{
		_isPlayable = true;
		if(_isPlayable)
		{
			RaycastHit hit;
			//var animPercentage = (Time.time - _animeStartTime) * _entitySpeed;
			//var nextPos = Vector3.Lerp(_startPosition, _targetPosition, animPercentage / _distancePosition);
		    var nextPos = _transform.position + (_targetPosition - _transform.position).normalized * _entitySpeed * Time.deltaTime;
			Ray ray = new Ray(new Vector3(nextPos.x, 520f, nextPos.z), Vector3.down);
			if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
			{
				//Debug.Log(hit.point);
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
