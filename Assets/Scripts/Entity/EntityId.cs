/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using UnityEngine;

[CreateAssetMenu(menuName ="Prototipo/Data/Entity/Id")]
public sealed class EntityId:ScriptableObject{
    [SerializeField] private string _id;

    public string Id { get => _id;}
}
