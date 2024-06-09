using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider;      // 音量滑块
    public Text volumeText;          // 音量显示文本
    public Slider sfxSlider;         // 音效滑块
    public Text sfxText;             // 音效显示文本
    public AudioSource musicSource;  // 音乐音频源
    public AudioSource sfxSource;    // 音效音频源

    void Start()
    {
        // 初始化滑块和文本
        volumeSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;
        UpdateVolumeText();
        UpdateSFXText();

        // 添加滑块值变化的监听器
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }

    void OnVolumeSliderValueChanged(float value)
    {
        musicSource.volume = value;
        UpdateVolumeText();
    }

    void OnSFXSliderValueChanged(float value)
    {
        sfxSource.volume = value;
        UpdateSFXText();
    }

    void UpdateVolumeText()
    {
        volumeText.text = $"Volume: {(int)(volumeSlider.value * 100)}%";
    }

    void UpdateSFXText()
    {
        sfxText.text = $"SFX: {(int)(sfxSlider.value * 100)}%";
    }
}
