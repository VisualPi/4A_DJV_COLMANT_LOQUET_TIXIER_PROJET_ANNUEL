using UnityEngine;
using System.Collections;

public class TerrainGenerationScript : MonoBehaviour
{

	[SerializeField]
	GameObject TerrainII;

	float[,] heightArray;

	[SerializeField]
	int Map_Length;

	[SerializeField]
	int Map_Width;

	[SerializeField]
	int Map_Height;

	//Valeur pour faire varier les hauteur
	float strength = 7.0f;

	float randomiseur;


	// Use this for initialization
	void Start()
	{



		//Création d'un TerrainData qui va contenir toutes les infos nécessaies a la génération d'une map aléatoire
		TerrainData InfoTerrain = new TerrainData();

		//heightArray = new float[InfoTerrain.alphamapWidth, InfoTerrain.alphamapHeight];

		heightArray = new float[Map_Width, Map_Length];

		//PARAMETRE ORIGINAL DE LA MAP//
		// SIZE
		InfoTerrain.size = new Vector3(Map_Width, Map_Height, Map_Length);
		//
		InfoTerrain.heightmapResolution = 512 + 1;
		// LECTURE ONLY BITCH InfoTerrain.heightmapScale = new Vector3(0, 25, 0);
		//InfoTerrain.

		//PARAMETRE DE LA HEIGHT MAP//
		//RESOLUTION (puissance de 2)
		//InfoTerrain.alphamapResolution = 512 + 1;

		//QUELQUES OUTPUT
		Debug.Log("WIDTH: " + InfoTerrain.alphamapWidth);
		Debug.Log("LENGTH: " + InfoTerrain.alphamapHeight);

		Debug.Log("HEIGHT_MAP_DETAIL_HEIGHT :" + InfoTerrain.detailHeight);
		Debug.Log("X :" + InfoTerrain.size.x);
		Debug.Log("Y:" + InfoTerrain.size.y);
		Debug.Log("Z :" + InfoTerrain.size.z);
		Debug.Log("HEIGHT_MAP_WIDTH :" + InfoTerrain.heightmapWidth);
		Debug.Log("HEIGHT_MAP_HEIGHT :" + InfoTerrain.heightmapHeight);
		Debug.Log("HEIGHT_MAP_RESOLUTION :" + InfoTerrain.heightmapResolution);
		Debug.Log("HEIGHT_MAP_SCALE: " + InfoTerrain.heightmapScale);
		Debug.Log("SIZE :" + InfoTerrain.size);

		//InfoTerrain.detailHeight = 25;
		//Debug.Log("HEIGHT_MAP_HEIGHT :" + InfoTerrain.detailHeight.ToString());

		//TOUTE LA MAP A 0
		for(int y = 0 ; y < Map_Width ; y++)
		{
			for(int x = 0 ; x < Map_Length ; x++)
			{

				heightArray[x, y] = 0.0f;

			}
		}

		//Set le terrain en hauteur
		InfoTerrain.SetHeights(0, 0, heightArray);

		//Set ArrayHeight
		for(int y = 0 ; y < Map_Width ; y++)
		{
			for(int x = 0 ; x < Map_Length ; x++)
			{
				randomiseur = Random.Range(0.0f, strength);
				if(randomiseur > 3.0f)
				{
					heightArray[x, y] = 0.0f;
				}
				else
				{
					heightArray[x, y] = randomiseur;
				}

			}
		}





		//Création d'un Terrain, ajout automatique d'un Collider
		TerrainII = Terrain.CreateTerrainGameObject(InfoTerrain);
		//Terrain.CreateTerrainGameObject(InfoTerrain);
		Terrain Terrainp = TerrainII.GetComponent<Terrain>();
	}



	// Update is called once per frame
	void Update()
	{

	}
}
