using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuViewController : MonoBehaviour {
    private Dictionary<string, GameObject> _views;

    private void Awake() {
        _views = new Dictionary<string, GameObject>();
        
        foreach (Transform child in transform) {
            UIDocument childDocument = child.GetComponent<UIDocument>();
            if (childDocument != null) {
                _views.Add(child.gameObject.name,child.gameObject);
            }
        }
        
        ActivateView(_views.First().Key);
    }

    public void ActivateView(string viewName) {
        foreach (var pair in _views) {
            pair.Value.SetActive(false);
        }
        
        _views[viewName].SetActive(true);
    }
}
