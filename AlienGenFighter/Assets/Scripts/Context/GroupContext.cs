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

        public List<EntityScript> Entities { get; set; }

        public override void AddWater(EdibleInformations water)
        {
            throw new System.NotImplementedException();
        }

        public override void AddFood(EdibleInformations water)
        {
            throw new System.NotImplementedException();
        }
    }
}
