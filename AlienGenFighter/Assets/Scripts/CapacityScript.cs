using System.Collections.Generic;

public enum ECapacity
{
    Walk = 0, Run, Jump, Swim, Fly
};


public class CapacityScript
{
    private const byte NbCapacity = 5;
    private Dictionary<ECapacity, bool> _capacities;

    public CapacityScript()
    {
        _capacities = new Dictionary<ECapacity, bool>();
        for ( ECapacity i = 0 ; i < (ECapacity)NbCapacity ; ++i )
            _capacities.Add(i, false);
        _capacities[ECapacity.Walk] = true;
    }
    public bool GetCapacityAt(ECapacity cap)
    {
        return _capacities[cap];
    }
    public void SetCapacityAt(ECapacity cap, bool value)
    {
        _capacities[cap] = value;
    }
}
