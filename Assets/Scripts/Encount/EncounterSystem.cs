/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EncounterSystem : MonoBehaviour
{
    #region PROPERTIES  
    #endregion
    #region PRIVATE VARIABLES 
    [SerializeField] private EntityId[] _ids;
      private int _stepCount;
      private int _nextEncounter;
      public static EntityId[] Enemies;
      [SerializeField] private bool _onBattlefield; 
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void OnEnable()
    {
     MessageSystem.Instance.Register<EncounterMessage>(this.MessageHandler);
    }
    private void Start()
    {
        Enemies = _ids;
        MessageSystem.Instance.Notify(new EncounterMessage(EncounterActions.CONFIGURE_ENCOUNTER,"Arena01","WaveTest"));
    }

    private void Update() {
        if(_stepCount == _nextEncounter){
            InicializeEncounter();
        }
    }

    private void InicializeEncounter()
    {
        MessageSystem.Instance.Notify(new LoadMessage(LoadActions.ASYNC_LOAD));
        Pause();
    }

    private  void Pause()
    {
        IPause[] array = FindObjectsOfType<MonoBehaviour>().OfType<IPause>().ToArray();
        foreach (var pause in array)
        {
            pause.Pause();
        }
    }
    private void Resume()
    {
        IPause[] array = FindObjectsOfType<MonoBehaviour>().OfType<IPause>().ToArray();
        foreach (var pause in array)
        {
            pause.Resume();
        }
    }

    private void OnDisable()
    {
     MessageSystem.Instance.UnRegister<EncounterMessage>(this.MessageHandler);
    }
    #endregion

    #region OWN METHODS
    private bool MessageHandler(Message message){
        EncounterMessage eM = message as EncounterMessage;
        if(!Object.ReferenceEquals(eM,null)){
            if(eM.Action == EncounterActions.STEPS && _onBattlefield)
            {
                _stepCount++;
            }else if(eM.Action == EncounterActions.CONFIGURE_ENCOUNTER)
            {
                MessageSystem.Instance.Notify(new LoadMessage(LoadActions.ASYNC_BUFFERING,eM.Arena));
                _nextEncounter = Random.Range(10,30); 
            }else if(eM.Action == EncounterActions.END)
            {
                _stepCount = 0;
                 MessageSystem.Instance.Notify(new LoadMessage(LoadActions.RETURN));
                Resume();
            }
        }
        return false;
    }
    #endregion

    #region ROTINAS

    #endregion
}

public sealed class EncounterMessage:Message{
    public readonly EncounterActions Action;
    public readonly string Arena;  

    public readonly string WaveId;

    public EncounterMessage(EncounterActions action)
    {
        Action = action;
    }

    public EncounterMessage(EncounterActions action, string arena, string waveid)
    {
        Action = action;
        Arena = arena; 
        WaveId = waveid;
    }
}

public enum EncounterActions{
    STEPS,
    CONFIGURE_ENCOUNTER,
    END
}
