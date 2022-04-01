/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject, IArenaAction
{
    private Vector3 _startPosition;
    private Vector3 _finalPosition;
    protected EntityView _operator;
    protected EntityView[] _targets;
    [SerializeField] private bool _melee;
    [SerializeField] private bool _selfTarget;
    [SerializeField] private AbilityId _identifier;
    [SerializeField] private string _name;
    [SerializeField] private int _manaCost;
    public string Name { get => _name;}
    public int ManaCost { get => _manaCost;}
    public AbilityId Identifier { get => _identifier;}

    public void SetTarget(params EntityView[] target){
        _targets = target;
    }
    public void SetOperator(EntityView operatorInGame){
        _operator = operatorInGame;
    }
    public IEnumerator Apply(BattleController controller)
    {
        if (_melee)
        {
            yield return Approach(controller);
            yield return Action(controller);
            yield return Return(controller);
        }
        else
        {
            yield return Action(controller);
        }
    }
    public abstract IEnumerator Action(BattleController controller);
    public virtual IEnumerator Return(BattleController controller) 
    { 
        float timeElapsed = 0; 

        while (true)
        {
            timeElapsed += Time.deltaTime;
            Vector3 currentPosition = Vector3.Slerp(_finalPosition, _startPosition, timeElapsed / GameConfig.TimeReturn);
            _operator.transform.position = currentPosition;
            if ((timeElapsed / GameConfig.TimeReturn) >= 1)
            {
                break;
            }
            yield return null;
        }
    }

    public virtual IEnumerator Approach(BattleController controller)
    {
        EntityView target = _targets[0];
        float timeElapsed = 0;
        _startPosition = _operator.transform.position;
        _finalPosition = target.transform.position + new Vector3(target.transform.forward.x, 0, target.transform.forward.z) * GameConfig.DistanceMeleeCorrection;


        while (true)
        {
            timeElapsed += Time.deltaTime;
            Vector3 currentPosition = Vector3.Slerp(_startPosition, _finalPosition, timeElapsed / GameConfig.TimeAprox);
            _operator.transform.position = currentPosition;
            if ((timeElapsed / GameConfig.TimeAprox) >= 1)
            {
                break;
            }
            yield return null;
        }
    }
}
