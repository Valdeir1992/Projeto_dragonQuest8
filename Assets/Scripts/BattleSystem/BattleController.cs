/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    #region  PUBLIC VARIABLES  
    #endregion

    #region PRIVATE VARIABLES  
    private int _currentHero;
    private EntityView[] _enemies;
    private Hero[] _party;
    private Queue<CommandArena> _actions = new Queue<CommandArena>();
    [SerializeField] private Animator _anim; 
    [SerializeField] private Transform[] _heroTransformPositions;
    [SerializeField] private Transform[] _enemiesTransformPositions;

    public EntityView[] Enemies { get => _enemies.Where(enemy =>!enemy.IsDead).ToArray();}
    public EntityView[] Party { get => _party.Where(enemy => !enemy.IsDead).ToArray(); }
    #endregion

    #region EVENTS

    #endregion

    #region UNITY METHODS 
    private void OnEnable()
    {
     MessageSystem.Instance.Register<BattleMessage>(this.MessageHandler);    
    }
    private void Start()
    {
        _enemies = EnemyFactory.Instance.GetEnemies(EncounterSystem.Enemies);
        _party = PartyFactory.Instance.GetParty(PartySystem.CurrentParty);
        SetupParty();
        SetupEnemies();
    }

    private void SetupEnemies()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i].transform.position = _enemiesTransformPositions[i].position;
            _anim.SetInteger("Enemies", _enemies.Length);
        }
    }

    private void SetupParty()
    {
        for (int i = 0; i < _party.Length; i++)
        {
            _party[i].transform.position = _heroTransformPositions[i].position;
            _party[i].transform.rotation = Quaternion.Euler(0, 180, 0);
            _party[i].Configure(new EntityPersistentData() { HP = 3 });
        }
    }

    private void OnDisable()
    {
      if(MessageSystem.Ative){
          MessageSystem.Instance.UnRegister<BattleMessage>(this.MessageHandler);
      }
    }
    #endregion

    #region OWN METHODS
    private bool MessageHandler(Message message)
    {
        BattleMessage bM = message as BattleMessage;
        if(!ReferenceEquals(bM,null))
        {
            if(bM.Action == BattleActions.NEXT)
            {
                _anim.SetTrigger("Next");
            }
            if(bM.Action == BattleActions.FLEE)
            {
                Flee();
            }else if(bM.Action == BattleActions.FIGHT)
            {
                Fight();
            } 
            else if(bM.Action == BattleActions.COMMAND)
            {
                MessageSystem.Instance.Notify(new HUDArenaMessage(HUDArenaActions.HIDDEN_ALL));
                _actions.Enqueue(bM.Command); 
                _currentHero++; 
                if (_currentHero == Party.Length)
                {  
                    Combate();
                }
                else
                {
                    Fight();
                }
                 _anim.SetTrigger("Next");
            }else if(bM.Action == BattleActions.CLEAR)
            {
                Clear();
            }
            return true;
        }
        return false;
    }

    private void Clear()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            Destroy(_enemies[i].gameObject);
        }
        for (int i = 0; i < _party.Length; i++)
        {
            Destroy(_party[i].gameObject);
        }
    }

    private void Flee()
    {
        Clear();
        MessageSystem.Instance.Notify(new EncounterMessage(EncounterActions.END));
    }
    private void Fight(){
        MessageSystem.Instance.Notify(new HUDArenaMessage(HUDArenaActions.SHOW_FIGHT_OPTIONS,_party[_currentHero], Enemies));
        _anim.SetBool("Fight", true);
        _anim.SetTrigger("Next");
    }

    private void Combate(){
        _anim.SetBool("IsReady", true);

        foreach (var item in Enemies)
        {
            CommandArena enemyAction = new CommandArena()
            {
                Operator = item,
                           Targets = new EntityView[] { _party[UnityEngine.Random.Range(0, Party.Length)] },
                           ActionId = item.InitialOptions[0]
            };
            _actions.Enqueue(enemyAction);
        }
        StartCoroutine(Coroutine_Combate());
        MessageSystem.Instance.Notify(new HUDArenaMessage(HUDArenaActions.HIDDEN_ALL));
    }

    public bool EnemiesIsDead()
    {
        foreach (var item in Enemies)
        {
            if(item.IsDead == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool PartyIsDead()
    {
        foreach (var item in Party)
        {
            if (item.IsDead == false)
            {
                return false;
            }
        }
        return true;
    }
     
     
    #endregion

    private IEnumerator Coroutine_Combate()
    {
        yield return new WaitForSeconds(1);

        while(_actions.Count > 0 && !PartyIsDead() && !EnemiesIsDead())
        {
            CommandArena command = _actions.Dequeue();
            if (command.Operator.IsDead) continue;

            IArenaAction action = AbilityFactory.Instance.GetAbility(command.ActionId);
            action.SetOperator(command.Operator);
            action.SetTarget(command.Targets);

            yield return action.Apply(this);
            yield return new WaitForSeconds(GameConfig.TimeReturn + 1);
        }  

        _anim.SetInteger("Enemies", Enemies.Length);
        _anim.SetInteger("Party", Party.Length);
        _anim.SetTrigger("Next");
        _currentHero = 0;
    }
}

public sealed class BattleMessage:Message
{
    public readonly BattleActions Action;
    public readonly CommandArena Command;

    public BattleMessage(BattleActions action)
    {
        Action = action;
    }

    public BattleMessage(BattleActions action, CommandArena command)
    {
        Action = action;
        Command = command;
    }
}
public enum BattleActions
{
    FLEE,
    FIGHT,
    NEXT,
    COMMAND,
    CLEAR
}

public sealed class CommandArena
{
    public EntityView Operator;
    public EntityView[] Targets;
    public AbilityId ActionId;
}

 
 