/******************************************************************************
* Copyright (c) 2022 Just Fun
* All rights reserved.
* Programador: Valdeir Antonio do Nascimento
* Data: 
*****************************************************************************/


public sealed class CameraMessage:Message
{
    public readonly CameraActions Action;
    public readonly int Camera;

    public CameraMessage(CameraActions action)
    {
        Action = action;
    }

    public CameraMessage(CameraActions action, int camera) : this(action)
    {
        Camera = camera;
    }
}
