using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;



public class Slot : MonoBehaviour
{
    public Element element; // The element currently occupying this slot

    public bool IsFree()
    {
        return element == null; // Check if the slot is free
    }

    public void AddElement(Element newElement)
    {
        if (IsFree())
        {
            element = newElement; // Assign the new element to this slot
            // TODO: Add element moving logic.
        }
        else
        {
            Debug.LogWarning("Slot is not free. Cannot add element.");
        }
    }
}
