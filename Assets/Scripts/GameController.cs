using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public MessageManager MessageManager;
    public MessageFactory MessageFactory;
    public MobilePhone MobilePhone;
    public event Action<MessageInstance> TooLate;
    public Slider TimeToSentBar;
    public HateBar HateBar;
    public MessageInstance CurrentMessage;
    public float Percent = 0.2f;

    void Start()
    {
        MessageFactory = new MessageFactory(MessageManager);
        TooLate += OnTooLate;
        CurrentMessage = MessageFactory.Create(MessageManager.FirstMessage);
        MobilePhone.MessageSent += OnMessageSent;
    }

    private void OnMessageSent(string answer)
    {
        var levenshteinDistance = LevenshteinDistance(answer, CurrentMessage.RightAnswer);
        var errorLimit = CurrentMessage.RightAnswer.Length * Percent;
        Debug.Log($"Answer analyze. Distance: {levenshteinDistance} Limit: {errorLimit}");
        if (levenshteinDistance < errorLimit)
        {
            Debug.Log("Right answer");
            if (CurrentMessage.NextMessage != null)
            {
                CurrentMessage = MessageFactory.Create(CurrentMessage.NextMessage);
            }
        }
        else
        {
            Debug.Log("Wrong answer");
            HateBar.CurrentHate++;
            var angryMessage = CurrentMessage.AngryMessage;
            MobilePhone.SentHerMessage(angryMessage);
            if (CurrentMessage.NextMessage != null)
            {
                CurrentMessage = MessageFactory.Create(CurrentMessage.NextMessage);
            }
        }
    }

    public static int LevenshteinDistance(string string1,string string2)
    {
        if (string1==null) throw new ArgumentNullException("string1");
        if (string2==null) throw new ArgumentNullException("string2");
        int diff;
        int [,] m = new int[string1.Length+1,string2.Length+1];

        for (int i=0;i<=string1.Length;i++) { m[i,0]=i; }
        for (int j=0;j<=string2.Length;j++) { m[0,j]=j; }

        for (int i=1;i<=string1.Length;i++) {
            for (int j=1;j<=string2.Length;j++)
            {
                if (!char.IsLetter(string1[i - 1]) || !char.IsLetter(string2[j - 1]))
                {
                    diff = 0;
                }
                else
                {
                    diff = (char.ToLower(string1[i - 1]) == char.ToLower(string2[j - 1])) ? 0 : 1;
                }

                m[i,j]=Math.Min(Math.Min(m[i-1,j]+1,m[i,j-1]+1),m[i-1,j-1]+diff);
            }
        }
        return m[string1.Length,string2.Length];
    }

    private void OnTooLate(MessageInstance obj)
    {
        Debug.Log("too late message: "+obj.Message);
        HateBar.CurrentHate++;
        if (obj.NextMessage != null)
        {
            CurrentMessage = MessageFactory.Create(obj.NextMessage);
        }
    }

    void Update()
    {
        var delta = Time.deltaTime;


        if (CurrentMessage.TimeBeforeSending > 0)
        {
            CurrentMessage.TimeBeforeSending -= delta;

            if (CurrentMessage.TimeBeforeSending <= 0)
            {
                MobilePhone.SentHerMessage(CurrentMessage.Message);
                MobilePhone.SetRightAnswer(CurrentMessage.RightAnswer);
                TimeToSentBar.gameObject.SetActive(true);
            }
            else
            {
                TimeToSentBar.gameObject.SetActive(false);
            }
        }

        if (CurrentMessage.TimeBeforeSending <= 0)
        {
            if (CurrentMessage.TimeForAnswer > 0)
            {
                TimeToSentBar.gameObject.SetActive(true);
                CurrentMessage.TimeForAnswer -= delta;
                TimeToSentBar.normalizedValue = CurrentMessage.TimeForAnswer / CurrentMessage.Description.AnswerTime;
                if (CurrentMessage.TimeForAnswer <= 0)
                {
                    TooLate?.Invoke(CurrentMessage);
                }
            }
        }

    }
}