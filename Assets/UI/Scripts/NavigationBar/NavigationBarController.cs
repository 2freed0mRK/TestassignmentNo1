using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationBarController : UIBehaviour
{
    [SerializeField] List<NavigationBarButton> _buttons;
    [SerializeField] HorizontalLayoutGroup _horizontalGroup;
    [SerializeField] SelectorAnimation _selector;
    [SerializeField] Animator _animator;

    private int _buttonIndexToFollow = -1; // -1 nothing is selected
    private RectTransform _hGroupRect;

    // for this iteration I have decided that size would be calculated from Horizontal Group Size \ by buttons.count +1
    private float _sizeNormal;
    private float _sizeSelected;

    void CalculateButtonsSize()
    {
        if (_hGroupRect == null)
        {
            _hGroupRect = _horizontalGroup.GetComponent<RectTransform>();
        }
        //calculating end positon of the selection
        _sizeNormal = (_hGroupRect.rect.size.x - (_horizontalGroup.padding.right + _horizontalGroup.padding.left + (_buttons.Count - 1) * _horizontalGroup.spacing)) / (_buttons.Count + 1);
        _sizeSelected = _sizeNormal * 2f;
        _selector.rectTransform.sizeDelta = new Vector2(_sizeSelected, _selector.rectTransform.sizeDelta.y);

    }
    public void HideNavigationBar()
    {
        _animator.SetTrigger("Hide");
    }
    public void ShowNavigationBar()
    {
        _animator.SetTrigger("Show");
    }

    public void ContentActivated(int buttonId)
    {
        SelectButton(buttonId);
    }
    public void Closed()
    {
        if (_buttonIndexToFollow == -1)
        {
            return;
        }
        UnselectAllButtons();
        _buttonIndexToFollow = -1;
    }
    void ButtonsSubscribeEvents()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int buttonIndex = i;
            _buttons[i].Button.onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }
    void ButtonsSetSize()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].RectSize.sizeDelta = new Vector2(_sizeNormal, _buttons[i].RectSize.sizeDelta.y);
            _buttons[i].SelectedCoordinate = CalculateSelectedCooridnates(i);
            _buttons[i].SetAnimationValue(_sizeNormal);
        }
    }

    float CalculateSelectedCooridnates(int index)
    {
        float selectedCoordinate = _horizontalGroup.padding.right + _sizeSelected * 0.5f + (index) * _horizontalGroup.spacing + (index) * _sizeNormal;
        return selectedCoordinate;
    }
    void OnButtonClick(int index)
    {
        if (_buttons[index].isLocked)
        {
            //playing locked animation
            _buttons[index].PlayLockedAnimation();
            return;
        }

        if (index == _buttonIndexToFollow)
        {
            // checking if it the same button clicked if so hiding it
            _buttons[_buttonIndexToFollow].SetAnimationTriggerOff();
            _selector.WriteNewCoordinates(new Vector2(_buttons[index].SelectedCoordinate, 0f));
            _selector.PlayVerticalDisappearance();
            _buttonIndexToFollow = -1;
            return;
        }

        if (_buttonIndexToFollow == -1)
        {
            // checking is there any button selected, -1 means none
            SelectButton(index);
            return;
        }
        if (_buttonIndexToFollow != -1 && _buttonIndexToFollow != index)
        {
            // playing hiding animation for previous button
            UnselectButton(_buttonIndexToFollow);
        }
        _buttonIndexToFollow = index;
        _buttons[index].AnimationSetSelected();
        _selector.WriteNewCoordinates(new Vector2 (_buttons[index].SelectedCoordinate, 0f));
        _selector.PlayHorizontalMovement();
    }

    private void SelectButton(int index)
    {
        _buttons[index].AnimationSetSelected();
        _selector.WriteNewCoordinates(new Vector2(_buttons[index].SelectedCoordinate, 0));
        _selector.PlayVerticalAppearance();
        _buttonIndexToFollow = index;
    }
    private void UnselectButton(int index)
    {
        _buttons[index].SetAnimationTriggerOff();
    }
    private void UnselectAllButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetAnimationTriggerOff();
        }
    }
    #region Unity Lifetime calls

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateButtonsSize();
        ButtonsSubscribeEvents();
        SetDirty();
    }

    protected override void OnTransformParentChanged()
    {
        CalculateButtonsSize();
        ButtonsSubscribeEvents();
        SetDirty();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        CalculateButtonsSize();
        ButtonsSetSize();
        //ButtonsInitialization();
        SetDirty();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        SetDirty();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        CalculateButtonsSize();
        ButtonsSubscribeEvents();
        SetDirty();
    }

    #endregion

    protected void SetDirty()
    {
        if (!IsActive())
            return;
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        SetDirty();
    }

#endif

}
