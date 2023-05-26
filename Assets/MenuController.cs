using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour {
    private UIDocument _doc;
    private Button _playButton;
    private Button _settingsButton;
    private Button _exitButton;
    private Button _muteButton;
    private MenuViewController _viewController;

    [SerializeField] private VisualTreeAsset _settingsButtonTemplate;
    private VisualElement _settingsButtons;

    private VisualElement _buttonContainer;

    [Header("Mute Button")] 
    [SerializeField] private Sprite _mutedSprite;
    [SerializeField] private Sprite _unmutedSprite;
    private bool _muted;

    private void Awake() {
        _doc = GetComponent<UIDocument>();
        _viewController = FindObjectOfType<MenuViewController>();
    }

    private void OnEnable() {
        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _settingsButton = _doc.rootVisualElement.Q<Button>("SettingsButton");
        _exitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _muteButton = _doc.rootVisualElement.Q<Button>("MuteButton");
        _buttonContainer = _doc.rootVisualElement.Q<VisualElement>("ButtonContainer");
        
        _settingsButtons = _settingsButtonTemplate.CloneTree();
        var backButton = _settingsButtons.Q<Button>("BackButton");
        backButton.clicked += BackButtonOnClicked;
        
        _playButton.clicked += PlayButtonOnClicked;
        _exitButton.clicked += ExitButtonOnClicked;
        _muteButton.clicked += MuteButtonOnClicked;
        _settingsButton.clicked += SettingsButtonOnClicked;
        
        _playButton.Focus();
    }

    private void OnDisable() {
        _playButton.clicked -= PlayButtonOnClicked;
        _exitButton.clicked -= ExitButtonOnClicked;
        _muteButton.clicked -= MuteButtonOnClicked;
        _settingsButton.clicked -= SettingsButtonOnClicked;
    }

    private void BackButtonOnClicked() {
        _buttonContainer.Clear();
        _buttonContainer.Add(_playButton);
        _buttonContainer.Add(_settingsButton);
        _buttonContainer.Add(_exitButton);
        _playButton.Focus();
    }

    private void SettingsButtonOnClicked() {
        _buttonContainer.Clear();
        _buttonContainer.Add(_settingsButtons);
        _settingsButtons.Children().First().Focus();
    }
    
    private void PlayButtonOnClicked() {
        _viewController.ActivateView("CameraMenu");
    }

    private void ExitButtonOnClicked() {
        Application.Quit();
    }

    private void MuteButtonOnClicked() {
        _muted = !_muted;
        var bg = _muteButton.style.backgroundImage;
        bg.value = Background.FromSprite(_muted ? _mutedSprite : _unmutedSprite);
        _muteButton.style.backgroundImage = bg;

        AudioListener.volume = _muted ? 0 : 1;
    }
}
