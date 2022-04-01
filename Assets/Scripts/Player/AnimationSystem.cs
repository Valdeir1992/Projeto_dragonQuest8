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

public class AnimationSystem : MonoBehaviour,IPause
{
    #region PROPERTIES
    #endregion
    #region PRIVATE VARIABLES 
    private CharacterMediator _characterMediator; 
    [SerializeField] private Animator _anim;
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS 
    
    #endregion

    #region OWN METHODS
    public void Configure(CharacterMediator mediator){
        _characterMediator = mediator;
    } 

    public void Move(float velocity){
        if(_anim.GetBool("Idle")){
            _anim.SetBool("Idle",false);
        }
        _anim.SetFloat("Velocity",velocity);
    }
    public void Idle(){
        if(_anim.GetBool("Idle")) return;
        _anim.SetBool("Idle",true);
    }

    public void Pause()
    {
        _anim.speed = 0;
    }

    public void Resume()
    {
        _anim.speed = 1;
    }
    #endregion

    #region ROTINAS

    #endregion
}
