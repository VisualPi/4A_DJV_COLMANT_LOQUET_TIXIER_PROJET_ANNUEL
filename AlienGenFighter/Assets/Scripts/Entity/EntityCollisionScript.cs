using UnityEngine;

public class EntityCollisionScript : MonoBehaviour
{
	//public Collider _collider;
	[SerializeField] private EntityScript _entity; //TODO : reference croisée !!! T_T
	private string _lastCol = "";

	public void OnTriggerEnter(Collider col)
	{
		if(col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol))
		{
			if(!_lastCol.Equals(""))
                GameData.SquareMaps[_lastCol].GetContext().Entities.Remove(_entity);
            GameData.SquareMaps[col.name].GetContext().Entities.Add(_entity);
			_lastCol = col.name;
		}
        if (col.tag.Equals("Food"))
	    {
	        _entity.GetContext().AddFood(((EdibleScript)GameData.Ressources[col.name]).GetInformations());
	    }
        if (col.tag.Equals("Water"))
        {
            _entity.GetContext().AddWater(((EdibleScript)GameData.Ressources[col.name]).GetInformations());
        }
    }

	public void SetLastCol(string s)
	{
		_lastCol = s;
	}
}
