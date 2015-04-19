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

        RulesGroup rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(isHungry), new RuleAction(SearchForEat)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanGoToFood), new RuleAction(GoToEat)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanEat), new RuleAction(Eat)));
        _rules[0].Add(rg);

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
    #region MANGER
    private bool isHungry(SquareContext context)
    {
		//tester si l'entité a besoin de manger
		return false; //pour qu'il break tout de suite (pas implémenté)
    }
    private bool CanGoToFood(SquareContext context)
    {
        //tester s'il y a de la bouffe a proxi
        return true;
    }
    private bool CanEat(SquareContext context)
    {
        //tester si il peut manger (du coté nouriture pas du coté entité)
        return true;
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
	#region MANGER
	private void SearchForEat(SquareContext context)
    {
        //imaginer une animation ou il cherche a manger ou alors il se tient le ventre ...
    }
    private void GoToEat(SquareContext context)
    {
        //bouge jusqu'a la location de la bouffe
    }
    private void Eat(SquareContext context)
    {
        //manger
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
