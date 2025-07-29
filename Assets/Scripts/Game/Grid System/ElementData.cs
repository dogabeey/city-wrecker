using UnityEngine;

[System.Serializable]
public abstract class ElementData
{
    public string elementName;
    public Color elementColor = Color.white;
}

[System.Serializable]
public class JamElementData : ElementData
{
    public Material elementMaterial;
    public Material containerMaterial;
}
[System.Serializable]
public class MatchThreeElementData : ElementData
{
    public Sprite elementSprite;
}