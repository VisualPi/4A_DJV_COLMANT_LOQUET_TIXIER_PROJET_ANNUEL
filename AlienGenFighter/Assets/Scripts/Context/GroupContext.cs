﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Group;

namespace Assets.Scripts.Context
{
    public class GroupContext : Ressources
    {
        public GroupScript Group { get; set; }
        public int NbEntity { get; set; }

        public EntityScript Leader { get; set; }

        public List<EntityScript> Entities { get; set; }

        public GroupContext(GroupScript group=null)
        {
            Entities = new List<EntityScript>();
            Leader = null;
            Group = group;
        }
        public GroupContext(GroupContext group)
        {
            Entities = new List<EntityScript>();
            for ( var i = 0 ; i < group.Entities.Count ; ++i )
            {
                Entities.Add(group.Entities[i]);
            }
            Leader = Leader; //Pas une copie
            Water = new List<EdibleInformations>();
            for ( var i = 0 ; i < group.Water.Count ; ++i )
            {
                Water.Add(group.Water[i]);
            }
            Food = new List<EdibleInformations>();
            for ( var i = 0 ; i < group.Food.Count ; ++i )
            {
                Food.Add(group.Food[i]);
            }
            Group = group.Group;
        }

        public override void AddWater(EdibleInformations water)
        {
            Water.Add(water);
        }

        public override void AddFood(EdibleInformations food)
        {
            Food.Add(food);
        }

        public void AddEntity(EntityScript entity)
        {
            entity.GroupContext = this;//redondant pour le leader de base
            entity._isInGroup = true;
            Entities.Add(entity);
            for ( var i = 0 ; i < entity.GetContext().Food.Count ; ++i )
            {
                if ( !Food.Contains(entity.GetContext().Food[i]) )
                    Food.Add(entity.GetContext().Food[i]);
            }
            for ( var i = 0 ; i < entity.GetContext().Water.Count ; ++i )
            {
                if ( !Water.Contains(entity.GetContext().Water[i]) )
                    Water.Add(entity.GetContext().Water[i]);
            }
            if (Leader == null || ( entity.GetDNA().GetGeneAt(ECharateristic.Authority) >
                                    Leader.GetDNA().GetGeneAt(ECharateristic.Authority) ) )
                ChangeLeader(entity);

            if (Leader != null) Debug.Log("leader is : " + Leader.name);
            else Debug.LogError("LEADER IS NOT SUPPOSED TO BE NULL");
            Group.Collider.transform.localScale += new Vector3(.5f, .5f, .5f);
        }

        private void ChangeLeader(EntityScript entity)
        {
            Leader = entity;
            Group.Transform.parent = entity.GetTransform();
        }
    }
}
