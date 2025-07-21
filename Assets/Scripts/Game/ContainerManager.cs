using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Lionsfall;
using DG.Tweening;
using System.Collections;

public class ContainerManager : SingletonComponent<ContainerManager>
{
    public List<Container> containers = new List<Container>();
    [Header("References")]
    [AssetsOnly]
    public Container mainContainerPrefab;
    public Container tempContainer;
    [Header("Container Generation Settings")]
    public Transform containersParent;
    public Transform firstContainerPos;
    public Vector3 containerOffset;
    [Header("Container Movement Settings")]
    public float containerMoveDuration = 0.5f;
    public Transform containerMovePoint;
    public Ease movementEase;

    public Container CurrentContainer => containers[0];


    // Create containers using the main container prefab.
    public virtual void GenerateContainers(List<string> elementNameList)
    {
        foreach (string elementName in elementNameList)
        {
            int index = elementNameList.IndexOf(elementName);
            ElementData elementData = WorldManager.Instance.GetElementDataByName(elementName);

            Container container = Instantiate(mainContainerPrefab, containersParent);
            container.transform.position = firstContainerPos.position + containerOffset * index;
            container.Init(elementData);

            containers.Add(container);
        }

        foreach(Container c in containers)
        {
            if (c == null)
            {
                Debug.LogWarning("Container is null, skipping.");
                continue;
            }
            c.transform.SetParent(transform);
            c.transform.localPosition = firstContainerPos.localPosition + containerOffset * containers.IndexOf(c);
            c.gameObject.SetActive(true);
        }
    }

    public virtual void OnContainerIsFull(Container container)
    {
        if(container.isMainContainer)
        {
            if(containers.Count == 1)
            {
                EventManager.TriggerEvent(Const.GameEvents.LEVEL_COMPLETED, new EventParam());
            }
            StartCoroutine(SendNextContainer());
        }
        else
        {
            // Level is failed
            EventManager.TriggerEvent(Const.GameEvents.LEVEL_FAILED, new EventParam());
        }
    }
    public virtual void OnNextContainerArrived(Container container)
    {
        // For each element that is currently on temp container, try to add it to the first free slot of the current container.
        foreach (Element element in tempContainer.slots.Select(slot => slot.element).Where(element => element))
        {
            if (!container.TryAddElementToFirstFreeSlot(element))
            {
                Debug.LogWarning($"Container {container.elementName} is full, cannot add element {element.name}.");
            }
        }
    }

    // Send the first container away to the designated move point and move the next container to the first position, then the second to the second position, and so on.
    private IEnumerator SendNextContainer()
    {
        Container sentAwayContainer = containers[0];
        sentAwayContainer.transform.DOMove(containerMovePoint.position, containerMoveDuration).SetEase(movementEase);

        containers.RemoveAt(0);

        foreach(Container container in containers)
        {
            container.transform.DOMove(-containerOffset, containerMoveDuration).SetRelative().SetEase(movementEase);
        }

        yield return new WaitForSeconds(containerMoveDuration);

        if(containers.Count > 0)
            OnNextContainerArrived(CurrentContainer);
    }
}
