using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TempChanger : HorizontalLayoutGroup
{
    [SerializeField] RectTransform m_RectTransform;
    [SerializeField] float m_AnimationNormalized;
    [SerializeField] float m_AnimationExtraDelta = 0.2f;

    public float NormalSize { get; set; } = 170f;
    //public float SelectedSize { get; set; } = 340f;

    void LateUpdate()
    {
        //m_RectTransform.sizeDelta = new Vector2 (Mathf.Lerp(NormalSize, SelectedSize, m_AnimationNormalized), 0f);
        //SetDirty();
    }
    protected override void OnDidApplyAnimationProperties()
    {
        m_RectTransform.sizeDelta = new Vector2(Mathf.Lerp(NormalSize, NormalSize * 2f, m_AnimationNormalized), 0f);

        SetDirty();
    }
    protected void SetDirty()
    {
        if (!IsActive())
            return;
        LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
    }
}
