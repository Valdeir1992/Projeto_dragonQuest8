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
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadSystem : MonoBehaviour
{
    private AsyncOperation _load;
    private string _currentArena;

    #region PRIVATE VARIABLES
      
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void OnEnable()
    {
     MessageSystem.Instance.Register<LoadMessage>(this.MessageHandler);
    } 
    private void OnDisable()
    {
     MessageSystem.Instance.UnRegister<LoadMessage>(this.MessageHandler);
    }
    #endregion

    #region OWN METHODS
    private bool MessageHandler(Message message){
        LoadMessage lM = message as LoadMessage;

        if(!Object.ReferenceEquals(lM,null)){
            if(lM.Action == LoadActions.ASYNC_BUFFERING){
                _currentArena = lM.Arena;
                StartCoroutine(Coroutine_LoadAsync(_currentArena));
            }else if(lM.Action == LoadActions.ASYNC_LOAD){
                _load.allowSceneActivation = true;
            }else if(lM.Action == LoadActions.RETURN){
                Scene currentScene = SceneManager.GetSceneByName(_currentArena);
                SceneManager.UnloadSceneAsync(currentScene);
                StartCoroutine(Coroutine_LoadAsync(_currentArena));
            }
        }
        return false;
    }
    #endregion

    #region ROTINAS
    private IEnumerator Coroutine_LoadAsync(string arena){
        _load = SceneManager.LoadSceneAsync(arena,LoadSceneMode.Additive);
        _load.allowSceneActivation = false;
        while(true){
            if(_load.progress > 0.8f){
                break;
            }
        }
        yield break;
    }
    #endregion
}

public sealed class LoadMessage:Message{
    public readonly LoadActions Action;
    public readonly string Arena;

    public LoadMessage(LoadActions action)
    {
        Action = action;
    }

    public LoadMessage(LoadActions action, string arena)
    {
        Action = action;
        Arena = arena;
    }
}

public enum LoadActions{
    ASYNC_BUFFERING,
    ASYNC_LOAD,
    RETURN
}
