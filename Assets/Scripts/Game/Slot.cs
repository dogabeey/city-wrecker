using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Lionsfall;
using DG.Tweening;



public class Slot : MonoBehaviour
{

    public Element element; // The element currently occupying this slot

    public bool IsFree()
    {
        return element == null; // Check if the slot is free
    }

    public void AddElement(Element newElement, Container ownerContainer)
    {
        if (IsFree())
        {

            element = newElement; // Assign the new element to this slot
            EventManager.TriggerEvent(Const.GameEvents.ELEMENT_ADDED_TO_SLOT, new EventParam(paramObj: newElement.gameObject, paramBool: ownerContainer.isMainContainer));
            // TODO: Add element moving logic.
            element.transform.SetParent(transform); // Set the parent of the element to this slot
            if (!ownerContainer.isMainContainer && ownerContainer.IsFull()) // If temp container, immediately fire full event.
            {
                LevelScene.Instance.containerManager.OnContainerIsFull(ownerContainer);
            }

            element.transform.DOMove(transform.position, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                if (ownerContainer.isMainContainer && ownerContainer.IsFull()) // If the main container, fire the full event after the movement is over.
                {
                    Debug.Log(ownerContainer.name + " is full. Triggering on containerfull method.");
                    LevelScene.Instance.containerManager.OnContainerIsFull(ownerContainer);
                }
            });
        }
        else
        {
            Debug.LogWarning("Slot is not free. Cannot add element.");
        }
    }
}
