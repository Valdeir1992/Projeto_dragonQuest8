/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections.Generic;
using UnityEngine;

public sealed class AbilityFactory : GenericSingleton<AbilityFactory>
{
    [SerializeField] private List<Ability> _listAbilities;

    public Ability GetAbility(AbilityId identifier)
    {
        Ability ability = null;

        foreach (var item in _listAbilities)
        {
            if(item.Identifier.Id == identifier.Id)
            {
                ability = item;
                break;
            }
        }

        return ability;
    }
}

 
 