using System.Collections.Generic;

namespace Assets.Scripts.Context {
    public abstract class Ressources {
        public List<OtherTargetable> Water { get; protected set; }
        public List<OtherTargetable> Food { get; protected set; }

        public abstract void AddWater(OtherTargetable water);
        public abstract void AddFood(OtherTargetable water);
    }
}
