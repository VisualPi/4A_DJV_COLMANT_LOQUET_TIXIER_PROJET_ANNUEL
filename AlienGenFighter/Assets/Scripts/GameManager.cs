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
            foreach ( var ent in map.Value.Context.Entities )
            {
                //Debug.Log("Foreach " + ent.name);
                var next = false;

                for ( var priority = 0 ; priority < ent.Rules.Rules.Length ; ++priority )
                {
                    //Debug.Log("For priority = " + priority);
                    for ( var group = 0 ; group < ent.Rules.Rules[priority].Count ; ++group )
                    {
                        //Debug.Log("For group = " + group);
                        for ( var rule = 0 ; rule < ent.Rules.Rules[priority][group].RuleList.Count ; ++rule )
                        {
                            //Debug.Log("For rule = " + rule);
                            var curRule = ent.Rules.Rules[priority][group].RuleList[rule];
                            if ( curRule.Condition.Test(ent) )
                            {
                                curRule.Action.Execute(ent);
                                if ( rule == ent.Rules.Rules[priority][group].RuleList.Count - 1 )
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
