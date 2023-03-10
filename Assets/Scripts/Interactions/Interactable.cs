using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_OnInteract;

    [SerializeField]
    private bool m_IsOnce = false;
    
    private bool canBeUsed = true;

    public string InteractionText;

    public void Interact()
    {
        if (!canBeUsed)
            return;

        if (m_IsOnce)
            canBeUsed = false;

        m_OnInteract.Invoke();
    }

    public bool GetUsable()
    {
        return canBeUsed;
    }

    public void SetUsable(bool truth)
    {
        canBeUsed = truth;
    }

    public void SetText(string text)
    {
        InteractionText = text;
    }

    public UnityEvent GetInteractEvent()
    {
        return m_OnInteract;
    }
}
