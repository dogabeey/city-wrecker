using UnityEngine;

public class Element : MonoBehaviour
{
    internal ElementData elementData;

    public Renderer elementRenderer;

    public virtual void Init(ElementData data)
    {
        elementData = data;
        if (elementRenderer != null)
        {
            elementRenderer.material.color = elementData.elementColor;
        }
        name = elementData.elementName;
    }
}
