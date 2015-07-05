using Random = System.Random;
using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    // public SquareMapScript other;


    [SerializeField]
    int width;

    [SerializeField]
    int length;

    [SerializeField]
    List<GameObject> Prefab;

    int[,] grid;

    int[,] seedGrid;

    int actualSeed;






    // Use this for initialization
    void Start()
    {
        CreateGrid(width, length);
        //  other.getResources();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateGrid(int width, int length)
    {
        Random rnd = new Random();
        int seed = 0 ;

        grid = new int[width, length];
        // CreatePlane();

        for ( int j = 0 ; j < length ; ++j )
        {
            for ( int i = 0 ; i < width ; ++i )
            {
                seed = rnd.Next(0, 6);

                //var temp = sqrt(pow((tabX[s] - j), 2) + pow((tabY[s] - i), 2));
                //  Mathf.Sqrt(Mathf.Pow((grid[]-i),2) + Mathf.Pow(grid[] - i),2));

                if ( j == 0 || j == length - 1 || i == 0 || i == length - 1 )
                {
                    //     Debug.Log("Water");
                    CreatePlane(i, j, 4);
                    grid[i, j] = 4;
                }
                else if ( getCaseSeed(i, j) == 0 && seed == 3 )
                {
                    CreatePlane(i, j, 3);
                    grid[i, j] = 3;
                }
                else if ( getCaseSeed(i, j) == 3 )
                {
                    CreatePlane(i, j, 1);
                    grid[i, j] = 1;
                }
                else if ( getCaseSeed(i, j) == 2 )
                {
                    CreatePlane(i, j, 2);
                    grid[i, j] = 2;
                }
                else if ( getCaseSeed(i, j) == 1 )
                {
                    CreatePlane(i, j, 5);
                    grid[i, j] = 5;
                }
                /*else if()
                {
                    CreatePlane(i,j,2);
                }*/
                else
                {
                    //  Debug.Log("HERE");
                    CreatePlane(i, j, seed);
                    grid[i, j] = 0;
                }   //break;

                // Debug.Log("Case [" + i + "," + j + "] = " + grid[i, j]);
            }
            // break;
        }
    }


    void SeedGrid(int nb_seed)
    {

    }

    void CreatePlane(int i, int j, int seed)
    {
        float x;
        float y;
        x = i * 200;
        y = j * 200;
        GameObject go = Instantiate(Prefab[seed]);
        go.transform.position = new Vector3(x+100, 0, y+100);
        go.transform.parent = this.transform;



    }

    void CreateOtherPlane()
    {
        GameObject go = Instantiate(Prefab[0]);
        go.transform.position = new Vector3(1, 0, 0);
        go.transform.parent = this.transform;


    }

    int getCaseSeed(int x, int y)
    {
        return grid[x, y];
        //return actualSeed ;
    }
}
