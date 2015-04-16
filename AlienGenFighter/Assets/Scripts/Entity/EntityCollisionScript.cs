using UnityEngine;

public class EntityCollisionScript : MonoBehaviour
{
	//public Collider _collider;
	[SerializeField]
	public EntityScript _entity; //TODO : reference croisée !!! T_T
	private string _lastCol = "";

	public void OnTriggerEnter(Collider col)
	{
		if(col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol))
		{
			if(!_lastCol.Equals(""))
				MapManagerScript._SquareMaps[_lastCol].GetContext().GetEntityOnCurrentSquare().Remove(_entity);
			MapManagerScript._SquareMaps[col.name].GetContext().GetEntityOnCurrentSquare().Add(_entity);
			_lastCol = col.name;
		}
	}

	public void SetLastCol(string s)
	{
		_lastCol = s;
	}
}
