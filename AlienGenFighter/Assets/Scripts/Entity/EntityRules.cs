﻿using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.GUI;
using Assets.Scripts.Misc;
using Random = UnityEngine.Random;

public class EntityRules
{
    public List<RulesGroup>[] Rules { get; set; }

    public EntityRules()
    {
        Rules = new[] {
            new List<RulesGroup>(),
            new List<RulesGroup>(),
            new List<RulesGroup>()
        };

        // mourir - priority 0
        var rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(HasZeroFood), new RuleAction(Die)));
        Rules[0].Add(rg);

        // mourrir - priority 0
        rg = new RulesGroup(); // TODO JO FROM AMAU : Deux groupe pour mourir ?
        rg.RuleList.Add(new Rules(new RuleCondition(HasZeroWater), new RuleAction(Die)));
        Rules[0].Add(rg);


        // manger - priority 0
        rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(isHungry), new RuleAction(SearchForEat)));
        rg.RuleList.Add(new Rules(new RuleCondition(CanGoToFood), new RuleAction(GoToEat)));
        rg.RuleList.Add(new Rules(new RuleCondition(CanEat), new RuleAction(Eat)));
        Rules[0].Add(rg);

        // boire - priority 0
        rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(isTthirsty), new RuleAction(SearchForWater)));
        rg.RuleList.Add(new Rules(new RuleCondition(CanGoToWater), new RuleAction(GoToWater)));
        rg.RuleList.Add(new Rules(new RuleCondition(CanDrink), new RuleAction(Drink)));
        Rules[0].Add(rg);


        //groupe - priority 1
        rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(IsNotInAGroup), new RuleAction(Iddle)));
        rg.RuleList.Add(new Rules(new RuleCondition(WantToBeInAGroup), new RuleAction(SearchingForGroup)));
        Rules[1].Add(rg);

        //deleteGroup - Prority 1
        rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(IsInAGroup), new RuleAction(Iddle)));
        rg.RuleList.Add(new Rules(new RuleCondition(IsNotEnoughMember), new RuleAction(DeleteGroup)));
        Rules[1].Add(rg);


        //avancer aleatoirement - priority 2
        rg = new RulesGroup();
        rg.RuleList.Add(new Rules(new RuleCondition(CanMove), new RuleAction(Move)));
        Rules[2].Add(rg);
    }

    #region CONDITION_DEFINITION
    #region MOURIR
    private bool HasZeroFood(EntityScript entity)
    {
        return entity.State.Food <= 0;
    }
    private bool HasZeroWater(EntityScript entity)
    {
        return entity.State.Water <= 0;
    }
    #endregion
    #region GROUPE
    #region GROUPE_CREATION
    private bool IsNotInAGroup(EntityScript entity)
    {
        return !entity.IsInGroup && Time.time > Utils.MinuteToSecond(0.5f) * GameData.GameSpeed;
    }
    private bool WantToBeInAGroup(EntityScript entity)
    {
        if ( entity.DNA.GetGeneAt(ECharateristic.Sociability) < 70 )
            return false;
        var coef = Random.Range(0, 100);
        return coef > entity.DNA.GetGeneAt(ECharateristic.Sociability);
    }
    #endregion
    #region GROUPE_DELETE
    private bool IsInAGroup(EntityScript entity)
    {
        return entity.IsInGroup;
    }
    private bool IsNotEnoughMember(EntityScript entity)//cette fonction va retourner true si le group contient qu'une seule personne apres 5 minutes d'existance
    {
        if ( entity.GroupContext.Entities.Count > 1 )
            return false; //retourne faux si le groupe n'est pas vide
        return entity.GroupContext.Group.TimeEllapsed > Utils.MinuteToSecond(5);
    }

    #endregion
    #endregion
    #region MANGER
    private bool isHungry(EntityScript entity)
    {
        return entity.State.Food < 50;
    }
    private bool CanGoToFood(EntityScript entity)
    {
        for ( var i = 0 ; i < entity.Context.Food.Count ; ++i )
        {
            if ( Vector3.Distance(entity.Context.Food[i].Position,
                entity.Transform.position) < 5f )
            {
                entity.State.TargetedFood = entity.Context.Food[i];
                return true;
            }
        }
        if ( entity.GroupContext != null )
        {
            for ( var i = 0 ; i < entity.GroupContext.Food.Count ; ++i )
            {
                if ( Vector3.Distance(entity.GroupContext.Food[i].Position,
                    entity.Transform.position) < 5f )
                {
                    entity.State.TargetedFood = entity.GroupContext.Food[i];
                    return true;
                }
            }
        }

        return false;
    }
    private bool CanEat(EntityScript entity)
    {
        if ( Mathf.Abs(entity.Transform.position.x - entity.State.TargetedFood.Position.x) <= 2.0f
            && Mathf.Abs(entity.Transform.position.z - entity.State.TargetedFood.Position.z) <= 2.0f )
        {
            if (GameData.Ressources.ContainsKey(entity.State.TargetedFood.Name))
                return true;
            entity.Context.RemoveFoodByPosition(entity.State.TargetedFood.Position);
        }
        return false;
    }
    #endregion
    #region BOIRE
    private bool isTthirsty(EntityScript entity)
    {
        return entity.State.Water < 50;
    }
    private bool CanGoToWater(EntityScript entity)
    {
        for ( var i = 0 ; i < entity.Context.Water.Count ; ++i )
        {
            if ( Vector3.Distance(entity.Context.Water[i].Position,
                entity.Transform.position) < 5f )
            {
                entity.State.TargetedWater = entity.Context.Water[i];
                return true;
            }

        }
        if ( entity.GroupContext != null )
        {
            for ( var i = 0 ; i < entity.GroupContext.Water.Count ; ++i )
            {
                if ( Vector3.Distance(entity.GroupContext.Water[i].Position,
                    entity.Transform.position) < 5f )
                {
                    entity.State.TargetedWater = entity.GroupContext.Water[i];
                    return true;
                }
            }
        }
        return false;
    }
    private bool CanDrink(EntityScript entity)
    {
        if ( Mathf.Abs(entity.Transform.position.x - entity.State.TargetedWater.Position.x) <= 2.0f
            && Mathf.Abs(entity.Transform.position.z - entity.State.TargetedWater.Position.z) <= 2.0f )
        {
            if (GameData.Ressources.ContainsKey(entity.State.TargetedWater.Name))
                return true;

            entity.Context.RemoveWaterByPosition(entity.State.TargetedWater.Position);
        }
        return false;
    }
    #endregion
    #region BOUGER
    private bool CanMove(EntityScript entity)
    {
        return Mathf.Abs(entity.Movement.Position.x - entity.Movement.GetTargetPosition().x) <= 0.1f
               && Mathf.Abs(entity.Movement.Position.z - entity.Movement.GetTargetPosition().z) <= 0.1f;
    }

    #endregion
    #endregion
    //--------------------------------------------------------------------------------//
    #region ACTION_DEFINITION
    #region MOURIR
    private void Die(EntityScript entity)
    {
        EntityManagerScript.AddToQueueAndMove(entity);
        Log.Error.Entity("DIE !!!");
    }
    #endregion
    #region GROUPE
    #region GROUPE_CREATION
    private void Iddle(EntityScript entity) //TODO: a voir
    {
        return;
    }
    private void SearchingForGroup(EntityScript entity)//Pour l'instant ce sera creer un groupe
    {
        //entity.GroupContext = new GroupContext();
        entity.GroupContext = EntityManagerScript.GetGroupFromQueue().GroupContext;
        entity.GroupContext.AddEntity(entity);
        entity.GroupContext.Group.Transform.position = entity.Transform.position;
        entity.GroupContext.Group.Transform.parent = entity.Transform;
        entity.GroupContext.Group.StartTimer();
        GameEventMessage.AddEventMessage(EIconEventMessage.Group,entity.name + " is creating goup", "creation de groupe", DateTime.Now );

    }
    #endregion
    #region GROUPE_DELETE
    private void DeleteGroup(EntityScript entity)//Pour l'instant ce sera creer un groupe
    {
        EntityManagerScript.AddGroupToQueue(entity.GroupContext.Group);
        entity.IsInGroup    = false;
        entity.GroupContext = null;
    }
    #endregion
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
        entity.Movement.SetTargetPosition(entity.State.TargetedFood.Position);
    }
    private void Eat(EntityScript entity)
    {
        Log.Debug.Entity("Eat !!");
        var f = Random.Range(10, 50);//TODO : a voir le nombre de food
        entity.State.Food = entity.State.Food + GameData.Ressources[entity.State.TargetedFood.Name].Take(f);
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
        Log.Debug.Entity("Go To Drink");
        entity.Movement.SetTargetPosition(entity.State.TargetedWater.Position);
    }
    private void Drink(EntityScript entity)
    {
        Log.Debug.Entity("Drink !!");
        var w = Random.Range(10, 50);//TODO : a voir le nombre de Water
        entity.State.Water = entity.State.Water + GameData.Ressources[entity.State.TargetedWater.Name].Take(w);
    }
    #endregion
    #region BOUGER
    private void Move(EntityScript entity)
    {
        Log.Debug.Entity("{0} is moving.", entity);
        var newPos = new Vector3(
            entity.Movement.Position.x + Random.Range(-10f, 10f),
            entity.Movement.Position.y,
            entity.Movement.Position.z + Random.Range(-10f, 10f)
            );
        if(entity.IsInGroup && Random.Range(0,100) < entity.DNA.GetGeneAt(ECharateristic.Adventurous))
        {
            newPos.x = Mathf.Clamp(newPos.x, entity.GroupContext.Group.Collider.bounds.min.x, entity.GroupContext.Group.Collider.bounds.max.x);
            newPos.z = Mathf.Clamp(newPos.z, entity.GroupContext.Group.Collider.bounds.min.z, entity.GroupContext.Group.Collider.bounds.max.z);
            //TODO : gerer le fait que l'entite peut sortir des bounds, techniquement ça se joue avec l'aventure ..
        }
        newPos.x = Mathf.Clamp(newPos.x, 0, GameData.MapSize.x);
        newPos.z = Mathf.Clamp(newPos.z, 0, GameData.MapSize.z);
        entity.Movement.SetTargetPosition(newPos);
    }
    #endregion
    #endregion
}
