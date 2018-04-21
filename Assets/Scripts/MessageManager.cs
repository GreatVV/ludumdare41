using System;
using UnityEngine;

[CreateAssetMenu]
public class MessageManager : ScriptableObject
{
    public MessageDescription FirstMessage;

    public MessageDescription[] Messages;
    public string[] AngryMessages;
}