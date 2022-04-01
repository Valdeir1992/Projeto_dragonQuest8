/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 25/03/2022
*****************************************************************************/


using UnityEngine;

[CreateAssetMenu(menuName ="Prototipo/Data/Player")]
public sealed class PlayerData:ScriptableObject
{
    [SerializeField] private float _moveSpeed;

    public float MoveSpeed { get=>_moveSpeed; }
}
