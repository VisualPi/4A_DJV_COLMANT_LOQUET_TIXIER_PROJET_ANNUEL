using UnityEngine;
using System.Collections.Generic;

public class EntityManagerScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView _networkView;
    public static Queue<EntityScript> _AvailableEntities;
    [SerializeField]
    public EntityScript[] tab;

    public void Start()
    {
        _AvailableEntities = new Queue<EntityScript>(tab.Length);
        for ( int index = 0 ; index < tab.Length ; index++ )
        {
            EntityScript t = tab[index];
            AddToQueue(t);
        }

        tab = null;
    }
    public static EntityScript GetFromQueue()
    {
        var e = _AvailableEntities.Dequeue();
        e.EnableComponents();
        return e;
    }
    [RPC]
    public static void AddToQueue(EntityScript e)
    {

        e.DisableComponents();
        _AvailableEntities.Enqueue(e);
    }
    public static void AddToQueueAndMove(EntityScript e)
    {
        e.GetTransform().position = new Vector3(10000, 10000, _AvailableEntities.Count+10);
        AddToQueue(e);
    }
}
