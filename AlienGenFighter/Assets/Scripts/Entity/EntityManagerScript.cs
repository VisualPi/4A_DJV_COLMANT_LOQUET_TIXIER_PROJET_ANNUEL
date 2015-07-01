using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Context;
using Assets.Scripts.Group;

public class EntityManagerScript : MonoBehaviour
{
    [SerializeField]
    private NetworkView _networkView;
    public static Queue<EntityScript> AvailableEntities;
    [SerializeField]
    public EntityScript[] tab;

    [SerializeField] public GroupScript[] tabGroups;
    public static Queue<GroupScript> AvailableGroups; 

    private static int nbInstanciated;

    public void Start()
    {
        AvailableEntities = new Queue<EntityScript>(tab.Length);
        for ( int index = 0 ; index < tab.Length ; index++ )
        {
            EntityScript t = tab[index];
            AddToQueue(t);
        }
        nbInstanciated = 0;
        tab = null;
        AvailableGroups = new Queue<GroupScript>(tabGroups.Length);
        for ( var index = 0 ; index < tabGroups.Length ; index++ )
        {
            GroupScript t = tabGroups[index];
            AddGroupToQueue(t);
        }
        //nbInstanciated = 0;
        tabGroups = null;
    }
    public static EntityScript GetFromQueue()
    {
        var e = AvailableEntities.Dequeue();
        e.EnableComponents();
        e.name = "Entity_" + nbInstanciated;
        //e.collider.name = 
        GameData.Entities.Add(e.name, e);
        nbInstanciated++;
        return e;
    }
    public static GroupScript GetGroupFromQueue()
    {
        var g = AvailableGroups.Dequeue();
        g.GetComponent<Collider>().enabled = true; //TODO : a voir
        return g;
    }
    public static void AddGroupToQueue(GroupScript g)
    {
        g.Collider.enabled = false;
        g.Transform.position = new Vector3(-2000,1000,5000); //TODO:mouais
        AvailableGroups.Enqueue(g);
    }
    [RPC]
    public static void AddToQueue(EntityScript e)
    {
        //TODO : ajouter la gestion de groupe lors de la mort, leader etc...
        e.DisableComponents();
        AvailableEntities.Enqueue(e);
    }
    public static void AddToQueueAndMove(EntityScript e)
    {
        GameData.Entities.Remove(e.name);
        e.DisableComponents();
        e.GetTransform().position = new Vector3(10000, 10000, AvailableEntities.Count+10);
        AddToQueue(e);
    }
}
