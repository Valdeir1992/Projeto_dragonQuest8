/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EntityView : MonoBehaviour, IDestructible
{
    #region PRIVATE VARIABLES
      protected int _currentHp;
      protected int _currentMana;
      protected bool _isDead;
     private Animator _anim;
    [SerializeField] private AbilityId[] _initialOptions;
    [SerializeField] private AbilityId[] _abilities;
    [SerializeField] protected EntityData _data;

    public int CurrentHp { get => _currentHp;}
    public int CurrentMana { get => _currentMana;}
    public bool IsDead { get => _isDead;}
    public EntityData Data { get => _data;}
    public AbilityId[] Abilities { get => _abilities;}
    public Animator Anim { get => _anim;}
    public AbilityId[] InitialOptions { get => _initialOptions;}
    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS
    private void Awake() {
         _anim = GetComponent<Animator>();
         _anim.SetBool("IsMelee",_data.IsMelee);
     } 
    #endregion

    #region OWN METHODS
    public virtual void TakeDamage(int amount){
        _currentHp-=amount;
        _anim.SetTrigger("Damage");
        if(_currentHp <= 0){
            Dead();
        }
    } 

    public virtual void Dead(){
        _currentHp = 0;
        _isDead = true;
        _anim.SetTrigger("IsDead");
    } 
    #endregion

    #region ROTINAS

    #endregion
}

public interface IDestructible{
    void TakeDamage(int amount);
}

public interface IArenaAction{
    public void SetTarget(params EntityView[] target);
    public void SetOperator(EntityView operatorInGame);
    public IEnumerator Apply(BattleController controller);
}
