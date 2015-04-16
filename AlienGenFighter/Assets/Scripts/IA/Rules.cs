using UnityEngine;
using System.Collections;
using System;

public class Condition
{
	
}


public class Rules : MonoBehaviour
{
	public Action<EntityScript, Context> _action;
	public Condition _condition;
	public Rules(Condition cond, Action<EntityScript, Context> action)
	{
		_condition = cond;
		_action = action;
	}

	public void execute(EntityScript ent, Context cont)
	{
		_action(ent, cont);
	}

}
