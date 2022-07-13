using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidTop : MonoBehaviour
{
    public bool IsFilled;
    public Transform Target;
    private SpriteRenderer _sprite;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private ParticleSystem _particleBubles;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Target)
        {
            transform.position = Target.position;
        }

        if (IsFilled)
        {
            if (transform.localScale.y < 1)
            {
                transform.localScale += new Vector3(0, 1, 0) * _scaleSpeed * Time.deltaTime;
            }
            if (transform.localScale.y > 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
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
            }
        }
    }

    public void SetColor(Color color)
    {
        _sprite.color = color;
        _particleBubles.startColor = color;
    }

    public void EnableParticles(bool value)
    {
        _particleBubles.gameObject.SetActive(value);
    }

    public void Fill()
    {
        IsFilled = true;
        transform.localScale = new Vector3(1, 1, 1);
    }
}
