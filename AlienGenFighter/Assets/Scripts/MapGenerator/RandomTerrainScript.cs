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


    public float hauteurCoinBasGauche = 0.0f;
    public float hauteurCoinBasDroite = 0.0f;
    public float hauteurCoinHautGauche = 0.0f;
    public float hauteurCoinHautDroite = 0.0f;

    public float roughness = 0.0f;


    static Vector3 _gameArea;
    // La composante Y définit la hauteur maximale du terrain

    string seed;
    TerrainData terrainData;

    float[,] heightArray;

    float randomiseur;
    Vector2 distance;
    //float distanceF;
    int heightmapSize;
    
    //List<Vector3> hillsPoint = new List<Vector3>();
    int cDmd;
    int cSqr;
    Vector2 actualPoint;


   
   

    [SerializeField] private GameObject _civilisation;
   
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

        GameData.MapSize = sizeMap;

        _gameArea = sizeMap;
        heightArray = new float[(int)sizeMap.z+1, (int)sizeMap.x+1];
        heightmapSize = (int)sizeMap.x + 1;
        terrainData = terrain.terrainData;
        terrainData.heightmapResolution = (int)_gameArea.x;
        Debug.Log(terrainData.heightmapResolution.ToString());
        terrainData.SetHeights(0, 0, TheLastRandomTerrain());
       
        terrainData.size = _gameArea;
        //SetupAlphaMap();
        
        
        
        
        
    }

        
    



    


    Vector2 getDistanceBetweenTwoPoint(Vector2 hillPoint, Vector2 actualPoint)
    {
        //PAS LA VRAI DISTANCE A VOIR AVEC DES INT
        Vector2 tempDistance;
        tempDistance = actualPoint - hillPoint;
        return tempDistance;
    }

    float offset(float height, float roughness)
    {
       float offset = Random.Range(0,1)*height*roughness;
      // Debug.Log("OFFSET :" + offset.ToString());
       return offset;
    }

  
     void Start()
    {
        InitialiseTerrainParameter();
        //Debug.Log(terrainData.size);
    }
    
    public void CreateCivilisations()
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(30f, 520f, 30f), Vector3.down);
        if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
        {
            Instantiate(_civilisation, new Vector3(hit.point.x,
                                                    hit.point.y + _civilisation.transform.localScale.y,
                                                    hit.point.z), Quaternion.identity);
        }

        ray = new Ray(new Vector3(sizeMap.x - 30f, 520f, sizeMap.z - 30f), Vector3.down);
        if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
        {
            Instantiate(_civilisation, new Vector3(hit.point.x,
                                                    hit.point.y + _civilisation.transform.localScale.y,
                                                    hit.point.z), Quaternion.identity);
        }

        ray = new Ray(new Vector3(sizeMap.x / 2 , 520f, sizeMap.z / 2), Vector3.down);
        if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
        {
            Instantiate(_civilisation, new Vector3(hit.point.x,
                                                    hit.point.y + _civilisation.transform.localScale.y,
                                                    hit.point.z), Quaternion.identity);
        }

        ray = new Ray(new Vector3(sizeMap.x - 30f, 520f, 30f), Vector3.down);
        if(Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
        {
            Instantiate(_civilisation, new Vector3(hit.point.x,
                                                    hit.point.y + _civilisation.transform.localScale.y,
                                                    hit.point.z), Quaternion.identity);
        }
    }


    


    


    public float[,] TheLastRandomTerrain()
    {
       
        int nbIteration=0;

        int xSqr = 0;
        int ySqr = 0;
        int demiEspace = 0;
        float max = 0.0f;
        float min = 1.0f;
        
        float scale = roughness * sizeMap.x;


        heightArray[heightmapSize-1, 0] = hauteurCoinHautGauche;


        heightArray[heightmapSize - 1, heightmapSize - 1] = hauteurCoinHautDroite;


        heightArray[0, 0] = hauteurCoinBasGauche;


        heightArray[0, heightmapSize - 1] = hauteurCoinBasDroite;



        int espace = heightmapSize-1;
        

       while (espace > 1)
       {
            
           demiEspace =  espace /2;
           // Debug.Log("Espace Actuel: " + espace);
           // Debug.Log("Taille map: " + heightmapSize);

            for (xSqr = demiEspace; xSqr < heightmapSize ; xSqr += espace)
            { 
                for (ySqr = demiEspace; ySqr < heightmapSize ; ySqr +=  espace)
                {

                
                   // nbIteration++;

                    heightArray[ySqr, xSqr] = StepSquare(xSqr, ySqr, demiEspace) + (Random.Range(-0.5f, 0.5f) * (espace / sizeMap.x));

/*
                    if(heightArray[ySqr,xSqr]>max)
                    {
                        max = heightArray[ySqr, xSqr];
                    }
                    if (heightArray[ySqr, xSqr] < min)
                    {
                        min = heightArray[ySqr, xSqr];
                    }
  */
                    //Debug.Log("Point, X:" + xSqr + ", Y: " + ySqr + "  value: " + heightArray[ySqr, xSqr]);
                    
                    
                }
            }

           
            for (xSqr = 0; xSqr < heightmapSize; xSqr += demiEspace)
            {
                //int unJStart = ((ySqr / espace) % 2 == 0) ? espace : 0;
               int yStart =  ((xSqr/demiEspace) % 2 == 0) ? demiEspace : 0; 
                for (ySqr = yStart; ySqr < heightmapSize; ySqr += espace)
                {
                    heightArray[ySqr, xSqr] = StepDiamond(xSqr, ySqr, demiEspace) + (Random.Range(-0.5f, 0.5f) * (espace / sizeMap.x)) ;
         /*          
                    if (heightArray[ySqr, xSqr] > max)
                    {
                        max = heightArray[ySqr, xSqr];
                    }
                    if (heightArray[ySqr, xSqr] < min)
                    {
                        min = heightArray[ySqr, xSqr];
                    }
         */
                }
            }
           
            espace = demiEspace;
        }

     /*  for (int x = 0; x < heightmapSize; x++)
       {
           for (int y = 0; y < heightmapSize; y++)
           {
               
                   heightArray[y, x] += Random.Range(0.0f, 0.001f);
              
           }
       }*/
        //normalisation
      /* for (int i = 0; i < heightmapSize; i++)
       {
           for (int j = 0; j < heightmapSize; j++)
               heightArray[j, i] = ((heightArray[j, i] - min) * (1.0f / (max - min)));
       }*/

        /*var value=0.0f;
        var _lissage = true;
        if (_lissage)
        {
            for (int i = 0; i < heightmapSize; i++)
            {
                for (int j = 0; j < heightmapSize; j++)
                {
                    value = 0.0f;
                    int n = 0;
                    for (int k = i - 5; k <= i + 5; k++)
                    {
                        for (int l = j - 5; l <= j + 5; l++)
                        {
                            if ((k >= 0) && (k < heightmapSize) && (l >= 0) && (l < heightmapSize))
                            {
                                n++;
                                value += heightArray[l, k];
                            }
                        }
                    }
                    heightArray[j, i] = (value / n);
                }
            }
        }*/
       //Debug.Log("Nombre d'iteration: " + nbIteration);
       Debug.Log("Execution de Step Square :" + cSqr);
       Debug.Log("Execution de Step Diamond :" + cDmd);
       return heightArray;
        
    }

    //public 
    float StepSquare(int x, int y, int espace)
    {
        cSqr++;
        float heightValue = 0.0f;
        float a = 0.0f;
        float b = 0.0f;
        float c = 0.0f;
        float d = 0.0f;
        float ratio = 0.0f;
        float moyenne = 0.0f;
        float espaceVal = 0.0f;

        if( x >= espace  && y >= espace )
        {
            a = heightArray[y - espace, x - espace];
            ++ratio;
        }
        if (x+espace < heightmapSize && y >= espace)
        {
            b = heightArray[y - espace, x + espace];
            ++ratio;
        }
        if (x >= espace && (y + espace < heightmapSize))
        {
            c = heightArray[y + espace, x - espace];
            ++ratio;
        }
        if ((x + espace < heightmapSize) && (y + espace < heightmapSize))
        { 
            d = heightArray[y + espace, x + espace];
            ++ratio;
        }

        moyenne = (a + b + c + d) / ratio;

       if (espace < 1000)
        {
            espaceVal = (1.0f + ((float)espace / 1000.0f));
        }
        else if (espace > 1000)
        {
            espaceVal = (1.0f + ((float)espace / 10000.0f));
        }
        //Debug.Log("ESPACE VALUE : " + espaceVal);
        heightValue = moyenne;
       //Debug.Log("Valeur du carré:" + heightValue);
        return heightValue;
    }


    float StepDiamond(int x, int y, int espace)
    {
        cDmd++;
        float heightValue = 0.0f;
        float a = 0.0f;
        float b = 0.0f;
        float c = 0.0f;
        float d = 0.0f;
        float ratio = 0.0f;
        float moyenne = 0.0f;
        float espaceVal = 0.0f;

        if (x >= espace)
        {
            a = heightArray[y, x - espace];
            ++ratio;
        }
        if (x + espace < heightmapSize )
        {
            b = heightArray[y, x + espace];
            ++ratio;
        }
        if ( y >= espace)
        {
            c = heightArray[y - espace, x];
            ++ratio;
        }
        if (y + espace < heightmapSize)
        {
            d = heightArray[y + espace, x];
            ++ratio;
        }

        moyenne = (a + b + c + d) / ratio;
        if (espace < 1000)
        {
            espaceVal = (1.0f + ((float)espace / 1000.0f));
        }
        else if(espace > 1000)
        {
            espaceVal = (1.0f + ((float)espace / 10000.0f));
        }
       
       // Debug.Log("ESPACE VALUE : " + espaceVal);
        
        heightValue = moyenne;
        return heightValue;
    }


    void SetupAlphaMap()
    {
        float[,,] map = new float[terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight, 2];
		
		// For each point on the alphamap...
		for (int y = 0; y < terrain.terrainData.alphamapHeight; y++) {
			for (int x = 0; x < terrain.terrainData.alphamapWidth; x++) {
				// Get the normalized terrain coordinate that
				// corresponds to the the point.
				double normX = x * 1.0 / (terrain.terrainData.alphamapWidth - 1);
				double normY = y * 1.0 / (terrain.terrainData.alphamapHeight - 1);
				
				// Get the steepness value at the normalized coordinate.
				double angle = terrain.terrainData.GetSteepness((float)normX, (float)normY);
				
				// Steepness is given as an angle, 0..90 degrees. Divide
				// by 90 to get an alpha blending value in the range 0..1.
				double frac = angle / 90.0;
				map[x, y, 0] = (float)frac;
                map[x, y, 1] = 1 - (float)frac;
			}
		}
		
		terrain.terrainData.SetAlphamaps(0, 0, map);
	
    }
    

    

}
    
    






//RECHERCHE A SUPPRIMER UNE FOIS FINI//


