using System;
using UnityEngine;

[CreateAssetMenu]
public class MessageDescription : ScriptableObject
{
    public float SendTime;
    public float AnswerTime;
    public Message Message;
    public MessageDescription[] NextMessages;
}