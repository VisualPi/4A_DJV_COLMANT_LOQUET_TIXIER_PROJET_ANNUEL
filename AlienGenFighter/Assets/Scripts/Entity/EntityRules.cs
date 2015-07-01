using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Context;
using Assets.Scripts.Group;
using Assets.Scripts.Misc;
using Random = UnityEngine.Random;

public class EntityRules
{
    public List<RulesGroup>[] Rules { get; set; }

    public EntityRules()
    {
        Rules = new List<RulesGroup>[]
        {
            new List<RulesGroup>(),
            new List<RulesGroup>(),
            new List<RulesGroup>()
        };
        //mourir - pritity 0
        RulesGroup rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(HasZeroFood), new RuleAction(Die)));
        Rules[0].Add(rg);
        rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(HasZeroWater), new RuleAction(Die)));
        Rules[0].Add(rg);
        //groupe - priority 0
        rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(IsNotInAGroup), new RuleAction(Iddle)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(WantToBeInAGroup), new RuleAction(SearchingForGroup)));
        Rules[0].Add(rg);
        ////manger - priority 0
        rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(isHungry), new RuleAction(SearchForEat)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanGoToFood), new RuleAction(GoToEat)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanEat), new RuleAction(Eat)));
        Rules[0].Add(rg);
        //boire - priority 0
        rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(isTthirsty), new RuleAction(SearchForWater)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanGoToWater), new RuleAction(GoToWater)));
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanDrink), new RuleAction(Drink)));
        Rules[0].Add(rg);

        //avancer aleatoirement - priority 2
        rg = new RulesGroup();
        rg.GetRuleList().Add(new Rules(new RuleCondition(CanMove), new RuleAction(Move)));
        Rules[2].Add(rg);

    }

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
    #region GROUPE
    private bool IsNotInAGroup(EntityScript entity)
    {
        return !entity._isInGroup;
    }
    private bool WantToBeInAGroup(EntityScript entity)
    {
        if (entity.GetDNA().GetGeneAt(ECharateristic.Sociability) < 70)
            return false;
        var coef = Random.Range(0, 100);
        return coef > entity.GetDNA().GetGeneAt(ECharateristic.Sociability);
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
        if (entity.GroupContext != null)
        {
            for ( var i = 0 ; i < entity.GroupContext.Food.Count ; ++i )
            {
                if ( Vector3.Distance(entity.GroupContext.Food[i].Position,
                    entity.GetTransform().position) < 5f )
                {
                    entity.GetState().SetTargetedFood(entity.GroupContext.Food[i]);
                    return true;
                }
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
        if ( entity.GroupContext != null )
        {
            for ( var i = 0 ; i < entity.GroupContext.Water.Count ; ++i )
            {
                if ( Vector3.Distance(entity.GroupContext.Water[i].Position,
                    entity.GetTransform().position) < 5f )
                {
                    entity.GetState().SetTargetedWater(entity.GroupContext.Water[i]);
                    return true;
                }
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
        Log.Error.Entity("DIE !!!");
    }
    #endregion
    #region GROUPE
    private void Iddle(EntityScript entity) //TODO: a voir
    {
        return;
    }
    private void SearchingForGroup(EntityScript entity)//Pour l'instant ce sera creer un groupe
    {
        //entity.GroupContext = new GroupContext();
        var gr = EntityManagerScript.GetGroupFromQueue() as GroupScript;
        entity.GroupContext = gr.GroupContext;
        entity.GroupObject = gr.GameObject;
        entity.GroupContext.AddEntity(entity);
        gr.Transform.position = entity.GetTransform().position;
        gr.Transform.parent = entity.GetTransform();

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
        Log.Debug.Entity("Go To Eat");
        entity.GetMovement().SetTargetPosition(entity.GetState().GetTargetedFood().Position);
    }
    private void Eat(EntityScript entity) {
        Log.Debug.Entity("Eat !!");
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
    private void GoToWater(EntityScript entity) {
        Log.Debug.Entity("Go To Drink");
        entity.GetMovement().SetTargetPosition(entity.GetState().GetTargetedWater().Position);
    }
    private void Drink(EntityScript entity) {
        Log.Debug.Entity("Drink !!");
        var f = Random.Range(10, 50);
        GameData.Ressources[entity.GetState().GetTargetedWater().Name].Take(f);  //TODO : a voir le nombre de food
        entity.GetState().SetWater(entity.GetState().GetWater() + f);
    }
    #endregion
    #region BOUGER
    private void Move(EntityScript entity)
    {
        Log.Debug.Entity("{0} is moving.", entity);
        var pos = entity.GetMovement().GetPosition();
        var newPos = new Vector3(pos.x + Random.Range(-10f, 10f), pos.y, pos.z + Random.Range(-10f, 10f));
        if(entity._isInGroup && Random.Range(0,100) < entity.GetDNA().GetGeneAt(ECharateristic.Adventurous))
        {
            newPos.x = Mathf.Clamp(newPos.x, entity.GroupContext.Group.Collider.bounds.min.x, entity.GroupContext.Group.Collider.bounds.max.x);
            newPos.x = Mathf.Clamp(newPos.x, entity.GroupContext.Group.Collider.bounds.min.z, entity.GroupContext.Group.Collider.bounds.max.z);
            //TODO : gerer le fait que l'entite peut sortir des bounds, techniquement ça se joue avec l'aventure ..
        }
        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.MapSize.x);
        newPos.z = Mathf.Clamp(newPos.z, 0, GameData.MapSize.z);
        entity.GetMovement().SetTargetPosition(newPos);
    }
    #endregion
    #endregion
}
