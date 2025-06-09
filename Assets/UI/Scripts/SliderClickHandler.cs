using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderClickHandler : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Animator _animator;

    private bool isOn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button.onClick.AddListener(() => OnButtonClick());
    }
    void OnButtonClick()
    {
        isOn = _animator.GetBool("isOn");
        _animator.ResetTrigger("Selected");
        _animator.SetBool("isOn", !isOn);
        _animator.SetTrigger("Clicked");
    }
    // Update is called once per frame
    void Update()
    {

    }
}