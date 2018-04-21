using System;

[Serializable]
public class MessageInstance
{
    public float TimeBeforeSending;
    public float TimeForAnswer;
    public string Message;
    public string AngryMessage;
    public string RightAnswer;
    public MessageDescription NextMessage;
    public MessageDescription Description;
}