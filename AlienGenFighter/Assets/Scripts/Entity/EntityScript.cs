using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using Assets.Scripts.Context;
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
    [SerializeField] private List<Material>         _materials = new List<Material>(5);
    
    private DnaScript                               _dna;
    private CapacityScript                          _capacities;

    private bool                                    _isPlayable = false;
    private bool                                    _isAlive = true;

    private EntityRules                             _rules;
    private EntityStateScript                       _state;
    private EntityContext                           _context;
    public GroupContext                             GroupContext { get; set; }
    public GameObject                               GroupObject { get; set; }

    private float                                   _foodTime;
    private float                                   _drinkTime;

    public bool                                     _isInGroup { get; set; }

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
        _isInGroup = false;
        _collider.name = name;
    }

    void OnMouseDown()
    {
        Debug.Log("ADN = " + _dna.ToString());
        Debug.Log("Food : " + _state.GetFood() + " Water : " + _state.GetWater());
        Debug.Log("Context : Food count = " + _context.Food.Count + ", Water count = " + _context.Water.Count);
        if ( _isInGroup )
        {
            Debug.Log("For this entity ["
                + name
                + "] Group : Leader is "
                + GroupContext.Leader.name
                + " and there is "
                + GroupContext.Entities.Count
                + " entities in the group"
                );
        }

    }
    public void InitFromDna()
    {
        _context.Memory = _dna.GetGeneAt(ECharateristic.Memory);
        _meshRenderer.material = _materials[_dna.GetGeneAt(ECharateristic.Skincolor)];
        _pastilleRenderer.material = _materials[_dna.GetGeneAt(ECharateristic.Skincolor)];
        if (_dna.GetGeneAt(ECharateristic.Sociability) > 80)
        {
            //_groupCollider.enabled = true;
            //_groupContext = new GroupContext {Leader = this};
            //_groupContext.Entities.Add(this);
            //Debug.Log(name + " is ready to create a group");
        }
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
                _state.SetFood(_state.GetFood() - 1);
                _foodTime = 0f;
            }
            if ( _drinkTime >= 10f )
            {
                _state.SetWater(_state.GetWater() - 1);
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
        this.enabled = false;
    }
    public void EnableComponents()
    {
        this.enabled = true;
        _movement.enabled = true;
        _isPlayable = true;
        _movement.SetPlayable(true);
        _isAlive = true;
        _transform.parent = null;
    }

    public bool AskForGroup()
    {
        if ( _isInGroup ) //TODO : tester l'abandon de groupe
            return false;
        var r = Random.Range(0, 100);
        //Debug.Log("random is : " + r + " and sociability is : " + _dna.GetGeneAt(ECharateristic.Sociability) + " and timidity : " + _dna.GetGeneAt(ECharateristic.Timidity));
        float coef = ((_dna.GetGeneAt(ECharateristic.Sociability)*((100f - _dna.GetGeneAt(ECharateristic.Timidity))/100f)));
        return r < coef;
    }

    #region getterSetter
    public EntityCollisionScript GetColliderScript()
    {
        return _collision;
    }
    public void SetColliderScript(EntityCollisionScript col)
    {
        _collision = col;
    }
    public Transform GetTransform()
    {
        return _transform;
    }
    public void SetTransform(Transform tr)
    {
        _transform = tr;
    }
    public EntityMovementScript GetMovement()
    {
        return _movement;
    }
    public void SetMovement(EntityMovementScript mv)
    {
        _movement = mv;
    }
    public EntityRules GetRules()
    {
        return _rules;
    }
    public void SetRules(EntityRules rules)
    {
        _rules = rules;
    }
    public EntityStateScript GetState()
    {
        return _state;
    }
    public void SetState(EntityStateScript state)
    {
        _state = state;
    }
    public EntityContext GetContext()
    {
        return _context;
    }
    public void SetContext(EntityContext context)
    {
        _context = context;
    }
    public DnaScript GetDNA()
    {
        return _dna;
    }
    public void SetDNA(DnaScript dna)
    {
        _dna = dna;
    }
    public CapacityScript GetCapacity()
    {
        return _capacities;
    }
    public void SetCapacity(CapacityScript cap)
    {
        _capacities = cap;
    }
    public bool GetPlayable()
    {
        return _isPlayable;
    }
    public bool GetAlive()
    {
        return _isAlive;
    }
    public void SetPlayable(bool b)
    {
        _isPlayable = b;
    }
    public void SetAlive(bool b)
    {
        _isAlive = b;
    }
    #endregion
}

