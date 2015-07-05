using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Context;
using Assets.Scripts.Misc;
using Random = UnityEngine.Random;

[Serializable]
public class EntityScript : MonoBehaviour
{
    #region definitions
    [SerializeField] private Transform              _transform;
    [SerializeField] private GameObject             _gameObject;
    [SerializeField] private Rigidbody              _rigidbody;
    [SerializeField] private EntityMovementScript   _movement;
    [SerializeField] private EntityCollisionScript  _collision;
    [SerializeField] private CapsuleCollider        _collider;
    [SerializeField] private MeshRenderer           _pastilleRenderer;
    [SerializeField] private MeshRenderer           _meshRenderer;
    [SerializeField] private EntityCollisionScript  _contextCollision;
    [SerializeField] private List<Material>         _materials = new List<Material>(5);
    
    private DnaScript                               _dna;
    private CapacityScript                          _capacities;

    private bool                                    _isPlayable = false;
    private bool                                    _isAlive = true;

    private EntityRules                             _rules;
    private EntityStateScript                       _state;
    private EntityContext                           _context;

    public GroupContext                             GroupContext { get; set; }

    private float                                   _foodTime;
    private float                                   _drinkTime;

    public bool                                     IsInGroup { get; set; }

    #endregion

    public void Init()
    {
        _dna = new DnaScript();
        _dna.SetGeneAt(ECharateristic.Height, 1);
        _capacities = new CapacityScript();
        _rules = new EntityRules();
        _state = new EntityStateScript();
        _context = new EntityContext();
        _foodTime = 0f;
        _drinkTime = 0f;
        _movement.Init();
        GroupContext = null;
        IsInGroup = false;
        _collider.name = name;
        _collision.EntName = name;
        _contextCollision.EntName = name;
    }

    void OnMouseDown() {
        Log.Trace.Entity("INFO OnMouseDown\nADN = {0, -10}\n Food = {1, -10}\n Water = {2, -10}\n\nContext :\nFood count = {3, -10}\nWater count = {4, -10}",
            _dna, _state.Food, _state.Water, _context.Food.Count, _context.Water.Count);
        //Debug.Log("ADN = " + _dna);
        //Debug.Log("Food : " + _state.Food + " Water : " + _state.Water);
        //Debug.Log("Context : Food count = " + _context.Food.Count + ", Water count = " + _context.Water.Count);
        if ( IsInGroup ) {
            Log.Trace.Entity("INFO OnMouseDown _isInGoroup\nFor this entity [{0}] Group : Leader is {1} and there is {2} entities in the group.",
                name, GroupContext.Leader.name, GroupContext.Entities.Count);
            //Debug.Log("For this entity ["
            //    + name
            //    + "] Group : Leader is "
            //    + GroupContext.Leader.name
            //    + " and there is "
            //    + GroupContext.Entities.Count
            //    + " entities in the group"
            //    );
        }
    }

    public void InitFromDna()
    {
        _context.Memory = _dna.GetGeneAt(ECharateristic.Memory);
        _meshRenderer.material = _materials[_dna.GetGeneAt(ECharateristic.Skincolor)];
        _pastilleRenderer.material = _materials[_dna.GetGeneAt(ECharateristic.Skincolor)];
        //if (_dna.GetGeneAt(ECharateristic.Sociability) > 80)
        //{
            //_groupCollider.enabled = true;
            //_groupContext = new GroupContext {Leader = this};
            //_groupContext.Entities.Add(this);
            //Debug.Log(name + " is ready to create a group");
        //}
    }

    void Update()
    {
        if ( _isPlayable )
        {
            //TODO: things
            _foodTime += Time.deltaTime;
            _drinkTime += Time.deltaTime;
            if ( _foodTime >= 10f )
            {
                _state.Food = _state.Food - 1; // TODO JO FROM AMAU : Il peut avoir moins de 0 de nourriture ?
                _foodTime = 0f;
            }
            if ( _drinkTime >= 10f )
            {
                _state.Water = _state.Water - 1; // TODO JO FROM AMAU : Il peut avoir moins de 0 d'eau ?
                _drinkTime = 0f;
            }
        }
    }

    public void DisableComponents()
    {
        _movement.enabled = false;
        _isAlive = false;
        _isPlayable = false;
        _movement.SetPlayable(false);
        enabled = false;
        GroupContext = null;
    }

    public void EnableComponents()
    {
        enabled = true;
        _movement.enabled = true;
        _isPlayable = true;
        _movement.SetPlayable(true);
        _isAlive = true;
        _transform.parent = null;
    }

    public bool AskForGroup()
    {
        if ( IsInGroup ) //TODO : tester l'abandon de groupe
            return false;
        var r = Random.Range(0, 100);
        //Debug.Log("random is : " + r + " and sociability is : " + _dna.GetGeneAt(ECharateristic.Sociability) + " and timidity : " + _dna.GetGeneAt(ECharateristic.Timidity));
        var coef = ((_dna.GetGeneAt(ECharateristic.Sociability)*((100f - _dna.GetGeneAt(ECharateristic.Timidity))/100f)));
        return r < coef;
    }

    #region getterSetter

    public EntityCollisionScript CollisionScript {
        get { return _collision; }
        set { _collision = value; }
    }

    public Transform Transform {
        get { return _transform; }
        set { _transform = value; }
    }

    public EntityMovementScript Movement {
        get { return _movement; }
        set { _movement = value; }
    }

    public EntityRules Rules {
        get { return _rules; }
        set { _rules = value; }
    }

    public EntityStateScript State {
        get { return _state; }
        set { _state = value; }
    }

    public EntityContext Context {
        get { return _context; }
        set { _context = value; }
    }

    public DnaScript DNA {
        get { return _dna; }
        set { _dna = value; }
    }

    public CapacityScript Capacities {
        get { return _capacities; }
        set { _capacities = value; }
    }

    public bool Playable {
        get { return _isPlayable; }
        set { _isPlayable = value; }
    }

    public bool Alive {
        get { return _isAlive; }
        set { _isAlive = value; }
    }
    #endregion
}

