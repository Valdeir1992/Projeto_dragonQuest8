/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactory : GenericSingleton<EnemyFactory>
{
    #region PRIVATE VARIABLES
    [SerializeField] private EntityView[] _listPrefabs;
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void OnEnable()
    {
     
    }
    private void Start()
    {
     
    }

    private void OnDisable()
    {
     
    }

    #endregion

    #region OWN METHODS

    public EntityView[] GetEnemies(EntityId[] enemies)
    {
        List<EntityView> list = new List<EntityView>();
        foreach (var item in enemies)
        {
            for (int i = 0; i < _listPrefabs.Length; i++)
            {
                if(item.Id == _listPrefabs[i].Data.Identifier.Id)
                {
                    list.Add(Instantiate(_listPrefabs[i]));
                }
            }
        }
        return list.ToArray();
    }
    #endregion

    #region ROTINAS

    #endregion
}
