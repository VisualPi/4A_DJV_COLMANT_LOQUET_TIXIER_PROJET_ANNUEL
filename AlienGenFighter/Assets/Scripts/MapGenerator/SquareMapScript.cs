using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Context;
using Assets.Scripts.GUI;
using Assets.Scripts.GUI.ScrollList.Item;
using Assets.Scripts.GUI.ScrollList.Manager;
using Assets.Scripts.Misc;
using UnityEngine;

public class SquareMapScript : MonoBehaviour
{
    private float _moveSpeedInfluence = 1.0f;
    private float _temperature = 25.0f;

    private int _foodQuantity = 50;
    private int _drinkableWater = 1000;

    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private Transform _delimination;

    private SquareContext _context = new SquareContext();

    void Start()
    {
        if ( SquareCamera == null )
        {
            Debug.Log("error : _squareCamera is null");
            SquareCamera = GameObject.Find("Camera3D").GetComponent<Camera>();
        }
        if ( CameraManager == null )
        {
            Debug.Log("error : _cameraManager is null");
            CameraManager = GameObject.Find("CameraManager").GetComponent<CameraManagerScript>();
        }
    }

    public CameraManagerScript CameraManager { get; set; }
    public Camera SquareCamera { get; set; }
    public Menu InformationMenu { get; set; }
    public ManagedInformationMapList ManagedInformationMenu { get; set; }

    public void OnTriggerEnter(Collider col)
    {
        Log.Trace.SquareMap("Enter");
        if ( col.tag.Equals("Entity") )
            _context.NbEntity += 1;
    }
    public void OnTriggerExit(Collider col) {
        Log.Trace.SquareMap("Exit");
        if ( col.tag.Equals("Entity") )
            _context.NbEntity -= 1;
        if ( _context.NbEntity < 0 )
            _context.NbEntity = 0;//TODO : au cas ou pour les tests, a enlever apres
    }
    void setEnvironnementalConstraint(float moveSpeedInfluence, float temperature)
    {
        _moveSpeedInfluence = moveSpeedInfluence;
        _temperature = temperature;

    }
    private void setResources(int foodQuantity, int drinkableWater)
    {
        _foodQuantity = foodQuantity;
        _drinkableWater = drinkableWater;
    }

    public void getResources()
    {
        Debug.Log("NB ENTITE : " + _context.NbEntity);
        Debug.Log("NOURRITURE : " + _foodQuantity.ToString());
        Debug.Log("EAU : " + _drinkableWater.ToString());
    }

    public Transform GetDelimitation()
    {
        return _delimination;
    }

    public SquareContext Context {
        get { return _context; }
        set { _context = value; }
    }

    void consumeResources(int foodQuantity, int drinkableWater)
    {
        _foodQuantity -= foodQuantity;
        _drinkableWater -= drinkableWater;
    }
    void modifyEnvironnementalConstraint(float valueOfChangeInPercent)
    {
        _moveSpeedInfluence *= valueOfChangeInPercent;
        _temperature *= valueOfChangeInPercent;
    }

    void SetEntityLastMapName()
    {
        for ( int i = 0 ; i < _context.NbEntity ; ++i )
        {
            _context.Entities[i].CollisionScript.SetLastCol(_collider.name);
        }
    }

    public void OnMouseOver()
    {
        Log.Debug.Ui("OnMouseOver() want RemoveAll");
        ManagedInformationMenu.RemoveAll();

        Log.Debug.Ui("OnMouseOver() want Populate");
        ManagedInformationMenu.PopulatePanel(
            new List<ItemInformationMap> {
                new ItemInformationMap { Name = "Entity :", Value = _context.NbEntity.ToString() },
                new ItemInformationMap { Name = "Food :", Value = _context.Food.Sum(f => f.Quantity).ToString() },
                new ItemInformationMap { Name = "Water :", Value = _context.Water.Sum(w => w.Quantity).ToString() }
            });

        InformationMenu.IsOpen = true;
    }

    public void OnMouseExit()
    {
        InformationMenu.IsOpen = false;
    }

    public void OnMouseDown()
    {
        if ( CameraManager.IsCurrentCamera(SquareCamera) )
            return;

        RaycastHit hit;
        var ray = new Ray(new Vector3(_transform.position.x, 550f, _transform.position.z), Vector3.down);
        if ( Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")) )
        {
            SquareCamera.transform.position = new Vector3(hit.point.x, hit.point.y+50, hit.point.z);
        }

        CameraManager.ShowCamera(SquareCamera);
    }

    public void AddEdible(GameObject go, int nb)
    {
        RaycastHit hit;
        Ray ray;
        for ( var i = 0 ; i < nb ; i++ )
        {
            ray = new Ray(new Vector3(Random.Range(_collider.bounds.min.x, _collider.bounds.max.x), 520f, Random.Range(_collider.bounds.min.z, _collider.bounds.max.z)), Vector3.down);
            if ( Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")) )
            {
                var obj = Instantiate(go, new Vector3(hit.point.x,
                                                        hit.point.y + go.transform.localScale.y,
                                                        hit.point.z), Quaternion.identity) as GameObject;
                obj.name = obj.name + "_" + this.name + "_" + i;
                GameData.Ressources.Add(obj.name, obj.GetComponent<EdibleScript>());
            }
        }
    }

}
