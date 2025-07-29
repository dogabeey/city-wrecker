using System.Collections.Generic;
using UnityEngine;

namespace Lionsfall
{
    public class MatchTracker : MonoBehaviour
    {
        internal List<ThreeMatchElement> pickedElements = new List<ThreeMatchElement>();

        private void OnEnable()
        {
            EventManager.StartListening(Const.GameEvents.ELEMENT_PICKED, OnElementPicked);
        }
        private void OnDisable()
        {
            EventManager.StopListening(Const.GameEvents.ELEMENT_PICKED, OnElementPicked);
        }
        public void OnElementPicked(EventParam e)
        {
            if(e.paramElement != null)
            {
                pickedElements.Add(e.paramElement as ThreeMatchElement);
            }
            else
            {
                Debug.LogError("Element picked event received with null element.");
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            pickedElements = new List<ThreeMatchElement>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}