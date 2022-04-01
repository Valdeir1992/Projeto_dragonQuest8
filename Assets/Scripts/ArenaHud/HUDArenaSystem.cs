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

public class HUDArenaSystem : MonoBehaviour
{
    #region PRIVATE VARIABLES
      private EntityView[] _enemies;
      private AbilityId _selectedAbility;
      private EntityView _hero;
      [SerializeField] private CanvasGroup _initialOptions;
      [SerializeField] private CanvasGroup _fightOptions;
      [SerializeField] private CanvasGroup _enemiesOptions;
      [SerializeField] private Button _btnFight;
      [SerializeField] private Button _btnFlee;
      [SerializeField] private Button _btnAttack;
      [SerializeField] private Button _btnAbilities;
      [SerializeField] private RectTransform _enemiesPanel;
    [SerializeField] private Button _prefabEnemySelectionButton;
    #endregion

    #region EVENTS
    
    #endregion

    #region UNITY METHODS
    private void Awake() {
        _btnFlee.onClick.AddListener(()=>{
            MessageSystem.Instance.Notify(new BattleMessage(BattleActions.FLEE),0.3f);
        });
        _btnFight.onClick.AddListener(()=>{
            MessageSystem.Instance.Notify(new BattleMessage(BattleActions.FIGHT),0.3f);
        });
        _btnAttack.onClick.AddListener(() =>
        {
            _selectedAbility = _hero.InitialOptions[0];
            ShowEnemiesOptions();
        });
    }
    private void OnEnable()
    {
     MessageSystem.Instance.Register<HUDArenaMessage>(this.MessageHandler);
    }
    private void Start()
    {
     
    }

    private void OnDisable()
    {
     if(MessageSystem.Ative){
         MessageSystem.Instance.UnRegister<HUDArenaMessage>(this.MessageHandler);
     }
    }
    #endregion

    #region OWN METHODS

    private bool MessageHandler(Message message)
    {
        HUDArenaMessage hAM = message as HUDArenaMessage;
        if(!ReferenceEquals(hAM,null))
        {
            if(hAM.Action == HUDArenaActions.SHOW_FIGHT_OPTIONS){
                _hero = hAM.Hero;
                _enemies = hAM.Enemies;
                ShowFightCanva();
            }else if(hAM.Action == HUDArenaActions.SHOW_INITIAL_OPTIONS)
            { 
                ShowInitialOptionCanva();
            }else if(hAM.Action == HUDArenaActions.SHOW_ENEMIES_OPTIONS)
            {
                ShowEnemiesOptions();
            }else if(hAM.Action == HUDArenaActions.HIDDEN_ALL)
            {
                HiddenCanva(_initialOptions);
                HiddenCanva(_enemiesOptions);
                HiddenCanva(_fightOptions);
            }
        }
        return false;
    }
    private void ShowEnemiesOptions()
    {
        foreach (var item in _enemies)
        {
            Button enemy = Instantiate(_prefabEnemySelectionButton,_enemiesPanel);
            enemy.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = item.name;
            enemy.onClick.AddListener(() =>
            {
                MessageSystem.Instance.Notify(new BattleMessage(BattleActions.COMMAND, new CommandArena()
                {
                    Operator = _hero,
                    Targets = new EntityView[] { item },
                    ActionId = _selectedAbility
                }));

                MessageSystem.Instance.Notify(new BattleMessage(BattleActions.NEXT),0.25f);
            }); 
        }

        _enemiesPanel.ForceUpdateRectTransforms();
        ShowCanva(_enemiesOptions);

        MessageSystem.Instance.Notify(new BattleMessage(BattleActions.NEXT));
    }
    private void ShowInitialOptionCanva(){
        ShowCanva(_initialOptions);
        HiddenCanva(_fightOptions);
        HiddenCanva(_enemiesOptions);
    }
    private void ShowFightCanva(){
        ShowCanva(_fightOptions);
        HiddenCanva(_initialOptions);
        
    }
    private void ShowCanva(CanvasGroup canva){
        canva.alpha = 1;
        canva.blocksRaycasts = true;
        canva.interactable = true;
    }
    private void HiddenCanva(CanvasGroup canva){
        canva.alpha = 0;
        canva.blocksRaycasts = false;
        canva.interactable = false;

        for (int i = 0; i < _enemiesPanel.childCount; i++)
        {
            Destroy(_enemiesPanel.GetChild(i).gameObject);
        }
    }
    #endregion

    #region ROTINAS

    #endregion
}

public sealed class HUDArenaMessage:Message
{
    public readonly HUDArenaActions Action;
    public readonly EntityView Hero;
    public readonly EntityView[] Enemies;

    public HUDArenaMessage(HUDArenaActions action)
    {
        Action = action;
    }

    public HUDArenaMessage(HUDArenaActions action, EntityView hero,EntityView[] enemies)
    {
        Action = action;
        Hero = hero;
        Enemies = enemies;
    } 
}
public enum HUDArenaActions{
    SHOW_FIGHT_OPTIONS,
    SHOW_INITIAL_OPTIONS,
    SHOW_ENEMIES_OPTIONS,
    HIDDEN_ALL
}
