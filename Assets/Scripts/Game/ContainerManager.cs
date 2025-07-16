using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Lionsfall;
using DG.Tweening;

public class ContainerManager : MonoBehaviour
{
    public List<Container> containers = new List<Container>();
    [Header("References")]
    [AssetsOnly]
    public Container mainContainerPrefab;
    public Container tempContainer;
    [Header("Container Generation Settings")]
    public Transform firstContainerPos;
    public Vector3 containerOffset;
    [Header("Container Movement Settings")]
    public float containerMoveDuration = 0.5f;
    public Transform containerMovePoint;
    public Ease movementEase;

    public Container CurrentContainer => containers[0];


    // Create containers using the main container prefab.
    public virtual void GenerateContainers(List<Container> containers)
    {
        this.containers = containers;

        foreach(Container container in containers)
        {
            if (container == null)
            {
                Debug.LogWarning("Container is null, skipping.");
                continue;
            }
            container.transform.SetParent(transform);
            container.transform.localPosition = firstContainerPos.localPosition + containerOffset * containers.IndexOf(container);
            container.gameObject.SetActive(true);
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
            else
            {
                SendNextContainer();
            }
        }
        else
        {
            // Level is failed
            EventManager.TriggerEvent(Const.GameEvents.LEVEL_FAILED, new EventParam());
        }
    }

    private void SendNextContainer()
    {
        containers.RemoveAt(0);
    }
}
