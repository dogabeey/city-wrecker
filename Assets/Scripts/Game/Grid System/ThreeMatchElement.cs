using UnityEngine;
using UnityEngine.Events;


namespace Lionsfall
{
    public abstract class ThreeMatchElement : Element
    {
        public override void Init(ElementData data)
        {
            if(elementRenderer is SpriteRenderer spriteRenderer && elementData is MatchThreeElementData matchThreeData)
            {
                spriteRenderer.sprite = matchThreeData.elementSprite;
            }
            name = elementData.elementName;
        }
    }
}