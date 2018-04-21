using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class MessagingTests
{
    [Test]
    public void CreateMessage()
    {
        var nextMessage = ScriptableObject.CreateInstance<MessageDescription>();
        nextMessage.Message = new Message();

        var message1 = ScriptableObject.CreateInstance<MessageDescription>();
        message1.SendTime = 2;
        message1.AnswerTime = 3f;
        message1.Message = new Message()
        {
            Text = "Smoothies",
            Options = new[] { "Lopata" },
        };
        message1.NextMessages = new[] { nextMessage };

        var messageFactory = new MessageFactory(ScriptableObject.CreateInstance<MessageManager>());
        var messageInstance = messageFactory.Create(message1);

        Assert.AreEqual(2f, messageInstance.TimeBeforeSending);
        Assert.AreEqual(3f, messageInstance.TimeForAnswer);
        Assert.AreEqual("Lopata", messageInstance.RightAnswer);
        Assert.AreEqual(nextMessage, messageInstance.NextMessage);
    }
}
