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
        foreach (Slot slot in slots)
        {
            if (slot.IsFree())
            {
                slot.AddElement(element, isMainContainer);
                return true;
            }
        }

        return false;
    }

    public static IEnumerable<string> GetAllElementNames()
    {
        WorldManager worldManager = WorldManager.Instance;
        if (worldManager == null)
        {
            Debug.LogWarning("WorldManager or currentWorld is not initialized.");
            return Enumerable.Empty<string>();
        }
        else
        {
            return worldManager.elementData.Select(data => data.elementName)
                                            .Where(name => !string.IsNullOrEmpty(name))
                                            .Distinct()
                                            .OrderBy(name => name);
        }
    }

    internal void Init(ElementData elementData)
    {
        elementName = elementData.elementName;
        containerRenderer.material = elementData.containerMaterial;
    }
}
