/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


using UnityEngine;

public sealed class CameraSystem:MonoBehaviour
{
    [SerializeField] private Animator _anim;


    private void OnEnable() {
        MessageSystem.Instance.Register<CameraMessage>(this.MessageHandler);
    }

    private void OnDisable(){
        if(MessageSystem.Ative){
            MessageSystem.Instance.UnRegister<CameraMessage>(this.MessageHandler);
        } 
    }

    private bool MessageHandler(Message message)
    {
        CameraMessage cM = message as CameraMessage;
        if(!Object.ReferenceEquals(cM,null)){
            if(cM.Action == CameraActions.SETUP){
                _anim.SetInteger("Camera",cM.Camera);
            }
            else if(cM.Action == CameraActions.SHOW_ENEMIES){
                _anim.SetInteger("Camera",0);
            }
            else if(cM.Action == CameraActions.SHOW_PARTY){
                _anim.SetInteger("Camera",1);
            }
        }
        return false;
    }
}
