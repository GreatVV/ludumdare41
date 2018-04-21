using System;
using UnityEngine;
using UnityEngine.UI;

public class HateBar : MonoBehaviour
{
    public int MaxHate;

    public Slider HateMeter;

    private int _currentHate;

    public int CurrentHate
    {
        get { return _currentHate; }
        set
        {
            _currentHate = value;
            HateChanged?.Invoke();
            HateMeter.normalizedValue = _currentHate /(float) MaxHate;
            if (_currentHate >= MaxHate)
            {
                MaxHateReached?.Invoke();
            }
        }
    }

    public event Action MaxHateReached;
    public event Action HateChanged;

}