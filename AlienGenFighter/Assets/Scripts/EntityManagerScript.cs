using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManagerScript : MonoBehaviour
{
	public List<EntityScript> _AvailableAliveEntities;
	//private List<EntityScript> _AvailableDeadEntities; //a voir 

	public EntityScript GetNewAliveEntity()
	{
		var ret = _AvailableAliveEntities[_AvailableAliveEntities.Count - 1];
		_AvailableAliveEntities.RemoveAt(_AvailableAliveEntities.Count - 1); 
		return ret;
	}
	
	//public EntityScript GetNewDeadEntity()
	//{
	//	var ret = _AvailableDeadEntities[_AvailableDeadEntities.Count - 1];
	//	_AvailableDeadEntities.RemoveAt(_AvailableDeadEntities.Count - 1);
	//	return ret;
	//}

	public void AddNewAliveEntity(EntityScript e)
	{
		_AvailableAliveEntities.Add(e);
	}

	//public void AddNewDeadEntity(EntityScript e)
	//{
	//	_AvailableDeadEntities.Add(e);
	//}
}
