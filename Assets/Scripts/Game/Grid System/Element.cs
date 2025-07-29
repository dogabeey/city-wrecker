using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


namespace Lionsfall
{
    public abstract class Element : MonoBehaviour
    {
        internal ElementData elementData;

        public Renderer elementRenderer;

        public abstract void Init(ElementData data);
    }
}