using System.Collections.Generic;

namespace Assets.Scripts.Context {
    public class EntityContext : Ressources {
        public int NbEntity {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public int Memorie {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public List<EntityScript> Entities {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public List<OtherTargetable> SourceWatter {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public List<OtherTargetable> SourceFood {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public override void AddWater(OtherTargetable water)
        {
            throw new System.NotImplementedException();
        }

        public override void AddFood(OtherTargetable water)
        {
            throw new System.NotImplementedException();
        }
    }
}
