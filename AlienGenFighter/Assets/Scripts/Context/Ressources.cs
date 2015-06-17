using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Context
{
    public abstract class Ressources
    {
        protected Ressources()
        {
            Water = new List<EdibleInformations>();
            Food = new List<EdibleInformations>();
        }
        public List<EdibleInformations> Water { get; protected set; }
        public List<EdibleInformations> Food { get; protected set; }

        public abstract void AddWater(EdibleInformations water);
        public abstract void AddFood(EdibleInformations food);

        public void RemoveWaterByPosition(Vector3 pos)
        {
            for (var i = 0; i < Water.Count; ++i)
            {
                if (Water[i].Position == pos)
                {
                    GameData.Ressources.Remove(Water[i].Name);
                    Water.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveFoodByPosition(Vector3 pos)
        {
            for (var i = 0; i < Food.Count; ++i)
            {
                if (Food[i].Position == pos)
                {
                    GameData.Ressources.Remove(Food[i].Name);
                    Food.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
