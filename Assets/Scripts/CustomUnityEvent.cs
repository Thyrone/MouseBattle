using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomUnityEvent : MonoBehaviour
{
    public UnityEvent myEvent;
    public void InvokeEvent()
    {
        myEvent.Invoke();
    }
}
