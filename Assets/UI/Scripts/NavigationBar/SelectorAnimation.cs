
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SelectorAnimation : UIBehaviour
{
    public RectTransform rectTransform ;
    [SerializeField] public Vector2 animationProgress;
    [SerializeField] public float minMaxOvershoot = 40f;
    [SerializeField] public float overshootDelta = 1.1f;
    [SerializeField] Animator animator;
    public Vector2 oldCoordinate { get; set; } = new Vector2(0f,0f);
    public Vector2 newCoordinate { get; set; } = new Vector2(0f, 0f);
    public void PlayHorizontalMovement()
    {
        animator.SetTrigger("Jump");
    }
    public void PlayVerticalDisappearance()
    {
        animator.SetTrigger("Hide");
    }
    public void PlayVerticalAppearance()
    {
        animator.SetTrigger("Show");
    }
    public void Animation()
    {
        var xOvershoot = newCoordinate.x + (newCoordinate.x * overshootDelta * Mathf.Sign(newCoordinate.x - oldCoordinate.x));
        rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(oldCoordinate.x, xOvershoot, animationProgress.x), (Mathf.Lerp(oldCoordinate.y, newCoordinate.y, animationProgress.y)));
    }

    public void WriteNewCoordinates(Vector2 vec)
    {
        newCoordinate = vec;
        oldCoordinate = rectTransform.anchoredPosition;
    }

    #region Unity Lifetime calls

    protected override void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        base.OnEnable();
        SetDirty();
    }

    protected override void OnTransformParentChanged()
    {
        SetDirty();
    }

    protected override void OnRectTransformDimensionsChange()
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
        SetDirty();
        Animation();
        Debug.Log("Update");
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
