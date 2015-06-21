using UnityEngine;

public class EdibleInformations
{
    public int Quantity;
    public int DefaultQuantity = 1000;
    public Vector3 Position;
    public string Name;

    public EdibleInformations()
    {
        Quantity = 0;
        Position = Vector3.zero;
        Name = "";
    }
    public EdibleInformations(EdibleInformations e)
    {
        Quantity = e.Quantity;
        DefaultQuantity = e.DefaultQuantity;
        Position = e.Position;
        Name = e.Name;
    }


}
