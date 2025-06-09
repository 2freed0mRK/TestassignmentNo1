using UnityEngine;
using UnityEngine.Events;
using static NavigationBarButton;
// using WindowController for both windows, because they only used to handle open and close states
public class WindowController : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private GameStateController _gameStateController;

    public GameStateController GameStateController { get { return _gameStateController; } set { _gameStateController = value;} }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }    // Update is called once per frame
    public void OnCloseClick()
    {
        _animator.SetTrigger("Close");
        if (_gameStateController != null)
        {
            _gameStateController.ShowHUD();
        }
    }
}
