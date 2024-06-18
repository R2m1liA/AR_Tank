using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // 初始化滑动条的初始值
        bgmSlider.value = AudioManager.Instance.GetVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        // 绑定滑动条值变化事件
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
