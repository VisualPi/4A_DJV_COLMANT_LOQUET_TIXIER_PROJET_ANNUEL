using UnityEngine;
using System.Collections;

public class EdibleScript : MonoBehaviour, OtherTargetable
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _gameObject;

    private EdibleInformations infos = new EdibleInformations();
    private string _lastCol = "";
    // Use this for initialization
    public void Start()
    {
        infos.Quantity = infos.DefaultQuantity;
        infos.Position = _transform.position;
        infos.Name = name;
    }

    // Update is called once per frame
    void Update()
    {
        if (infos.Quantity == infos.DefaultQuantity / 2) //TODO : a voir, fonctionnera que si on décremente la nourriture par 1 !!
        {
            _transform.localScale = tag.Equals("Water")
                                    ? new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z / 2)
                                    : new Vector3(_transform.localScale.x / 2, _transform.localScale.y, _transform.localScale.z);
        }
        if (infos.Quantity < 1)
        {
            if (tag.Equals("Food"))
            {
                GameData.SquareMaps[_lastCol].GetContext().Food.Remove(this);
            }
            else if (tag.Equals("Water"))
            {
                GameData.SquareMaps[_lastCol].GetContext().Water.Remove(this);
            }
            GameData.Ressources.Remove(this.name);
            Destroy(_gameObject);
        }
    }
    public void OnTriggerEnter(Collider col)
    {
        if (tag.Equals("")) return;
        if (col.tag.Equals("SquareMap") && !col.name.Equals(_lastCol))
        {
            if (!_lastCol.Equals(""))
            {
                if (tag.Equals("Food"))
                    GameData.SquareMaps[_lastCol].GetContext().Food.Remove(this);
                else if (tag.Equals("Water"))
                    GameData.SquareMaps[_lastCol].GetContext().Water.Remove(this);
            }
            if (tag.Equals("Food"))
                GameData.SquareMaps[col.name].GetContext().Food.Add(this);
            else if (tag.Equals("Water"))
                GameData.SquareMaps[col.name].GetContext().Water.Add(this);
            //Debug.Log("Now SquareMap : " + col.name + " list contains " + this.name);
            _lastCol = col.name;
        }
    }

    public Transform GetTransform()
    {
        return _transform;
    }
    public int GetQuantity()
    {
        return infos.Quantity;
    }
    public void Take(int value)
    {
        infos.Quantity -= value;
    }

    public EdibleInformations GetInformations()
    {
        return new EdibleInformations(infos);
    }
}


