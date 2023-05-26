using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMenuController : MonoBehaviour {
    private UIDocument _doc;
    private CameraDirector _director;
    private MenuViewController _viewController;
    private VisualElement _buttonContainer;


    [SerializeField] private VisualTreeAsset _cameraButtonTemplate;

    private void Awake() {
        _doc = GetComponent<UIDocument>();
        _director = FindObjectOfType<CameraDirector>();
        _viewController = FindObjectOfType<MenuViewController>();
        
        
    }

    private void OnEnable() {
        _buttonContainer = _doc.rootVisualElement.Q<VisualElement>("ButtonContainer");

        List<String> names = new List<string>();
        
        var cameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var camera in cameras) {
            string newName = camera.gameObject.name;
            var button = InstantiateButtonWithName("TemplateButton",newName);
            button.clicked += () => CameraButtonOnClicked(newName);
            var label = button.Q<Label>();
            label.text = newName;
            names.Add(newName);
        }

        _buttonContainer.Q<Button>(names.First()).Focus();
        
        var backButton = InstantiateButtonWithName("TemplateLowButton", "Back");
        backButton.clicked += BackButtonOnClicked;
        var backLabel = backButton.Q<Label>();
        backLabel.text = "Back";
    }

    private Button InstantiateButtonWithName(string templateName, string buttonName) {
        var template = _cameraButtonTemplate.CloneTree();
        var templateButton = template.Q<Button>(templateName);
        templateButton.name = buttonName;
        _buttonContainer.Add(templateButton);
        return _buttonContainer.Q<Button>(buttonName);
    }

    private void CameraButtonOnClicked(string cameraID) {
        _director.SwapToCamera(cameraID);
    }

    private void BackButtonOnClicked() {
        _viewController.ActivateView("MainMenu");
    }
}
