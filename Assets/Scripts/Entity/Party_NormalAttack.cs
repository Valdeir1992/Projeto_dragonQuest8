/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototipo/Data/Abilities/Party/Physic/Attack")]
public sealed class Party_NormalAttack : Ability
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
        MessageSystem.Instance.Notify(new CameraMessage(CameraActions.SETUP, 2));
        return base.Approach(controller);
    }

    public override IEnumerator Return(BattleController controller)
    {
        MessageSystem.Instance.Notify(new CameraMessage(CameraActions.SETUP, 0), 0.25f);
        return base.Return(controller);
    }
}

public enum CameraActions{
    SHOW_ENEMIES,
    SETUP,
    SHOW_PARTY
}

public sealed class GameConfig
{
    public static float DistanceMeleeCorrection = 1f;
    public static float TimeAprox = 0.95f;
    public static float TimeMeleeAtack = 1.5f;
    public static float TimeReturn = TimeAprox / 2;
}
