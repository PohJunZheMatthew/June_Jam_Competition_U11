using UnityEngine;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
    private static VisualTreeAsset e;
    [SerializeField] public VisualTreeAsset infoPopup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private static void spawn()
    {
        GameObject uiGO = new GameObject("Runtime_UIDocument");
        uiGO.AddComponent<UIDocument>();

    }
    public static void addPopup(VisualTreeAsset popup)
    {
        
    }
}
