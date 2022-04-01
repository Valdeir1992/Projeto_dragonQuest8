/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PartyFactory : GenericSingleton<PartyFactory>
{
    [SerializeField] private List<Hero> _listHeroes;
    public Hero[] GetParty(EntityId[] currentParty)
    {
        List<Hero> list = new List<Hero>();

        foreach (var item in currentParty)
        {
            for (int i = 0; i < _listHeroes.Count; i++)
            {
                if (item.Id == _listHeroes[i].Data.Identifier.Id)
                {
                    list.Add(Instantiate(_listHeroes[i]));
                }
            }
        }
        return list.ToArray();
    }
}

 
 