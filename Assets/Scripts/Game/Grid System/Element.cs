using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Lionsfall;

public class Element : MonoBehaviour, IPointerClickHandler
{
    internal ElementData elementData;

    public Renderer elementRenderer;

    public virtual void Init(ElementData data)
    {
        elementData = data;
        if (elementRenderer != null)
        {
            elementRenderer.material = elementData.elementMaterial;
        }
        name = elementData.elementName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SendElementToFittingContainer();
    }

    public void SendElementToFittingContainer()
    {
        if(elementData.elementName == LevelScene.Instance.containerManager.CurrentContainer.elementName)
        {
            if (LevelScene.Instance.containerManager.CurrentContainer.TryAddElementToFirstFreeSlot(this))
            {
                Debug.Log("Element " + elementData.elementName + " added to the main container.");
            }
            else
            {
                Debug.Log("No free slot available in the container for element: " + elementData.elementName);
            }
        }
        else if(LevelScene.Instance.containerManager.tempContainer.TryAddElementToFirstFreeSlot(this))
        {
            Debug.Log("Element " + elementData.elementName + " added to he temp container.");
        }
    }
}
