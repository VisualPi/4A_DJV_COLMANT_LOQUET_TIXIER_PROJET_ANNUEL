using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts.Context;
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
        ////manger - priority 0
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
    private bool HasZeroFood(EntityScript entity)
    {
        return entity.GetState().GetFood() <= 0;
    }
    private bool HasZeroWater(EntityScript entity)
    {
        return entity.GetState().GetWater() <= 0;
    }
    #endregion
    #region MANGER
    private bool isHungry(EntityScript entity)
    {
        return entity.GetState().GetFood() < 50;
    }
    private bool CanGoToFood(EntityScript entity)
    {
        for ( var i = 0 ; i < entity.GetContext().Food.Count ; ++i )
        {
            if ( Vector3.Distance(entity.GetContext().Food[i].Position,
                entity.GetTransform().position) < 5f )
            {
                entity.GetState().SetTargetedFood(entity.GetContext().Food[i]);
                return true;
            }
        }
        return false;
    }
    private bool CanEat(EntityScript entity)
    {
        if ( Mathf.Abs(entity.GetTransform().position.x - entity.GetState().GetTargetedFood().Position.x) <= 2.0f
            && Mathf.Abs(entity.GetTransform().position.z - entity.GetState().GetTargetedFood().Position.z) <= 2.0f )
        {
            if ( !GameData.Ressources.ContainsKey(entity.GetState().GetTargetedFood().Name) )
            {
                entity.GetContext().RemoveFoodByPosition(entity.GetState().GetTargetedFood().Position);
                return false;
            }
            return true;
        }
        else
            return false;
    }
    #endregion
    #region BOIRE
    private bool isTthirsty(EntityScript entity)
    {
        return entity.GetState().GetWater() < 50;
    }
    private bool CanGoToWater(EntityScript entity)
    {
        for ( var i = 0 ; i < entity.GetContext().Water.Count ; ++i )
        {
            if ( Vector3.Distance(entity.GetContext().Water[i].Position,
                entity.GetTransform().position) < 5f )
            {
                entity.GetState().SetTargetedWater(entity.GetContext().Water[i]);
                return true;
            }

        }
        return false;
    }
    private bool CanDrink(EntityScript entity)
    {
        if ( Mathf.Abs(entity.GetTransform().position.x - entity.GetState().GetTargetedWater().Position.x) <= 2.0f
            && Mathf.Abs(entity.GetTransform().position.z - entity.GetState().GetTargetedWater().Position.z) <= 2.0f )
        {
            if ( !GameData.Ressources.ContainsKey(entity.GetState().GetTargetedWater().Name) )
            {
                entity.GetContext().RemoveWaterByPosition(entity.GetState().GetTargetedWater().Position);
                return false;
            }
            return true;
        }
        else
            return false;
    }
    #endregion
    #region BOUGER
    private bool CanMove(EntityScript entity)
    {
        return Mathf.Abs(entity.GetMovement().GetPosition().x - entity.GetMovement().GetTargetPosition().x) <= 0.1f
               && Mathf.Abs(entity.GetMovement().GetPosition().z - entity.GetMovement().GetTargetPosition().z) <= 0.1f;
    }

    #endregion
    #endregion
    #region ACTION_DEFINITION
    #region MOURIR
    private void Die(EntityScript entity)
    {
        EntityManagerScript.AddToQueueAndMove(entity);
        //Debug.LogError("DIE !!!");
        return;
    }
    #endregion
    #region MANGER
    private void SearchForEat(EntityScript entity)
    {
        return;
        //imaginer une animation ou il cherche a manger ou alors il se tient le ventre ...
    }
    private void GoToEat(EntityScript entity)
    {
        //Debug.Log("Go To eat");
        entity.GetMovement().SetTargetPosition(entity.GetState().GetTargetedFood().Position);
    }
    private void Eat(EntityScript entity)
    {
        //Debug.Log("Eat !!");
        var f = Random.Range(10, 50);
        GameData.Ressources[entity.GetState().GetTargetedFood().Name].Take(f); //TODO : a voir le nombre de food
        entity.GetState().SetFood(entity.GetState().GetFood() + f);
    }
    #endregion
    #region BOIRE
    private void SearchForWater(EntityScript entity)
    {
        return;
        //imaginer une animation ou il cherche a manger ou alors il se tient le ventre ...
    }
    private void GoToWater(EntityScript entity)
    {
        //Debug.Log("Go To drink");
        entity.GetMovement().SetTargetPosition(entity.GetState().GetTargetedWater().Position);
    }
    private void Drink(EntityScript entity)
    {
        //Debug.Log("Eat !!");
        var f = Random.Range(10, 50);
        GameData.Ressources[entity.GetState().GetTargetedWater().Name].Take(f);  //TODO : a voir le nombre de food
        entity.GetState().SetWater(entity.GetState().GetWater() + f);
    }
    #endregion
    #region BOUGER
    private void Move(EntityScript entity)
    {
        //Debug.Log("Entity " + entity + " is moving");
        var pos = entity.GetMovement().GetPosition();
        var newPos = new Vector3(pos.x + Random.Range(-10f, 10f), pos.y, pos.z + Random.Range(-10f, 10f));
        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.MapSize.x);
        newPos.z = Mathf.Clamp(newPos.z, 0, GameData.MapSize.z);
        entity.GetMovement().SetTargetPosition(newPos);
    }
    #endregion
    #endregion
}
