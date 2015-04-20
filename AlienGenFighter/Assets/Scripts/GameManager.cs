using UnityEngine;
using System;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

    public void Update()
    {
        foreach( var map in MapManagerScript._SquareMaps)
        {
			//Debug.Log("Foreach " + map.Value.name);
            foreach(var ent in map.Value.GetContext().GetEntityOnCurrentSquare())
            {
				//Debug.Log("Foreach " + ent.name);
				map.Value.GetContext().SetCurrentEntity(ent);
                bool next = false;
                for(var priority = 0 ; priority < ent.GetRules().GetRules().Length ; ++priority)
                {
					//Debug.Log("For priority = " + priority);
					for(var group = 0 ; group < ent.GetRules().GetRules()[priority].Count ; ++group)
                    {
						//Debug.Log("For group = " + group);
						for(var rule = 0; rule < ent.GetRules().GetRules()[priority][group].GetRuleList().Count; ++rule)
                        {
							//Debug.Log("For rule = " + rule);
							var curRule = ent.GetRules().GetRules()[priority][group].GetRuleList()[rule];
                            if (curRule._condition.Test(map.Value.GetContext()))
                            {
                                curRule._action.Execute(map.Value.GetContext());
                                if (rule == ent.GetRules().GetRules()[priority][group].GetRuleList().Count - 1)
                                    next = true;
                            }
                            else
                                break;
                        }
                        if (next)
                            break;
                    }
                    if (next)
                        break;
                }

            }
        }
    }



    public bool isHungry(SquareContext context)
    {
        //tester si l'entité a besoin de manger
        return true;
    }


}
