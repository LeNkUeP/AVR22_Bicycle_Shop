using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SignalRColorBase<TEntityController, TArgs> : SignalRUnityFeatureBase<TEntityController, TArgs>
    where TEntityController : SignalREntityController<TArgs>
{
    /// <summary>
    /// Farbe parsen
    /// </summary>
    /// <param name="value"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    protected bool TryParseColor(string value, out Color color)
    {
        if (ColorsDict.TryGetValue(value.ToLowerInvariant(), out color))
        {
            return true;
        }
        else if (value[0] == '#')
        {
            byte r = byte.Parse(value.Substring(1, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(value.Substring(3, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(value.Substring(5, 2), NumberStyles.HexNumber);
            color = new Color32(r, g, b, 255);
            return true;
        }
        else
        {
            color = Color.white;
            return false;
        }
    }

    public void HandleColor(string colorString) => Color = TryParseColor(colorString, Color);

    protected Color TryParseColor(string value, Color defaultColor) => TryParseColor(value, out var color) ? color : defaultColor;

    public Renderer coloredObject;

    /// <summary>
    /// Gibt die Farbe des Würfels an.
    /// </summary>
    public Color Color
    {
        get => _Color;
        set
        {
            _Color = value;
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                coloredObject.material.color = value;
                //gameObject.scene.GetRootGameObjects()
                //.First(go => go.name == "Cube")
                //.GetComponent<Renderer>().material.color = value;
            });
        }
    }
    private Color _Color = Color.white;

    /// <summary>
    /// Bekannte Farbnamen
    /// </summary>
    private static readonly Dictionary<string, Color> ColorsDict = new Dictionary<string, Color>
    {
        [nameof(Color.red)] = Color.red,
        [nameof(Color.black)] = Color.black,
        [nameof(Color.blue)] = Color.blue,
        [nameof(Color.clear)] = Color.clear,
        [nameof(Color.cyan)] = Color.cyan,
        [nameof(Color.gray)] = Color.gray,
        [nameof(Color.green)] = Color.green,
        [nameof(Color.grey)] = Color.grey,
        [nameof(Color.magenta)] = Color.magenta,
        [nameof(Color.red)] = Color.red,
        [nameof(Color.white)] = Color.white,
        [nameof(Color.yellow)] = Color.yellow,
    };
}