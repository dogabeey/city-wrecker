using UnityEngine.Events;


namespace Lionsfall
{
    public abstract class ThreeMatchElement : Element
    {
        public UnityEvent onElementMatched;

        public override void Init(ElementData data)
        {
            elementData = data;
            if (elementRenderer != null)
            {
                elementRenderer.material = elementData.elementMaterial;
            }
            name = elementData.elementName;
        }
        public void Match()
        {
            // Logic for matching the element
            onElementMatched?.Invoke();
        }

        public virtual void OnElementMatched()
        {
            EventManager.TriggerEvent(Const.GameEvents.ELEMENT_MATCHED);
            onElementMatched?.Invoke();
        }
    }
}