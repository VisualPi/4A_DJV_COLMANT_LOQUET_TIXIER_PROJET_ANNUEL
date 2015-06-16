using UnityEngine;

public interface OtherTargetable
{
	void Start();
	void OnTriggerEnter(Collider col);
	Transform GetTransform();
	void Take(int value);
}
