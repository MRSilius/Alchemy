using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] private List<Liquid> _liquidParts;
    [SerializeField] private LiquidTop _topLiquid;

    [SerializeField] private FlaskManager _manager;

    private Vector3 _defaultPosition;
    private Vector3 _chosenPosition;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _chosenEffect;
    [SerializeField] private Transform _shadow;

    [SerializeField] private Transform _overflowTargetPosition;
    [SerializeField] private Flask _overflowTarget;
    [SerializeField] private bool isOverflows;
    private float _distanceToOverflow;
    public Transform OverflowPosition;
    public SpriteRenderer _overflowSprite;
    [SerializeField] private List<Liquid> _liquidToOverflow;
    public bool InOverflowProcess;
    public bool Win;

    private AudioManager _audio;

    public bool IsChosen
    {
        get
        {
            return _isChosen;
        }
        set
        {
            _isChosen = value;

            if (value)
            {
                _chosenEffect.SetActive(true);
            }
            else
            {
                _chosenEffect.SetActive(false);
            }
        }
    }
    private bool _isChosen;

    public bool Empty;
    public bool FilledWithOneColor;

    private void Start()
    {
        for (int i = 0; i < _liquidParts.Count; i++)
        {
            _liquidParts[i].ID = i;
            _liquidParts[i].Flask = this;
        }
        _manager = FindObjectOfType<FlaskManager>();
        _audio = FindObjectOfType<AudioManager>();

        _defaultPosition = transform.position;
        _chosenPosition = transform.position + new Vector3(0, 0.4f, 0);
        CheckFlaskState();
        _shadow.localScale = new Vector3(0, 0, 0);
        _shadow.parent = transform.parent;
    }

    private void OnMouseDown()
    {
        if (InOverflowProcess == false && !Win)
        {
            _manager.ChoseFlask(this);
        }
    }

    private void Update()
    {
        if (_isChosen)
        {
            if (_shadow.localScale.y < 0.65f)
            {
                _shadow.localScale += new Vector3(1, 1, 0) * Time.deltaTime * 8;
            }
            else
            {
                _shadow.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }
        else
        {
            if (_shadow.localScale.y > 0)
            {
                _shadow.localScale -= new Vector3(1, 1, 0) * Time.deltaTime * 8;
            }
            else
            {
                _shadow.localScale = new Vector3(0, 0, 0);
            }
        }

        if (isOverflows)
        {
            InOverflowProcess = true;

            _distanceToOverflow = Vector2.Distance(transform.position, _overflowTargetPosition.position);

            transform.position = Vector3.MoveTowards(transform.position, _overflowTargetPosition.position, Time.deltaTime * _speed * 7);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _overflowTargetPosition.rotation, Time.deltaTime * _speed * 100);

            if (_distanceToOverflow < 0.05f && transform.rotation == _overflowTargetPosition.rotation)
            {

                //Проверка вылито ли все
                if (_liquidToOverflow.Count > 0)
                {
                    if (_liquidToOverflow[0].IsFilled)
                    {
                        _audio.PlayLiquidSound();
                        Overflow();
                    }

                    if (_liquidToOverflow[0].IsFull == false)
                    {
                        _liquidToOverflow.Remove(_liquidToOverflow[0]);
                    }
                }
                else
                {
                    _audio.StopLiquidSound();
                    isOverflows = false;
                    _overflowSprite.gameObject.SetActive(false);
                    _overflowTargetPosition = null;
                }
            }
        }
        else
        {
            float distToDefault = Vector2.Distance(transform.position, _defaultPosition);

            if (_overflowTarget)
            {
                if (distToDefault > 0.05 || transform.rotation != Quaternion.Euler(0, 0, 0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, _defaultPosition, Time.deltaTime * _speed * 7);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * _speed * 100);
                }
                else
                {
                    _overflowTarget.InOverflowProcess = false;
                    _overflowTarget = null;
                    InOverflowProcess = false;
                }
            }
            else
            {
                if (_isChosen)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _chosenPosition, Time.deltaTime * _speed);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, _defaultPosition, Time.deltaTime * _speed);
                }
            }
        }
    }

    public void AddLiquid(Color color)
    {
        for (int i = 0; i < _liquidParts.Count; i++)
        {
            if (_liquidParts[i].IsFilled == false)
            {
                _liquidParts[i].IsFilled = true;
                SetTopTarget(_liquidParts[i].Top);

                _liquidParts[i].SetColor(color);
                _topLiquid.SetColor(color);

                if (i == 0)
                {
                    _topLiquid.IsFilled = true;
                    _topLiquid.EnableParticles(true);
                }
                break;
            }
        }
        CheckFlaskState();
    }

    public void SetLiquid(int index, Color color)
    {
        SetTopTarget(_liquidParts[index].Top);
        _topLiquid.SetColor(color);
        _liquidParts[index].Fill(color);

        if (index == 0)
        {
            _topLiquid.Fill();
            _topLiquid.EnableParticles(true);
        }
    }

    public void OverflowTo(Flask targetFlask, int emptyParts)
    {
        _overflowTargetPosition = targetFlask.OverflowPosition;
        _overflowTarget = targetFlask;
        _overflowTarget.InOverflowProcess = true;
        _distanceToOverflow = Vector2.Distance(transform.position, _overflowTargetPosition.position);
        _liquidToOverflow = HasFullParts(emptyParts);
        _overflowSprite.color = _liquidToOverflow[0].Color;
        isOverflows = true;
    }

    public void Overflow()
    {
        _overflowSprite.gameObject.SetActive(true);
        _overflowTarget.AddLiquid(_liquidToOverflow[0].Color);
        RemoveLiquid();
    }

    public void RemoveLiquid()
    {
        for (int i = _liquidParts.Count - 1; i > -1; i--)
        {
            if (_liquidParts[i].IsFilled == true)
            {
                _liquidParts[i].IsFilled = false;
                SetTopTarget(_liquidParts[i].Top);

                if (i == 0)
                {
                    _topLiquid.IsFilled = false;
                    _topLiquid.EnableParticles(false);
                }
                break;
            }
        }
        CheckFlaskState();
    }

    public int EmptyParts()
    {
        int parts = 0;
        for (int i = 0; i < _liquidParts.Count; i++)
        {
            if (_liquidParts[i].IsFilled == false)
            {
                parts++;
            }
        }
        return parts;
    }

    public bool IsEmpty()
    {
        for (int i = _liquidParts.Count - 1; i > -1; i--)
        {
            if (_liquidParts[i].IsFilled == true)
            {
                return false;
            }
        }
        return true;
    }

    public bool HasEmptyPart()
    {
        for (int i = 0; i < _liquidParts.Count; i++)
        {
            if (_liquidParts[i].IsFilled == false)
            {
                return true;
            }
        }
        return false;
    }

    public Color TopColor()
    {
        for (int i = _liquidParts.Count - 1; i > -1; i--)
        {
            if (_liquidParts[i].IsFilled == true)
            {
                return _liquidParts[i].Color;
            }
        }

        return Color.black;
    }

    public List<Liquid> HasFullParts(int canHold)
    {
        var parts = new List<Liquid>();

        int firstFullIndex = 0;

        for (int i = _liquidParts.Count - 1; i > -1; i--)
        {
            if (_liquidParts[i].IsFilled == true)
            {
                firstFullIndex = i;
                break;
            }
        }

        Color firstColor = Color.black;

        for (int i = firstFullIndex; i > firstFullIndex - canHold; i--)
        {
            if (i < 0)
            {
                break;
            }
            if (_liquidParts[i].IsFilled == true)
            {
                if (i == firstFullIndex)
                {
                    firstColor = _liquidParts[i].Color;
                    parts.Add(_liquidParts[i]);
                }
                else
                {
                    if (_liquidParts[i].Color == firstColor)
                    {
                        parts.Add(_liquidParts[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        return parts;
    }


    public void FullPart(int id)
    {
    }

    public void EmptyPart(int id)
    {
        if (id != 0)
        {
            _topLiquid.SetColor(_liquidParts[id - 1].Color);
        }
    }

    public void SetTopTarget(Transform target)
    {
        _topLiquid.Target = target;
    }

    private void CheckFlaskState()
    {
        Color firstColor = Color.clear;

        for (int i = 0; i < _liquidParts.Count; i++)
        {
            if (i == 0)
            {
                if (_liquidParts[i].IsFilled == false)
                {
                    Empty = true;
                    FilledWithOneColor = false;
                    break;
                }
                else
                {
                    Empty = false;
                }

                firstColor = _liquidParts[i].Color;
            }
            else
            {
                if (_liquidParts[i].Color != firstColor)
                {
                    FilledWithOneColor = false;
                    break;
                }
                if (i == 3)
                {
                    if (_liquidParts[i].Color == firstColor)
                    {
                        FilledWithOneColor = true;
                    }
                }
            }
        }

        _manager.CheckFlasksState(false);
        //print("Empty - " + Empty + " FilledWithOneColor - " + FilledWithOneColor + "  Color - " + firstColor);
    }
}
