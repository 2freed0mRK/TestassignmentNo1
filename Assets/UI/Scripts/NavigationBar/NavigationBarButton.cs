using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NavigationBarButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Image _lock;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SizeChanger _sizeAnimationProxy;
    [SerializeField] private ButtonsType _buttonType;

    public ButtonsType buttonType { get { return _buttonType; } set { _buttonType = value; SetIcon(); } }
    public Button Button { get { return _button;}}
    [field: SerializeField] public RectTransform RectSize { get; set; }
    public float SelectedCoordinate { get; set; }
    public bool isLocked = false;
    public enum ButtonsType
    {
        Home,
        Map,
        Shop,
        Disabled,
        None
    }
    void Awake()
    {
        if (RectSize == null)
        {
            RectSize = GetComponent<RectTransform>();
        }
        SetIcon();
    }
    void Start()
    {
        if (RectSize == null)
        {
            RectSize = GetComponent<RectTransform>();
        }
        SetIcon();
    }

    public void AnimationSetSelected()
    {
        _animator.SetBool("IsActive", true);
    }
    public void SetAnimationTriggerOff()
    {
        _animator.SetBool("IsActive", false);
        _animator.ResetTrigger("Selected");
        _animator.SetTrigger("Normal");
    }
    public void SetAnimationValue(float value)
    {
        _sizeAnimationProxy.NormalSize = value;
    }
    public void PlayLockedAnimation()
    {
        _animator.SetTrigger("PlayLocked");
    }

    private void OnValidate()
    {
        SetIcon();
    }
    private void SetIcon()
    {
        switch (_buttonType)
        {
            case ButtonsType.Home:
                _image.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);
                _image.overrideSprite = _sprites[0];
                isLocked = false;
                break;
            case ButtonsType.Map:
                _image.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);
                _image.overrideSprite = _sprites[1];
                isLocked = false;
                break;
            case ButtonsType.Shop:
                _image.gameObject.SetActive(true);
                _lock.gameObject.SetActive(false);
                _image.overrideSprite = _sprites[2];
                isLocked = false;
                break;
            case ButtonsType.Disabled:
                _image.gameObject.SetActive(false);
                _lock.gameObject.SetActive(true);
                isLocked = true;
                _animator.SetBool("IsLocked", isLocked);
                break;

            case ButtonsType.None:
                _image.gameObject.SetActive(false);
                _lock.gameObject.SetActive(false);
                break;
        }
    }
}
