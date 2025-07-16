using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;


public class Container : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();

    public void AddElementToFirstFreeSlot(Element element)
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsFree())
            {
                slot.AddElement(element);
                return;
            }
        }
        Debug.LogWarning("No free slot available to add the element.");
    }
}
