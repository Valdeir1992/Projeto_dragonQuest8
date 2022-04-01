/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


public sealed class Enemy:EntityView{
    private void Start() {
        _currentHp = _data.MaxHP;
        _currentMana = _data.MaxMan; 
    }
}
