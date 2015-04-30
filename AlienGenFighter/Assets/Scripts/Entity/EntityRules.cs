using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EntityRules
{
	private List<RulesGroup>[] _rules;
	public EntityRules()
	{
		_rules = new List<RulesGroup>[]
		{
			new List<RulesGroup>(),
			new List<RulesGroup>(),
			new List<RulesGroup>()
		};
		//mourir - pritity 0
		RulesGroup rg = new RulesGroup();
		rg.GetRuleList().Add(new Rules(new RuleCondition(HasZeroFood), new RuleAction(Die)));
		_rules[0].Add(rg);
		rg = new RulesGroup();
		rg.GetRuleList().Add(new Rules(new RuleCondition(HasZeroWater), new RuleAction(Die)));
		_rules[0].Add(rg);
		//manger - priority 0
		rg = new RulesGroup();
		rg.GetRuleList().Add(new Rules(new RuleCondition(isHungry), new RuleAction(SearchForEat)));
		rg.GetRuleList().Add(new Rules(new RuleCondition(CanGoToFood), new RuleAction(GoToEat)));
		rg.GetRuleList().Add(new Rules(new RuleCondition(CanEat), new RuleAction(Eat)));
		_rules[0].Add(rg);
		//boire - priority 0
		rg = new RulesGroup();
		rg.GetRuleList().Add(new Rules(new RuleCondition(isTthirsty), new RuleAction(SearchForWater)));
		rg.GetRuleList().Add(new Rules(new RuleCondition(CanGoToWater), new RuleAction(GoToWater)));
		rg.GetRuleList().Add(new Rules(new RuleCondition(CanDrink), new RuleAction(Drink)));
		_rules[0].Add(rg);

		//avancer aleatoirement - priority 2
		rg = new RulesGroup();
		rg.GetRuleList().Add(new Rules(new RuleCondition(CanMove), new RuleAction(Move)));
		_rules[2].Add(rg);

	}

	public List<RulesGroup>[] GetRules()
	{
		return _rules;
	}

	public bool b = true;




	#region CONDITION_DEFINITION
	#region MOURIR
	private bool HasZeroFood(SquareContext context)
	{
		return context.GetCurrentEntity().GetState().GetFood() <= 0;
	}
	private bool HasZeroWater(SquareContext context)
	{
		return context.GetCurrentEntity().GetState().GetWater() <= 0;
	}
	#endregion
	#region MANGER
	private bool isHungry(SquareContext context)
	{
		return context.GetCurrentEntity().GetState().GetFood() < 70;
	}
	private bool CanGoToFood(SquareContext context)
	{
		for(var i = 0 ; i < context.GetStaticEntitiesByType("Food").Count ; ++i)
		{
			if(Vector3.Distance(context.GetStaticEntitiesByType("Food")[i].GetTransform().position,
				context.GetCurrentEntity().GetTransform().position) < 5f)
			{
				context.GetCurrentEntity().GetState().SetTargetedFood(context.GetStaticEntitiesByType("Food")[i] as EdibleScript);
				return true;
			}

		}
		return false;
	}
	private bool CanEat(SquareContext context)
	{
		return Mathf.Abs(context.GetCurrentEntity().GetTransform().position.x - context.GetCurrentEntity().GetState().GetTargetedFood().GetTransform().position.x) <= 0.5f
				&& Mathf.Abs(context.GetCurrentEntity().GetTransform().position.z - context.GetCurrentEntity().GetState().GetTargetedFood().GetTransform().position.z) <= 0.5f
				&& context.GetCurrentEntity().GetState().GetTargetedFood().GetQuantity() > 0;
	}
	#endregion
	#region BOIRE
	private bool isTthirsty(SquareContext context)
	{
		return context.GetCurrentEntity().GetState().GetWater() < 70;
	}
	private bool CanGoToWater(SquareContext context)
	{
		for(var i = 0 ; i < context.GetStaticEntitiesByType("Water").Count ; ++i)
		{
			if(Vector3.Distance(context.GetStaticEntitiesByType("Water")[i].GetTransform().position,
				context.GetCurrentEntity().GetTransform().position) < 5f)
			{
				context.GetCurrentEntity().GetState().SetTargetedWater(context.GetStaticEntitiesByType("Water")[i] as EdibleScript);
				return true;
			}

		}
		return false;
	}
	private bool CanDrink(SquareContext context)
	{
		return Mathf.Abs(context.GetCurrentEntity().GetTransform().position.x - context.GetCurrentEntity().GetState().GetTargetedWater().GetTransform().position.x) <= 0.5f
				&& Mathf.Abs(context.GetCurrentEntity().GetTransform().position.z - context.GetCurrentEntity().GetState().GetTargetedWater().GetTransform().position.z) <= 0.5f
				&& context.GetCurrentEntity().GetState().GetTargetedWater().GetQuantity() > 0;
	}
	#endregion
	#region BOUGER
	private bool CanMove(SquareContext context)
	{
		return Mathf.Abs(context.GetCurrentEntity().GetMovement().GetPosition().x - context.GetCurrentEntity().GetMovement().GetTargetPosition().x) <= 0.1f
			   && Mathf.Abs(context.GetCurrentEntity().GetMovement().GetPosition().z - context.GetCurrentEntity().GetMovement().GetTargetPosition().z) <= 0.1f;
	}

	#endregion
	#endregion
	#region ACTION_DEFINITION
	#region MOURIR
	private void Die(SquareContext context)
	{
		EntityManagerScript.AddToQueue(context.GetCurrentEntity());
		return;
	}
	#endregion
	#region MANGER
	private void SearchForEat(SquareContext context)
	{
		return;
		//imaginer une animation ou il cherche a manger ou alors il se tient le ventre ...
	}
	private void GoToEat(SquareContext context)
	{
		//Debug.Log("Go To eat");
		context.GetCurrentEntity().GetMovement().SetTargetPosition(context.GetCurrentEntity().GetState().GetTargetedFood().GetTransform().position);
	}
	private void Eat(SquareContext context)
	{
		//Debug.Log("Eat !!");
		var f = 1;
		context.GetCurrentEntity().GetState().GetTargetedFood().Take(f); //TODO : a voir le nombre de food
		context.GetCurrentEntity().GetState().SetFood(context.GetCurrentEntity().GetState().GetFood() + f);
	}
	#endregion
	#region BOIRE
	private void SearchForWater(SquareContext context)
	{
		return;
		//imaginer une animation ou il cherche a manger ou alors il se tient le ventre ...
	}
	private void GoToWater(SquareContext context)
	{
		Debug.Log("Go To drink");
		context.GetCurrentEntity().GetMovement().SetTargetPosition(context.GetCurrentEntity().GetState().GetTargetedWater().GetTransform().position);
	}
	private void Drink(SquareContext context)
	{
		Debug.Log("Eat !!");
		var f = 1;
		context.GetCurrentEntity().GetState().GetTargetedWater().Take(f);  //TODO : a voir le nombre de food
		context.GetCurrentEntity().GetState().SetWater(context.GetCurrentEntity().GetState().GetWater() + f);
	}
	#endregion
	#region BOUGER
	private void Move(SquareContext context)
	{
		//Debug.Log("Entity " + context.GetCurrentEntity() + " is moving");
		var pos = context.GetCurrentEntity().GetMovement().GetPosition();
		context.GetCurrentEntity().GetMovement().SetTargetPosition(new Vector3(pos.x + Random.Range(-15f, 15f), 1, pos.z + Random.Range(-15f, 15f)));
	}
	#endregion
	#endregion
}
