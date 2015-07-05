using System.Collections.Generic;

namespace Assets.Scripts.Context
{
    public class EntityContext : Ressources
    {
        public EntityContext()
        {
            //Entities = new List<EntityScript>();
        }
        //public int NbEntity { get; set; }

        public int Memory { get; set; }

        //private List<EntityScript> Entities { get; set; }

        public override void AddWater(EdibleInformations water)
        {
            if ( Water.Count == Memory )
            {
                var waterTemp = new List<EdibleInformations>();
                for ( var i = 1 ; i < Water.Count - 1 ; ++i )
                    waterTemp.Add(Water[i]);
                waterTemp.Add(water);
                Water = waterTemp;
            }
            else
            {
                //TODO : tester si la ressource existe pas déja dans le groupe mais dans ce cas : reference croisé
                Water.Add(water);
            }

        }

        public override void AddFood(EdibleInformations food)
        {
            if ( Food.Count == Memory )
            {
                var foodTemp = new List<EdibleInformations>();
                for ( var i = 1 ; i < Food.Count - 1 ; ++i )
                    foodTemp.Add(Food[i]);
                foodTemp.Add(food);
                Food = foodTemp;
            }
            else
            {
                //TODO : tester si la ressource existe pas déja dans le groupe mais dans ce cas : reference croisé
                Food.Add(food);
            }
        }
    }
}
