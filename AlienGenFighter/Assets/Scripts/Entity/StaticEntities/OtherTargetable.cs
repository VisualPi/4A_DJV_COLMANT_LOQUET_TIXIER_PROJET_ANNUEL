using UnityEngine;

public interface OtherTargetable
{
    void Start();
    void OnTriggerEnter(Collider col);
    Transform Transform { get; }
    void Take(int value);
}
