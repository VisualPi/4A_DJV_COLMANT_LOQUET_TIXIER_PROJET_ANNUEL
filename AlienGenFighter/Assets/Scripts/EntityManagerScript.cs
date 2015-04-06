using UnityEngine;
using System.Collections.Generic;

public class EntityManagerScript : MonoBehaviour
{
	[SerializeField] public NetworkView _networkView;
	private readonly Queue<EntityScript> _AvailableEntities = new Queue<EntityScript>();

	public EntityScript GetFromQueue()
	{
		var e = _AvailableEntities.Dequeue();
		e.EnableComponents(); //TODO : RPC ?!
		return e;
	}
	[RPC]
	public void AddToQueue(EntityScript e)
	{
		e.DisableComponents(); //TODO: RPC ?!
		_AvailableEntities.Enqueue(e);
	}
}
