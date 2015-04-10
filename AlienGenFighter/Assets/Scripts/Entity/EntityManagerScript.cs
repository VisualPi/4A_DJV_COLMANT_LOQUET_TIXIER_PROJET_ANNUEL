using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EntityManagerScript : MonoBehaviour
{
	[SerializeField] private NetworkView _networkView;
	public Queue<EntityScript> _AvailableEntities;
	[SerializeField] public EntityScript[] tab;
	public void Start()
	{
		_AvailableEntities = new Queue<EntityScript>(tab.Length);
		for (int index = 0; index < tab.Length; index++)
		{
			EntityScript t = tab[index];
			AddToQueue(t);
		}

		tab = null;
	}
	public EntityScript GetFromQueue()
	{
		var e = _AvailableEntities.Dequeue();
		e.EnableComponents();
		return e;
	}
	[RPC]
	public void AddToQueue(EntityScript e)
	{
		e.DisableComponents();
		_AvailableEntities.Enqueue(e);
	}
}
