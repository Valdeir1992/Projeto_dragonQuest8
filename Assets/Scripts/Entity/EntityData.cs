/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using UnityEngine;

[CreateAssetMenu(menuName ="Prototipo/Data/Entity/States")]
public sealed class EntityData:ScriptableObject{
    [SerializeField] private bool _isMelee;
    [SerializeField] private int _speed;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxMan;
    [SerializeField] private EntityId _identifier;

    public int Speed { get => _speed;}
    public int MaxHP { get => _maxHP;}
    public int MaxMan { get => _maxMan;}
    public EntityId Identifier { get => _identifier;}
    public bool IsMelee { get => _isMelee;}
}
