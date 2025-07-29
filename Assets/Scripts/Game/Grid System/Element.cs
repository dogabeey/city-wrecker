using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Serialization;

public abstract class Element : MonoBehaviour
{
    internal ElementData elementData;

    public Renderer elementRenderer;

    public abstract void Init(ElementData data);
}
