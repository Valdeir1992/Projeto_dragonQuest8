using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MessageSystem : GenericSingleton<MessageSystem>
{
    private Dictionary<Type, List<Func<Message, bool>>> _dicListeners = new Dictionary<Type, List<Func<Message, bool>>>();

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool Register<U> (Func<Message,bool> messageHandler) where U:Message
    {
        Type messageType = typeof(U);
        if (_dicListeners.ContainsKey(messageType))
        {
            List<Func<Message, bool>> list = _dicListeners[messageType];

            foreach (var item in list)
            {
                if(item == messageHandler)
                {
                    return false;
                }
            }
            list.Add(messageHandler);
            return true;
        }
        else
        {
            List<Func<Message, bool>> list = new List<Func<Message, bool>>();
            list.Add(messageHandler);
            _dicListeners.Add(messageType, list);
            return true;
        } 
    }

    public bool Notify(Message message)
    {
        Type messageType = message.GetType();
        if (_dicListeners.ContainsKey(messageType))
        {
            foreach (var item in _dicListeners[messageType])
            {
                if (item.Invoke(message))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool Notify(Message message , float time)
    {
        StartCoroutine(Coroutine_SendMessage(message, time));
        return true;
    }

    private IEnumerator Coroutine_SendMessage(Message message, float time)
    {
        yield return new WaitForSeconds(time);
        Notify(message);
    }

    public bool UnRegister<U>(Func<Message,bool> messageHandler)
    {
        Type messageType = typeof(U);
        if (_dicListeners.ContainsKey(messageType))
        {
            List<Func<Message, bool>> list = _dicListeners[messageType];

            if (list.Contains(messageHandler))
            {
                list.Remove(messageHandler);
                return true;
            } 
        }
        return false;
    }
}
