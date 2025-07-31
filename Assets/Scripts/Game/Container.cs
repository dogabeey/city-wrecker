using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Lionsfall;


public class Container : MonoBehaviour
{
    public bool isMainContainer = true; // Indicates if this is the main container or a temporary container.
    [Tooltip("The name of the container, used for identification.")]
    [ValueDropdown("GetAllElementNames")]
    public string elementName;
    public MeshRenderer containerRenderer;
    public List<Slot> slots = new List<Slot>();

    public bool TryAddElementToFirstFreeSlot(Element element)
    {
        if (elementName == element.elementData.elementName || !isMainContainer)
        {
            foreach (Slot slot in slots)
            {
                if (slot.IsFree())
                {
                    // Check if the element is already owned by a slot.
                    Slot currentSlot = element.GetComponentInParent<Slot>();
                    if (currentSlot)
                    {
                        currentSlot.element = null;
                    }

                    slot.AddElement(element, this);
                    Debug.Log("Adding element to " + slot.name);
                    return true;
                }
            }
        }

        return false;
    }

    public static IEnumerable<string> GetAllElementNames()
    {
        GameManager worldManager = GameManager.Instance;
        if (worldManager == null)
        {
            Debug.LogWarning("WorldManager or currentWorld is not initialized.");
            return Enumerable.Empty<string>();
        }
        else
        {
            return GameManager.ElementData.Select(data => data.elementName)
                                            .Where(name => !string.IsNullOrEmpty(name))
                                            .Distinct()
                                            .OrderBy(name => name);
        }
    }

    internal void Init(ElementData elementData)
    {
        if (elementData is JamElementData jamData)
        {
            gameObject.name = elementData.elementName + " container";
            elementName = elementData.elementName;
            containerRenderer.material = jamData.containerMaterial;
        }
    }

    internal bool IsFull()
    {
        // Check if all slots are full
        return slots.All(slot => !slot.IsFree());
    }
}
