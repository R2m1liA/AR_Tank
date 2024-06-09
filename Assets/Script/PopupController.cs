using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel;  // 弹窗面板
    public Button showButton;      // 显示弹窗按钮
    public Button closeButton;     // 关闭弹窗按钮

    void Start()
    {
        // 初始隐藏弹窗面板
        popupPanel.SetActive(false);

        // 添加按钮点击事件监听
        showButton.onClick.AddListener(ShowPopup);
        closeButton.onClick.AddListener(ClosePopup);
    }

    void ShowPopup()
    {
        popupPanel.SetActive(true);
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
