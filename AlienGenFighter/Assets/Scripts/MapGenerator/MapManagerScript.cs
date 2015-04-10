using UnityEngine;
using System.Collections.Generic;

public class MapManagerScript : MonoBehaviour
{
	public static Dictionary<string, SquareMapScript> _SquareMaps;

	void Start()
	{
		_SquareMaps = new Dictionary<string, SquareMapScript>();
	}
	void Update()
	{

	}

	//TODO : Dans un "GameManagerScript" alimenter les listes des squaremapScript avec chaque unités instancié
}
