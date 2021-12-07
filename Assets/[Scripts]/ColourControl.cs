using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColourControl : MonoBehaviour
{
    public Color color;
    private List<SpriteRenderer> renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        ChangeColour(this.color);
    }

    private void ChangeColour()
    {
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("_Color", color);
        }
    }

    public void ChangeColour(Color color = new Color())
    {
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("_Color", color);
        }
    }

    //public void ChangeColour(Color? color)
    //{
    //    if (color == null)
    //    {
    //        color = this.color;
    //    }

    //    foreach (var renderer in renderers)
    //    {
    //        renderer.material.SetColor("_Color", (Color)color);
    //    }
    //}
}
