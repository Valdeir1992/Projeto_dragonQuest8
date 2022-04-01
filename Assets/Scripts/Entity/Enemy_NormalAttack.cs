/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototipo/Data/Abilities/Enemy/Physic/Attack")]
public sealed class Enemy_NormalAttack : Ability
{
    public override IEnumerator Action(BattleController controller)
    {
        EntityView target = _targets[0];
        if (!target.IsDead)
        {
            _operator.Anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            target.TakeDamage(2);
            yield return new WaitForSeconds(GameConfig.TimeMeleeAtack);
        }
    }
    public override IEnumerator Approach(BattleController controller)
    {
        MessageSystem.Instance.Notify(new CameraMessage(CameraActions.SETUP, 3));
        return base.Approach(controller);
    }

    public override IEnumerator Return(BattleController controller)
    {
        MessageSystem.Instance.Notify(new CameraMessage(CameraActions.SETUP, 0), 0.25f);
        return base.Return(controller);
    }
}
