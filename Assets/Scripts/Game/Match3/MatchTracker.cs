using System.Collections.Generic;
using UnityEngine;

namespace Lionsfall
{
    public class MatchTracker : MonoBehaviour
    {
        internal ThreeMatchElement firstElement;
        internal ThreeMatchElement secondElement;

        private void OnEnable()
        {
            EventManager.StartListening(Const.GameEvents.ELEMENT_PICKED, OnElementPicked);
            EventManager.StartListening(Const.GameEvents.ELEMENTS_SELECTED, OnElementsSelected);
        }
        private void OnDisable()
        {
            EventManager.StopListening(Const.GameEvents.ELEMENT_PICKED, OnElementPicked);
            EventManager.StopListening(Const.GameEvents.ELEMENTS_SELECTED, OnElementsSelected);
        }
        public void OnElementPicked(EventParam e)
        {
            if(e.paramElement != null)
            {
                if(e.paramInt == 1)
                {
                    firstElement = e.paramElement as ThreeMatchElement;
                }
                else if (e.paramInt == 2)
                {
                    secondElement = e.paramElement as ThreeMatchElement;
                }
                else
                {
                    Debug.LogError("Invalid parameter int value received for element picked event: " + e.paramInt);
                }
            }
            else
            {
                Debug.LogError("Element picked event received with null element.");
            }
        }
        public void OnElementsSelected(EventParam e)
        {
            // Logic to remove all picked elements if they're 3 or more and of same type

            firstElement = null;
            secondElement = null;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}