using UnityEngine;

public class EntityCollisionScript : MonoBehaviour
{
    //[SerializeField]
    //private Collider _collider;
    [SerializeField] public string EntName;
    [SerializeField] private string _lastCol = "";

    public void OnTriggerEnter(Collider col)
    {
        if ( col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol) )
        {
            if ( !_lastCol.Equals("") )
                GameData.SquareMaps[_lastCol].Context.Entities.Remove(GameData.Entities[EntName]);
            GameData.SquareMaps[col.name].Context.Entities.Add(GameData.Entities[EntName]);
            _lastCol = col.name;
        }
        if ( col.tag.Equals("Food") )
        {
            GameData.Entities[EntName].Context.AddFood(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
        if ( col.tag.Equals("Water") )
        {
            GameData.Entities[EntName].Context.AddWater(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
    }

    public void SetLastCol(string s)
    {
        _lastCol = s;
    }
}
