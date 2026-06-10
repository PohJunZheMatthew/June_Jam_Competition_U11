using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class UiScript : MonoBehaviour
{
    private UIDocument _uiDocument;
    private Button _myButton;
    private Button _backSettingsButton;

    [Header("UXML Source Assets")]
    [SerializeField] private VisualTreeAsset mainMenuAsset;
    [SerializeField] private VisualTreeAsset settingsMenuAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.parent != null)
        {
            _uiDocument = transform.parent.GetComponent<UIDocument>();
        }
        else
        {
            // Fallback: If there's no parent, check the exact same GameObject
            _uiDocument = GetComponent<UIDocument>();
        }
        
        if (_uiDocument == null)
        {
            Debug.LogError("UiScript Error: No UIDocument component found on the parent GameObject!");
            return;
        }

        VisualElement root = _uiDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("UiScript Error: rootVisualElement is null. Is a UXML asset assigned to the UIDocument?");
            return;
        }

        // Try finding the button directly
        _myButton = root.Q<Button>("Settings");

        if (_myButton != null)
        {
            _myButton.clicked += showSettings;
            Debug.Log("Button successfully linked!");
        }
        else
        {
            Debug.LogError("UiScript Error: Could not find a Button named 'Settings' in the UXML hierarchy.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void showSettings()
    {
        Debug.Log("Show settings!!!");
        _uiDocument.visualTreeAsset = settingsMenuAsset;
    }
    private void showMainMenu()
    {
        _uiDocument.visualTreeAsset = mainMenuAsset;
    }
}
