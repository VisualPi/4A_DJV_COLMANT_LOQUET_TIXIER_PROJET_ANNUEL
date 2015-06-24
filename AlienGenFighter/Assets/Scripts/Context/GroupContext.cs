using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Assets.Scripts.Context
{
    public class GroupContext : Ressources
    {
        public GroupContext()
        {
            Entities = new List<EntityScript>();
        }
        public int NbEntity { get; set; }

        public EntityScript Leader { get; set; }

        public List<EntityScript> Entities { get; set; }

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
            Entities.Add(entity);
            for (var i = 0; i < entity.GetContext().Food.Count; ++i)
            {
                if(!Food.Contains(entity.GetContext().Food[i]))
                    Food.Add(entity.GetContext().Food[i]);
            }
            for ( var i = 0 ; i < entity.GetContext().Water.Count ; ++i )
            {
                if ( !Water.Contains(entity.GetContext().Water[i]) )
                    Water.Add(entity.GetContext().Water[i]);
            }
        }

    }
}
