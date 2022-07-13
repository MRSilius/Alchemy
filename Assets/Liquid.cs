using UnityEngine;

public class Liquid : MonoBehaviour
{
    public bool IsFull;
    public bool IsFilled;
    private SpriteRenderer _sprite;
    public SpriteRenderer _bottomSprite;
    public SpriteRenderer _topSprite;
    [SerializeField] private float _scaleSpeed;
    public Transform Top;

    public Color Color;

    public Flask Flask;
    public int ID;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsFilled)
        {
            if (transform.localScale.y < 1)
            {
                transform.localScale += new Vector3(0, 1, 0) * _scaleSpeed * Time.deltaTime;
            }
            if (transform.localScale.y >= 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                if (!IsFull)
                {
                    Flask.FullPart(ID);
                    IsFull = true;
                }
            }
        }
        else
        {
            if (transform.localScale.y > 0)
            {
                transform.localScale -= new Vector3(0, 1, 0) * _scaleSpeed * Time.deltaTime;
            }
            else
            {
                transform.localScale = new Vector3(1, 0, 1);
                if (IsFull)
                {
                    Flask.EmptyPart(ID);
                    IsFull = false;
                }
            }
        }
    }

    public void SetColor(Color color)
    {
        Color = color;
        _sprite.color = color;
        if (_bottomSprite)
        {
            _bottomSprite.color = color;
        }
        if (_topSprite)
        {
            _topSprite.color = color;
        }
    }

    public void Fill(Color color)
    {
        IsFilled = true;
        SetColor(color);

        transform.localScale = new Vector3(1, 1, 1);
    }
}
