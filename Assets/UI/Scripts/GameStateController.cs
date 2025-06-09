using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _coinsButton;
    [SerializeField] WindowController _settings;
    [SerializeField] WindowController _rewards;
    [SerializeField] TopResurceHUD _topResurceHUD;
    [SerializeField] NavigationBarController _navigationBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _settingsButton.onClick.AddListener(() => OnSettingsButtonClick());
        _coinsButton.onClick.AddListener(() => OnRewardsButtonClick());
        _settings.gameObject.SetActive(false);
        _rewards.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void OnSettingsButtonClick()
    {
        _settings.GameStateController = this;
        _settings.gameObject.SetActive(true);
        _topResurceHUD.HideHUD();
        _navigationBar.HideNavigationBar();
    }

    public void ShowHUD()
    {
        _topResurceHUD.ShowHUD();
        _navigationBar.ShowNavigationBar();
    }
    void OnRewardsButtonClick()
    {
        _rewards.GameStateController = this;
        _rewards.gameObject.SetActive(true);
        _topResurceHUD.HideHUD();
        _navigationBar.HideNavigationBar();
    }
}
