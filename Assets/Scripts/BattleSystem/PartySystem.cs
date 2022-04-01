/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using UnityEngine;

public sealed class PartySystem : MonoBehaviour
{
    public static EntityId[] CurrentParty;

    [SerializeField] private EntityId[] _party;

    private void Awake()
    {
        CurrentParty = _party;
    }
}

 
 