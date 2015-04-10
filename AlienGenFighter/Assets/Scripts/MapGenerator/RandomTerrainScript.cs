using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic; // used for Sum of array

public class RandomTerrainScript : MonoBehaviour
{
    [SerializeField]
    Terrain terrain;
    
    [SerializeField]
    Vector3 sizeMap;
    // La composante Y définit la hauteur maximale du terrain

    string seed;
    TerrainData terrainData;

    float[,] heightArray;

    float randomiseur;
    Vector2 distance;
    float distanceF;
    
    List<Vector3> hillsPoint = new List<Vector3>();
    
    Vector2 actualPoint;

    
/*
    void InitPropertyForTerrainData(int length, int height, int width)
    {
        sizeMap = new Vector3(length, height, width);
        
    }
    */
    void InitialiseTerrainParameter()
    {
        heightArray = new float[(int)sizeMap.z, (int)sizeMap.x];
        terrainData = terrain.terrainData;
        terrainData.size = sizeMap;
        terrainData.SetHeights(0, 0, ArrayMapCreator());
    }


    void CreateHill()
    {
        int seedHill = 3;
        float RandomHeight = 0.0f;


        for (int i = 0; i < seedHill; ++i)
        {
            RandomHeight = Random.Range(0.3f, 0.5f);
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


    float[,] ArrayMapCreator()
    {

        CreateHill();




        float range = 0.0f;
       // int randomV = Random.Range(0, 2);
      //  int op = 0;
       
        
        foreach (Vector3 hillPoint in hillsPoint)
        {
            range = Random.Range(60.0f, 150.0f);
       
            for (int y = 0; y < sizeMap.z; y++)
            {
                for (int x = 0; x < sizeMap.x; x++)
                {

              
                    
                   distanceF = Vector3.Distance(hillPoint, new Vector3(x,0,y));
                   // distance = getDistanceBetweenTwoPoint(hillPoint, new Vector2(x,y));
                    //Debug.Log("(" + x + "," + y + ") = " + distance.magnitude.ToString());

                   if (distanceF < range)
                   {
                       if (heightArray[y, x] != hillPoint.y)
                       {
                           heightArray[y, x] += (hillPoint.y - ((hillPoint.y / range) * distanceF));
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
                   }        
                }                
            }
        }


        for (int g = 0; g < sizeMap.z; g++)
        {
            for (int o = 0; o < sizeMap.x; o++)
            {
                heightArray[g, o] += Random.Range(0.0f, 0.001f);
            }
        }


        return heightArray;
    }
    
    void Start()
    {
        InitialiseTerrainParameter();
        
      


        
    }
}
	
	
