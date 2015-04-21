using System;
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
		if (tab.Length<=0)
			throw new Exception("No Entity!");

		_AvailableEntities = new Queue<EntityScript>(tab.Length);
		for(int index = 0 ; index < tab.Length ; index++)
		{
			EntityScript e = tab[index];
			AddToQueue(e);
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
}
