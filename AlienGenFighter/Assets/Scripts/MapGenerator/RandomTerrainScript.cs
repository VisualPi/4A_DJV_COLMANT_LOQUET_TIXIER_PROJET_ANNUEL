using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic; // used for Sum of array

public class RandomTerrainScript : MonoBehaviour
{

    [SerializeField]
    Terrain terrain;
    
    [SerializeField]
    public Vector3 sizeMap;

    static Vector3 _gameArea;
    // La composante Y définit la hauteur maximale du terrain

    string seed;
    TerrainData terrainData;

    float[,] heightArray;

    float randomiseur;
    Vector2 distance;
    float distanceF;
    
    List<Vector3> hillsPoint = new List<Vector3>();
    
    Vector2 actualPoint;

	[SerializeField] private GameObject _civilisation;
    [SerializeField] private GameObject _food;
    [SerializeField] private GameObject _water;
    enum SizeMapZE
    {
        A,B,C
    };

    
/*
    void InitPropertyForTerrainData(int length, int height, int width)
    {
        sizeMap = new Vector3(length, height, width);
        
    }
    */
    void InitialiseTerrainParameter()
    {
       
        _gameArea = sizeMap;
        heightArray = new float[(int)sizeMap.z, (int)sizeMap.x];
        terrainData = terrain.terrainData;
        terrainData.heightmapResolution = (int)_gameArea.x;
        terrainData.SetHeights(0, 0, ArrayMapCreator());
        terrainData.size = _gameArea;
		//CreateCivilisations();
        //PutFoodAndWater(50);
    }


    void CreateHill()
    {
        int seedHill = 3;
        float RandomHeight = 0.0f;

        for (int i = 0; i < seedHill; ++i)
        {
            RandomHeight = Random.Range(0.5f, 0.8f);
            int x = Random.Range(0, (int)sizeMap.x) ;
            int z = Random.Range(0, (int)sizeMap.z) ;

              
                heightArray[z, x] = RandomHeight;
                hillsPoint.Add(new Vector3(x, RandomHeight,z));
               // Debug.Log("X: " + x + ", Z: " + z + ", Y:" + RandomHeight);
        }
    }


    void CreateVolcano(Vector2 sizeMapD, float range)
    {
       /* if (distance.magnitude < range)
        {
            if (heightArray[(int)sizeMapD.y, (int)sizeMapD.x] != 0.3f)
            {
                heightArray[(int)sizeMapD.y, (int)sizeMapD.x] += 0.3f - ((0.3f / range) * distance.magnitude);
            }
        }*/

        if (distance.magnitude < 10.0f)
        {
            heightArray[(int)sizeMapD.y, (int)sizeMapD.x] = 0.2f;
        }
    }


    Vector2 getDistanceBetweenTwoPoint(Vector2 hillPoint, Vector2 actualPoint)
    {
        //PAS LA VRAI DISTANCE A VOIR AVEC DES INT
        Vector2 tempDistance;
        tempDistance = actualPoint - hillPoint;
        return tempDistance;
    }

    private float SampleGaussian(float x, float mu, float sigma)
    {
        float d = (x - mu);
        return Mathf.Exp(-d * d / (sigma * sigma));
    }

    float[,] ArrayMapCreator()
    {

        CreateHill();




        float range = 0.0f;
       

        Debug.Log(Mathf.PerlinNoise(10.0f, 10.0f));
        
        foreach (Vector3 hillPoint in hillsPoint)
        {
            range = Random.Range(60.0f, 150.0f);
       
            for (int y = 0; y < sizeMap.z; y++)
            {
                for (int x = 0; x < sizeMap.x; x++)
                {
                    heightArray[y, x] = 0.0f;
                    heightArray[y, x] = Mathf.PerlinNoise(Time.time * 1.0F, 0.0F);
                    distanceF = Vector3.Distance(hillPoint, new Vector3(x,0,y));
                  /*
                   if (Vector3.Distance(new Vector3(x,0,y), new Vector3(sizeMap.x/2 ,0,sizeMap.z/2))<10)
                   {
                       heightArray[y, x] += 0.0f;
                   }
                   else
                   {
                       heightArray[y, x] += 0.01f;
                   }



                    
                                       if (distanceF < range)
                                       {
                                           if (heightArray[y, x] != hillPoint.y)
                                           {
                                               heightArray[y, x] = (hillPoint.y - ((hillPoint.y / range) * distanceF)) + 0.01f;
                                           }
                                       }
                                       else
                                       {
                                           if (heightArray[y, x] == 0.0f)
                                           {
                                               heightArray[y, x] = 0.01f;
                                           }
                                           else
                                           {
                                               heightArray[y, x] += 0.01f;
                                           }
                                       }  */
                }               
            }
        }

/*
        for (int g = 0; g < sizeMap.z; g++)
        {
            for (int o = 0; o < sizeMap.x; o++)
            {
                heightArray[g, o] += Random.Range(0.0f, 0.001f);
            }
        }
*/

        return heightArray;
    }
    
    void Start()
    {
        InitialiseTerrainParameter();
        Debug.Log(terrainData.size);
    }
	public void CreateCivilisations()
	{
		RaycastHit hit;
		Ray ray = new Ray(new Vector3(15f, 255f, sizeMap.z / 2), Vector3.down);
		if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
		{
			Instantiate(_civilisation, new Vector3(hit.point.x,
													hit.point.y + _civilisation.transform.localScale.y,
													hit.point.z), Quaternion.identity);
		}

		ray = new Ray(new Vector3(sizeMap.x - 15f, 255f, sizeMap.z / 2), Vector3.down);
		if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
		{
			Instantiate(_civilisation, new Vector3(hit.point.x,
													hit.point.y + _civilisation.transform.localScale.y,
													hit.point.z), Quaternion.identity);
		}

		ray = new Ray(new Vector3(sizeMap.x / 2, 255f, sizeMap.z - 15f), Vector3.down);
		if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
		{
			Instantiate(_civilisation, new Vector3(hit.point.x,
													hit.point.y + _civilisation.transform.localScale.y,
													hit.point.z), Quaternion.identity);
		}

		ray = new Ray(new Vector3(sizeMap.x / 2, 255f, 15f), Vector3.down);
		if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
		{
			Instantiate(_civilisation, new Vector3(hit.point.x,
													hit.point.y + _civilisation.transform.localScale.y,
													hit.point.z), Quaternion.identity);
		}
	}
    public void PutFoodAndWater(int nb)
    {
        RaycastHit hit;
        Ray ray;
        for (var i = 0; i < nb; i++)
        {
            ray = new Ray(new Vector3(Random.Range(0, sizeMap.x), 255f, Random.Range(0, sizeMap.z)), Vector3.down);
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
            {
                Instantiate(_food, new Vector3(hit.point.x,
                                                        hit.point.y + _civilisation.transform.localScale.y,
                                                        hit.point.z), Quaternion.identity);
            }
        }
        for (var i = 0; i < nb; i++)
        {
            ray = new Ray(new Vector3(Random.Range(0, sizeMap.x), 255f, Random.Range(0, sizeMap.z)), Vector3.down);
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
            {
                Instantiate(_water, new Vector3(hit.point.x,
                                                        hit.point.y + _civilisation.transform.localScale.y,
                                                        hit.point.z), Quaternion.identity);
            }
        }

    }
}
	
	
