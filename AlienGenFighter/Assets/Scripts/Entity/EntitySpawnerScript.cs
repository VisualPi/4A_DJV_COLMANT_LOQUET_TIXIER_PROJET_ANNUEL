﻿using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerScript : MonoBehaviour
{
    [SerializeField]
    private int _nbDefaultEntity = 100;
    [SerializeField]
    private Transform _transform;
    private List<EntityScript> _entitiesInCivilisation;
    // Use this for initialization
    void Start()
    {
        _entitiesInCivilisation = new List<EntityScript>(_nbDefaultEntity);
        var skin = (byte)Random.Range(0, 4);
        for ( var i = 0 ; i < _nbDefaultEntity ; ++i )
        {
            var e = EntityManagerScript.GetFromQueue();
            e.transform.position = new Vector3(_transform.position.x + Random.Range(1.0f, 15f), _transform.position.y, _transform.position.z + Random.Range(1.0f, 15f));
            e.Init();
            e.DNA.SetGeneAt(ECharateristic.Skincolor, skin);
            e.InitFromDna();
            _entitiesInCivilisation.Add(e);
        }
    }
}
