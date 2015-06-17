using System.Collections.Generic;

namespace Assets.Scripts.Context
{
    public class EntityContext : Ressources
    {
        public EntityContext()
        {
            Entities = new List<EntityScript>();
        }
        public int NbEntity { get; set; }

        public int Memory { get; set; }

        public List<EntityScript> Entities { get; set; }

        public override void AddWater(EdibleInformations water)
        {
            Water.Add(water);
        }

        public override void AddFood(EdibleInformations food)
        {
            Food.Add(food);
        }
    }
}
