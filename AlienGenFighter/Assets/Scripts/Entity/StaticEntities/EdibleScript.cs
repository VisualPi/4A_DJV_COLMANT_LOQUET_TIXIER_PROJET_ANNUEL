using System.Runtime.InteropServices;
using UnityEngine;
using Assets.Scripts.Misc;

public class EdibleScript : MonoBehaviour, OtherTargetable
{
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private GameObject _gameObject;

    private EdibleInformations _infos = new EdibleInformations();
    private string _lastCol = "";

    // Use this for initialization
    public void Start()
    {
        _infos.Quantity = _infos.DefaultQuantity;
        _infos.Position = _transform.position;
        _infos.Name = name;
    }

    // Update is called once per frame
    void Update()
    {
        if ( _infos.Quantity == _infos.DefaultQuantity / 2 ) //TODO : a voir, fonctionnera que si on décremente la nourriture par 1 !!
        {
            _transform.localScale = tag.Equals("Water") // TODO JO FROM AMAU : Pourquoi Water et pas un autre ? (groupe, food, ...)
                                    ? new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z / 2)
                                    : new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z);
        }
        if ( _infos.Quantity < 1 )
        {
            //DeleteEdible();
        }
    }

    private void DeleteEdible()
    {
        if ( tag.Equals("Food") )
        {
            GameData.SquareMaps[_lastCol].Context.Food.Remove(this);
        }
        else if ( tag.Equals("Water") )
        {
            GameData.SquareMaps[_lastCol].Context.Water.Remove(this);
        }
        GameData.Ressources.Remove(name);
        Destroy(_gameObject);
    }
    public void OnTriggerEnter(Collider col)
    {
        if ( tag.Equals("") ) return;
        if ( col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol) )
        {
            if (tag.Equals("Food"))
            {
                if (!_lastCol.Equals(""))
                    GameData.SquareMaps[_lastCol].Context.Food.Remove(this);
                GameData.SquareMaps[col.name].Context.Food.Add(this);
            }
            else if (tag.Equals("Water"))
            {
                if (!_lastCol.Equals(""))
                    GameData.SquareMaps[_lastCol].Context.Water.Remove(this);
                GameData.SquareMaps[col.name].Context.Water.Add(this);
            }
            Log.Debug.Map("Now SquareMap : {0} list contains {1}.", col.name, name);
            _lastCol = col.name;
        }
    }

    public int Quantity {
        get { return _infos.Quantity; }
    }

    public int Take(int value)
    {
        if (_infos.Quantity - value <= 0)
        {
            Debug.LogWarning("Deleting because " + _infos.Quantity);
            value = _infos.Quantity;
            DeleteEdible();
        }
        _infos.Quantity -= value;
        return value;
    }

    public Transform Transform {
        get { return _transform; }
    }

    public EdibleInformations Informations {
        get { return _infos; }
    }
}


