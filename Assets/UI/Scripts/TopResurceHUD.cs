using System.Collections.Generic;
using UnityEngine;

public class TopResurceHUD : MonoBehaviour
{
    [SerializeField] List<Animator> _animators;
    [SerializeField] GameObject _settingsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void HideHUD()
    {
        foreach (var animator in _animators)
        {
            animator.SetTrigger("Hide");
            _settingsButton.SetActive(false);
        }
    }
    public void ShowHUD()
    {
        foreach (var animator in _animators)
        {
            animator.SetTrigger("Show");
            _settingsButton.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
