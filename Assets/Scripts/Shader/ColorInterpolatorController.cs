using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInterpolatorController : MonoBehaviour
{
    [SerializeField] public GameObject obj1;
    [SerializeField] public GameObject obj2;

    private Color _color1;
    private Color _color2;

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        _color1 = SetColor(obj1);
        _color2 = SetColor(obj2);
        rend.material.SetColor("_Color1", _color1);
        rend.material.SetColor("_Color2", _color2);
    }
    private Color SetColor(GameObject obj)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        return rend.material.color;
    }
}
