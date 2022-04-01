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

public class MovimentSystem : MonoBehaviour,IPause
{
    #region  PROPERTIES
    public float Velocity { get; private set;}
    #endregion
    #region PRIVATE VARIABLES
      private bool _pause;
      private CharacterController _character;
      private CharacterMediator _characterMediator;
      private Camera _mainCamera; 
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void Awake()
    {
        _character = GetComponent<CharacterController>();    
        _mainCamera = Camera.main;
    }     private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        Move(direction);    
    } 
    #endregion

    #region OWN METHODS

    public void Configure(CharacterMediator mediator)
    {
        _characterMediator = mediator;
    }
    private void Move(Vector3 input)
    {
        if(_pause) return;
        Vector3 move = _mainCamera.transform.TransformDirection(input);  
        float sqrMagnitude = move.sqrMagnitude;
        if(sqrMagnitude > 0)
        { 
            if(sqrMagnitude > 1){
                move = move.normalized;
                sqrMagnitude = move.sqrMagnitude;
            }
            transform.forward = new Vector3(move.x,0,move.z); 
            _characterMediator.Move(); 
            _character.SimpleMove(new Vector3(move.x,0,move.z) * _characterMediator.Data.MoveSpeed); 
        }else{
            _characterMediator.Idle();
        }
        Velocity = sqrMagnitude;
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Resume()
    {
        _pause = false;
    }
    #endregion

    #region ROTINAS

    #endregion
}
