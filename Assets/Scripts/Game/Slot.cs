using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Lionsfall;



public class Slot : MonoBehaviour
{
    public Element element; // The element currently occupying this slot

    public bool IsFree()
    {
        return element == null; // Check if the slot is free
    }

    public void AddElement(Element newElement, bool isMainContainer)
    {
        if (IsFree())
        {
            element = newElement; // Assign the new element to this slot
            EventManager.TriggerEvent(Const.GameEvents.ELEMENT_ADDED_TO_SLOT, new EventParam(paramObj: newElement.gameObject, paramBool: isMainContainer));
            // TODO: Add element moving logic.
        }
        else
        {
            Debug.LogWarning("Slot is not free. Cannot add element.");
        }
    }
}
