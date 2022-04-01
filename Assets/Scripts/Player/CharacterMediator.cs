/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 25/03/2022
*****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMediator : MonoBehaviour
{
    #region  PROPERTIES
    public PlayerData Data { get=>_data; }    
    #endregion
    #region PRIVATE VARIABLE
    private float _count;
    private MovimentSystem _movimentSystem; 
    private AnimationSystem _animSytem;
    [SerializeField] private PlayerData _data;
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void Awake() 
    {
        _movimentSystem = GetComponent<MovimentSystem>();
        _movimentSystem.Configure(this);

        _animSytem = GetComponent<AnimationSystem>();
        _animSytem.Configure(this);
    } 
    private void Update() {
        if(_count <= 0.2f){
            _count += Time.deltaTime;
        }
    }
    #endregion

    #region OWN METHODS 
    public void Move(){
        _animSytem.Move(_movimentSystem.Velocity);
        if(_count >= 0.2f){
            MessageSystem.Instance.Notify(new EncounterMessage(EncounterActions.STEPS));
            _count = 0;
        }
    }
    public void Idle(){
        _animSytem.Idle();
    }
    #endregion

    #region ROTINAS

    #endregion
}

public interface IPause{
    void Pause();

    void Resume();
} 