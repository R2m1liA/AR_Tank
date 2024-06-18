using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }
}
