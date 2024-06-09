// using UnityEngine;
// using UnityEngine.UI;

// public class AudioController : MonoBehaviour
// {
//     public Slider volumeSlider;      // 音量滑块

//     public Slider sfxSlider;         // 音效滑块

//     public AudioSource musicSource;  // 音乐音频源
//     public AudioSource sfxSource;    // 音效音频源

//     void Start()
//     {
//         // 初始化滑块和文本
//         volumeSlider.value = musicSource.volume;
//         sfxSlider.value = sfxSource.volume;
        

//         // 添加滑块值变化的监听器
//         volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
//         sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
//     }

//     void OnVolumeSliderValueChanged(float value)
//     {
//         musicSource.volume = value;
//         // UpdateVolumeText();
//     }

//     void OnSFXSliderValueChanged(float value)
//     {
//         sfxSource.volume = value;
//         // UpdateSFXText();
//     }

   
// }
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer; // 引用Audio Mixer

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 确保在场景切换时不会销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); // 转换为分贝
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // 转换为分贝
    }

    public float GetVolume()
    {
        float volume;
        audioMixer.GetFloat("Volume", out volume);
        return Mathf.Pow(10, volume / 20); // 从分贝转换为线性值
    }

    public float GetSFXVolume()
    {
        float volume;
        audioMixer.GetFloat("SFXVolume", out volume);
        return Mathf.Pow(10, volume / 20); // 从分贝转换为线性值
    }
}
