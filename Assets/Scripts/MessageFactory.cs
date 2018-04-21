using System.Linq;
using UnityEngine;

public class MessageFactory
{
    private readonly MessageManager _messageManager;

    public MessageFactory(MessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    public MessageInstance Create(MessageDescription messageDescription)
    {
        var messageInstance = new MessageInstance();
        messageInstance.Description = messageDescription;
        messageInstance.Message = messageDescription.Message.Text;
        messageInstance.RightAnswer = messageDescription.Message.Options[Random.Range(0, messageDescription.Message.Options.Length)];
        messageInstance.TimeBeforeSending = messageDescription.SendTime;
        messageInstance.TimeForAnswer = messageDescription.AnswerTime;
        if (_messageManager.AngryMessages.Any())
        {
            messageInstance.AngryMessage = _messageManager.AngryMessages[Random.Range(0, _messageManager.AngryMessages.Length)];
        }

        if (messageDescription.NextMessages.Any())
        {
            messageInstance.NextMessage = messageDescription.NextMessages[Random.Range(0, messageDescription.NextMessages.Length)];
        }
        return messageInstance;
    }
}