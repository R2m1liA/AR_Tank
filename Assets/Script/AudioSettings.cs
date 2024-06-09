using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider; // 音量滑块
    public Slider sfxSlider; // 音效滑块

    private void Start()
    {
        // 初始化滑块值
        volumeSlider.value = AudioManager.Instance.GetVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        // 添加滑块事件监听
        volumeSlider.onValueChanged.AddListener(SetVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
