using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
        Debug.Log("Clicked on element: " + elementData.elementName);
    }

    public void SendElementToFittingContainer()
    {
        if(elementData.elementName == ContainerManager.Instance.CurrentContainer.elementName)
        {
            if (ContainerManager.Instance.CurrentContainer.TryAddElementToFirstFreeSlot(this))
            {
                Debug.Log("Element " + elementData.elementName + " added to the main container.");
            }
            else
            {
                Debug.Log("No free slot available in the container for element: " + elementData.elementName);
            }
        }
        else if(ContainerManager.Instance.tempContainer.TryAddElementToFirstFreeSlot(this))
        {
            Debug.Log("Element " + elementData.elementName + " added to he temp container.");
        }
    }
}
