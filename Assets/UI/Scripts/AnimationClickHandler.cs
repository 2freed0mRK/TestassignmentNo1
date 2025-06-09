using UnityEngine;
using UnityEngine.UI;

public class AnimationClickHandler : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button.onClick.AddListener(() => OnButtonClick());
    }
    void OnButtonClick()
    {
        _animator.ResetTrigger("Selected");
        _animator.SetTrigger("Clicked");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
