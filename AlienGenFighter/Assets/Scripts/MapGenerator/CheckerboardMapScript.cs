using UnityEngine;

public class CheckerboardMapScript : MonoBehaviour
{
    [SerializeField]
    GameObject Terrain;

   // [SerializeField]
   // Vector3 sizeMap;

    [SerializeField]
    GameObject CaseOfMap;

    [SerializeField]
    RandomTerrainScript gameArea;

    [SerializeField]
    private Camera _squareCamera;

    [SerializeField]
    private CameraManagerScript _cameraManager;

    BoxCollider[] checkerboard;
    // Use this for initialization
    /*void Start()
    {
        int nbCase = (int)sizeMap.x / 100;
        int nbCaseTotal = nbCase * nbCase;
        checkerboard = new BoxCollider[nbCaseTotal];
        
        for (int i = 0; i < nbCaseTotal; ++i)
        {
            checkerboard[i] = Terrain.AddComponent<BoxCollider>();
            
            checkerboard[i].size = new Vector3(100, 3, 100);
            if (i == 0)
            {
                checkerboard[i].center = new Vector3(50, 9, 50);
            }
            else
            {
                if (i < nbCase)
                {
                    checkerboard[i].center = checkerboard[i - 1].center + new Vector3(100, 0, 0);
                }
                else if (i >= nbCase && i < (nbCase * 2))
                {
                    checkerboard[i].center = checkerboard[i - nbCase].center + new Vector3(0, 0, 100);
                }
                else if (i >= (nbCase*2) && i < (nbCase * 3))
                {
                    checkerboard[i].center = checkerboard[i - nbCase].center + new Vector3(0, 0, 100);
                }
                else if (i >= (nbCase * 3) && i < (nbCase * 4))
                {
                    checkerboard[i].center = checkerboard[i - nbCase].center + new Vector3(0, 0, 100);
                }
                else if (i >= (nbCase * 4) && i < (nbCase * 5))
                {
                    checkerboard[i].center = checkerboard[i - nbCase].center + new Vector3(0, 0, 100);
                }
            }
        }

    }
    */
    void Awake()
    {
        Debug.Log(gameArea.sizeMap.x);
        Debug.Log(gameArea.sizeMap.z);

        //division 64 
        int nbCaseX = (int)gameArea.sizeMap.x / 64;
        int nbCaseZ = (int)gameArea.sizeMap.z / 64;
        //int nbCaseTotal = nbCase * nbCase;
        // checkerboard = new BoxCollider[nbCaseTotal];
        GameObject actualCase;
        Terrain.layer = 8;
        int x = 0;
        int z = 0;
        int cpt = 0;
        
        for (int i = 0; i < nbCaseZ; i++)
        {
            for (int j = 0; j < nbCaseX; j++)
            {
                if (x > (int)gameArea.sizeMap.x-32)
                {
                    x = 0;
                }
                if (z > (int)gameArea.sizeMap.z - 32)
                {
                    z = 0;
                }
                Vector3 colliderPosition = new Vector3(32+x, 100, 32+z);

                actualCase = (GameObject)Instantiate(CaseOfMap, colliderPosition, Quaternion.identity);
                actualCase.transform.parent = Terrain.transform;
                actualCase.GetComponent<BoxCollider>().name = "SquareMap_" + cpt;

                // Define for does not "GetComponent" for all component in SquareMapScript.
                var currentSquareMapScript = actualCase.GetComponent<SquareMapScript>();
                currentSquareMapScript.CameraManager = _cameraManager;
                currentSquareMapScript.SquareCamera = _squareCamera;

                MapManagerScript._SquareMaps.Add("SquareMap_" + cpt, currentSquareMapScript);
              //  Debug.Log(MapManagerScript._SquareMaps["SquareMap_" + cpt]);
                x += 64;
                cpt++;
            }
            z += 64;
            
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
