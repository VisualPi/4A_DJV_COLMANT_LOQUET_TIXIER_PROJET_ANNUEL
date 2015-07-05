using UnityEngine;
using Assets.Scripts.Context;
using Assets.Scripts.Group;

public class GroupColliderScript : MonoBehaviour
{
    [SerializeField] private GroupScript _group;

    public void OnTriggerEnter(Collider col)
    {
        if ( col.tag.Equals("Food") )
        {
            _group.GroupContext.AddFood(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
        if ( col.tag.Equals("Water") )
        {
            _group.GroupContext.AddWater(( (EdibleScript)GameData.Ressources[col.name] ).Informations);
        }
        if ( col.tag.Equals("Entity") )
        {
            if (GameData.Entities[col.name].AskForGroup())
            {
                //Debug.Log("Adding to the group ! ");
                _group.GroupContext.AddEntity(GameData.Entities[col.name]);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if ( col.tag.Equals("Entity") )
        {
            GameData.Entities[col.name].GroupContext = new GroupContext(_group.GroupContext);
        }
    }
}
