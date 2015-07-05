using UnityEngine;

public class EntityCollisionScript : MonoBehaviour
{
    //public Collider _collider;
    [SerializeField]
    private EntityScript _entity; //TODO : reference croisée !!! T_T
    private string _lastCol = "";

    public void OnTriggerEnter(Collider col)
    {
        if ( col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol) )
        {
            if ( !_lastCol.Equals("") )
                GameData.SquareMaps[_lastCol].Context.Entities.Remove(_entity);
            GameData.SquareMaps[col.name].Context.Entities.Add(_entity);
            _lastCol = col.name;
        }
        if ( col.tag.Equals("Food") )
        {
            _entity.Context.AddFood(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
        if ( col.tag.Equals("Water") )
        {
            _entity.Context.AddWater(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
    }

    public void SetLastCol(string s)
    {
        _lastCol = s;
    }
}
