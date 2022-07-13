using UnityEngine;

[System.Serializable]
public class LiquidPrefab
{
    public Color Color
    {
        get
        {
            switch (type)
            {
                case ColorType.red:
                    return new Color(1, 0.2962264f, 0.2962264f, 1);
                    break;
                case ColorType.green:
                    return new Color(0.2912424f, 0.9528302f, 0.2962264f, 1);
                    break;
                case ColorType.blue:
                    return new Color(0.2901961f, 0.8753911f, 0.9529412f, 1);
                    break;
                case ColorType.purple:
                    return new Color(1, 0.6169811f, 0.9802822f, 1);
                    break;
                case ColorType.yellow:
                    return new Color(1, 0.8812259f, 0.4377358f, 1);
                    break;
                case ColorType.black:
                    return new Color(0.254717f, 0.254717f, 0.254717f, 1);
                    break;
                case ColorType.orange:
                    return new Color(0.9529412f, 0.5343035f, 0.2901961f, 1);
                    break;
                default:
                    return Color.red;
                    break;
            }
        }
        set
        {
            _color = value;

            
        }
    }
    private Color _color;
    public enum ColorType { red, green, blue, purple, yellow, black, orange }
    public ColorType type;
}
