using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerDownHandler
{
    private Action onClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void OnClick(Action action)
    {
        onClick = action;
    }
}
