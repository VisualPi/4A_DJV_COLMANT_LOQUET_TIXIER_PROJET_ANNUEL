using UnityEngine;

public interface OtherTargetable
{
    void Start();
    void OnTriggerEnter(Collider col);
    Transform Transform { get; }
    int Take(int value);
}
