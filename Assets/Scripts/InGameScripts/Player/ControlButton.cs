using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ControlButton : Button
{
    public Action OnPointerClickHandler; 
    public Action OnPointerUpHandler; 

    public override void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpHandler?.Invoke();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        OnPointerClickHandler?.Invoke();
    }

}
