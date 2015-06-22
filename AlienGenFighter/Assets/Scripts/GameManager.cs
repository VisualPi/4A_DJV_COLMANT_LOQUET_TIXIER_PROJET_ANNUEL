using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RandomTerrainScript _terrain;
    [SerializeField]
    private GameObject _food;
    [SerializeField]
    private GameObject _water;
    public void Start()
    {
        //_terrain.PutFoodAndWater(50);
        _terrain.CreateCivilisations();
    }

    private bool first = true;
    public void Update()
    {
        if ( first ) //deg mais bon
        {
            foreach ( var map in GameData.SquareMaps )
            {
                map.Value.AddEdible(_food, 3);
                map.Value.AddEdible(_water, 3);
            }
            first = false;
        }

        foreach ( var map in GameData.SquareMaps )
        {
            //Debug.Log("Foreach " + map.Value.name);
            foreach ( var ent in map.Value.GetContext().Entities )
            {
                //Debug.Log("Foreach " + ent.name);
                bool next = false;
                for ( var priority = 0 ; priority < ent.GetRules().GetRules().Length ; ++priority )
                {
                    //Debug.Log("For priority = " + priority);
                    for ( var group = 0 ; group < ent.GetRules().GetRules()[priority].Count ; ++group )
                    {
                        //Debug.Log("For group = " + group);
                        for ( var rule = 0 ; rule < ent.GetRules().GetRules()[priority][group].GetRuleList().Count ; ++rule )
                        {
                            //Debug.Log("For rule = " + rule);
                            var curRule = ent.GetRules().GetRules()[priority][group].GetRuleList()[rule];
                            if ( curRule._condition.Test(ent) )
                            {
                                curRule._action.Execute(ent);
                                if ( rule == ent.GetRules().GetRules()[priority][group].GetRuleList().Count - 1 )
                                    next = true;
                            }
                            else
                                break;
                        }
                        if ( next )
                            break;
                    }
                    if ( next )
                        break;
                }

            }
        }
    }
}
