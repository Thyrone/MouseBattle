using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSprite : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMouseEnterEvent;
    [SerializeField] private UnityEvent OnMouseExitEvent;
    [SerializeField] private UnityEvent OnMouseDownEvent;
    [SerializeField] private UnityEvent OnMouseUpEvent;
    private bool AllowInteraction = true;

    private void OnMouseEnter()
    {
        if (!AllowInteraction)
            return;
        Debug.Log("Enter");
        OnMouseEnterEvent.Invoke();
    }

    private void OnMouseExit()
    {
        if (!AllowInteraction)
            return;
        OnMouseExitEvent.Invoke();
    }

    private void OnMouseDown()
    {
        if (!AllowInteraction)
            return;
        OnMouseDownEvent.Invoke();
    }

    private void OnMouseUp()
    {
        if (!AllowInteraction)
            return;
        OnMouseUpEvent.Invoke();
    }


    private void StopInteraction()
    {
        AllowInteraction = false;
    }

}
