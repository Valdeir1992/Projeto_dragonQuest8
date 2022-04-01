/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


public sealed class Hero:EntityView
{  
    public void Configure(EntityPersistentData data){
        _currentHp = data.HP;
    }
}

public class EntityPersistentData
{
    public int HP;
}
