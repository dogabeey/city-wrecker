using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ElementDataSettings", menuName = "Scriptable Objects/ElementDataSettings")]
public class ElementDataSettings : SerializedScriptableObject
{
    public List<ElementData> elementDataList = new List<ElementData>();
}
