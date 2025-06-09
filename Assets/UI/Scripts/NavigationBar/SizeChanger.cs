using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class SizeChanger : UIBehaviour
{
    [SerializeField] RectTransform m_RectTransform;
    [SerializeField] float m_AnimationNormalized;
    [SerializeField] float m_AnimationExtraDelta = 0.2f;

    public float NormalSize {  get; set; } = 170f;
    //public float SelectedSize { get; set; } = 340f;

    #region Unity Lifetime calls

    protected override void OnEnable()
    {
        base.OnEnable();
        SetDirty();
    }

    protected override void OnTransformParentChanged()
    {
        SetDirty();
    }

    protected override void OnDisable()
    {
        SetDirty();
        base.OnDisable();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        m_RectTransform.sizeDelta = new Vector2(Mathf.Lerp(NormalSize, NormalSize * 2f * m_AnimationExtraDelta, m_AnimationNormalized), 0f);
        SetDirty();
    }

    protected override void OnBeforeTransformParentChanged()
    {
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
