using UnityEngine;
using System.Collections;

[AddComponentMenu("Object Note")]
public class ObjectNote : MonoBehaviour
{
    public bool IsNew = true;

    public string Text;
    public bool ShowWhenSelected = true;
    public bool ShowWhenUnselected = false;
    public float Offset = 0.6f;
    public bool Open = false;
    public Color Color = Color.white;
    public int FontSize = 9;
    public bool Bold = false;

    public GUIStyle Style = null;
    public void SetStyle()
    {
        Style = new GUIStyle("Box");
        Texture2D whiteTex = new Texture2D(1, 1);
        whiteTex.SetPixel(0, 0, Color.white);
        Style.normal.background = whiteTex;
        Style.fontSize = FontSize;
        Style.fontStyle = Bold ? FontStyle.Bold : FontStyle.Normal;
        Style.fixedHeight = 0;
        Style.padding = new RectOffset(6, 0, 5, 0);
        Style.wordWrap = true;
        Style.alignment = TextAnchor.UpperLeft;
    }
}
