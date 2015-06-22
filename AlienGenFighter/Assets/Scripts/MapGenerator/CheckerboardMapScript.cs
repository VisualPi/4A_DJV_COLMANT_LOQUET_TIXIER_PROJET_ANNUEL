using Assets.Scripts.GUI;
using Assets.Scripts.GUI.Misc;
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
    [SerializeField]
    private Menu _informationMenu;
    [SerializeField]
    private ManagedInformationMapList _managedInformationMenu;
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

        for ( int i = 0 ; i < 8 ; i++ )
        {
            for ( int j = 0 ; j < 8 ; j++ )
            {
                if ( x > (int)gameArea.sizeMap.x - 32 )
                {
                    x = 0;
                }
                if ( z > (int)gameArea.sizeMap.z - 32 )
                {
                    z = 0;
                }
                if ( gameArea.sizeMap.x == 512 && gameArea.sizeMap.z == 512 )
                {
                    var colliderPosition = new Vector3(32+ x, 100, 32 + z);
                    actualCase = (GameObject)Instantiate(CaseOfMap, colliderPosition, Quaternion.identity);
                    actualCase.transform.parent = Terrain.transform;
                    actualCase.GetComponent<BoxCollider>().name = "SquareMap_" + cpt;
                    var currentSquareMapScript = actualCase.GetComponent<SquareMapScript>();

                    currentSquareMapScript.CameraManager = _cameraManager;
                    currentSquareMapScript.SquareCamera = _squareCamera;
                    currentSquareMapScript.InformationMenu = _informationMenu;
                    currentSquareMapScript.ManagedInformationMenu = _managedInformationMenu;

                    GameData.SquareMaps.Add("SquareMap_" + cpt, currentSquareMapScript);
                    x += 64;
                }
                else if ( gameArea.sizeMap.x == 1024 && gameArea.sizeMap.z == 1024 )
                {
                    var colliderPosition = new Vector3(64 + x, 100, 64 + z);
                    actualCase = (GameObject)Instantiate(CaseOfMap, colliderPosition, Quaternion.identity);
                    actualCase.transform.parent = Terrain.transform;
                    actualCase.GetComponent<BoxCollider>().name = "SquareMap_" + cpt;
                    actualCase.GetComponent<BoxCollider>().size = new Vector3(128, actualCase.GetComponent<BoxCollider>().size.y, 128);
                    var currentSquareMapScript = actualCase.GetComponent<SquareMapScript>();
                    currentSquareMapScript.GetDelimitation().transform.localScale = new Vector3(13, 1, 13);

                    currentSquareMapScript.CameraManager = _cameraManager;
                    currentSquareMapScript.SquareCamera = _squareCamera;
                    currentSquareMapScript.InformationMenu = _informationMenu;
                    currentSquareMapScript.ManagedInformationMenu = _managedInformationMenu;

                    GameData.SquareMaps.Add("SquareMap_" + cpt, currentSquareMapScript);
                    x += 128;
                }
                cpt++;
            }
            if ( gameArea.sizeMap.x == 512 && gameArea.sizeMap.z == 512 )
                z += 64;
            else if ( gameArea.sizeMap.x == 1024 && gameArea.sizeMap.z == 1024 )
                z += 128;
        }
    }
}
