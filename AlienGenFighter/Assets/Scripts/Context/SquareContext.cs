using System.Collections.Generic;

namespace Assets.Scripts.Context
{
    public class SquareContext
    {
        public List<EdibleScript> Water = new List<EdibleScript>();
        public List<EdibleScript> Food = new List<EdibleScript>();
        public SquareContext()
        {
            Entities = new List<EntityScript>();
        }
        public int NbEntity { get; set; }

        public List<EntityScript> Entities { get; set; }
    }
}
