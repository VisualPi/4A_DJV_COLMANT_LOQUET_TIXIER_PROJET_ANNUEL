using Assets.Scripts.Context;
using UnityEngine;

public class SquareMapScript : MonoBehaviour
{
    private float moveSpeedInfluence = 1.0f;
    private float temperature = 25.0f;

    private int foodQuantity = 50;
    private int drinkableWater = 1000;

    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _transform;
    [SerializeField] private CameraManagerScript _cameraManager;
    [SerializeField] private Camera _squareCamera;

    private SquareContext _context = new SquareContext();
    void Start()
    {
        if (_squareCamera == null)
        { // TODO : Fix it
            Debug.Log("error : _squareCamera is null");
            _squareCamera = GameObject.Find("Camera3D").GetComponent<Camera>();
        }
        if (_cameraManager == null)
        {
            Debug.Log("error : _cameraManager is null");
            _cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManagerScript>();
        }
    }

    public CameraManagerScript CameraManager
    {
        get { return _cameraManager; }
        set { _cameraManager = value; }
    }

    public Camera SquareCamera
    {
        get { return _squareCamera; }
        set { _squareCamera = value; }
    }

    public void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Enter on square");
        if (col.tag.Equals("entity"))
            _context.NbEntity+=1;
    }
    public void OnTriggerExit(Collider col)
    {
        //Debug.Log("Exit from square");
        if (col.tag.Equals("entity"))
            _context.NbEntity -= 1;
        if (_context.NbEntity < 0)
            _context.NbEntity = 0;//TODO : au cas ou pour les tests, a enlever apres
    }
    void setEnvironnementalConstraint(float _moveSpeedInfluence, float _temperature)
    {
        moveSpeedInfluence = _moveSpeedInfluence;
        temperature = _temperature;

    }
    private void setResources(int _foodQuantity, int _drinkableWater)
    {
        foodQuantity = _foodQuantity;
        drinkableWater = _drinkableWater;
    }
    public void getResources()
    {
        Debug.Log("NB ENTITE : " + _context.NbEntity);
        Debug.Log("NOURRITURE : " + foodQuantity.ToString());
        Debug.Log("EAU : " + drinkableWater.ToString());
    }

    public SquareContext GetContext()
    {
        return _context;
    }
    public void SetContext(SquareContext context)
    {
        _context = context;
    }
    void consumeResources(int _foodQuantity, int _drinkableWater)
    {
        foodQuantity = foodQuantity - _foodQuantity;
        drinkableWater = drinkableWater - _drinkableWater;
    }
    void modifyEnvironnementalConstraint(float valueOfChangeInPercent)
    {
        moveSpeedInfluence *= valueOfChangeInPercent;
        temperature *= valueOfChangeInPercent;
    }

    void SetEntityLastMapName()
    {
        for (int i = 0; i < _context.NbEntity; ++i)
        {
            _context.Entities[i].GetColliderScript().SetLastCol(_collider.name);
        }
    }

    public void OnMouseOver()
    {
        getResources();
    }

    public void OnMouseDown()
    {
        if (_cameraManager.IsCurrentCamera(_squareCamera)) return;

        _squareCamera.transform.position = new Vector3(_transform.position.x, _transform.position.y + 50, _transform.position.z);
        _cameraManager.ShowCamera(_squareCamera);
    }

    public void AddEdible(GameObject go, int nb)
    {
        RaycastHit hit;
        Ray ray;
        for (var i = 0; i < nb; i++)
        {
            ray = new Ray(new Vector3(Random.Range(_collider.bounds.min.x, _collider.bounds.max.x), 255f, Random.Range(_collider.bounds.min.z, _collider.bounds.max.z)), Vector3.down);
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Map")))
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
