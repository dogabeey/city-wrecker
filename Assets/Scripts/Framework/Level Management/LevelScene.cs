using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


namespace Lionsfall
{
    public class LevelScene : MonoBehaviour
    {
        public string levelName;
        [InlineEditor]
        public LevelEditor levelData; // This is the data for the current level, which can be used to access the level's elements, containers, and other properties.

        [HideInInspector] public bool isWin;
        [HideInInspector] public bool isLose;
        [HideInInspector] public bool isEnded;
        [HideInInspector] public ContainerManager containerManager;

        public static LevelScene Instance;

        private void Start()
        {
            Instance = this;
            EventManager.TriggerEvent(Const.GameEvents.LEVEL_STARTED, new EventParam());

            containerManager = FindAnyObjectByType<ContainerManager>();
        }


        private void Update()
        {
            if (isEnded) return;
            if (isWin) // PUT YOUR WIN CONDITIONS HERE
            {
                isEnded = true;
                EventParam param = new EventParam();
                EventManager.TriggerEvent(Const.GameEvents.LEVEL_COMPLETED, param); // You can trigger this event anywhere and It will trigger On Win actions in the inspector, along with regular Level Completion events. This one also passes the time it took to win the level.
            }
            if (isLose) // PUT YOUR LOSE CONDITIONS HERE
            {
                isEnded = true;
                EventParam param = new EventParam();
                EventManager.TriggerEvent(Const.GameEvents.LEVEL_FAILED, param); // You can trigger this event anywhere and It will trigger It will trigger On Lose actions in the inspector, along with regular Level Failure events. This one also passes the time it took to lose the level.
            }


        }

        public static IEnumerable<string> GetAllElementNames()
        {
            GameManager worldManager = GameManager.Instance;
            if (worldManager == null)
            {
                Debug.LogWarning("WorldManager or currentWorld is not initialized.");
                return Enumerable.Empty<string>();
            }
            else
            {
                return GameManager.ElementData.Select(data => data.elementName)
                                                .Where(name => !string.IsNullOrEmpty(name))
                                                .Distinct()
                                                .OrderBy(name => name);
            }
        }
    }
}