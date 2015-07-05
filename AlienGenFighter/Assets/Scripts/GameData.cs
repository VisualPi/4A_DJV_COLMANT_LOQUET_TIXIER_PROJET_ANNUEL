using UnityEngine;
using System.Collections.Generic;

public class GameData
{
    public static Dictionary<string, SquareMapScript> SquareMaps = new Dictionary<string, SquareMapScript>();
    public static Dictionary<string, OtherTargetable> Ressources = new Dictionary<string, OtherTargetable>();
    public static Dictionary<string, EntityScript> Entities = new Dictionary<string, EntityScript>();
    public static Vector3 MapSize;
}
