using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // �ҡ Slider UI ������ Inspector
    public AudioMixer masterMixer; // �ҡ MasterMixer ������ Inspector
    public string volumeParameterName = "MasterVolume"; // ���� Exposed Parameter � Mixer (default: "MasterVolume")

    private float currentVolume = 1f; // ��� Volume �Ѩ�غѹ (������鹷�� 1 ���� 100%)

    void Start()
    {
        // ��Ŵ��� Volume ���ѹ�֡��� (�����)
        currentVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // ��Ŵ�ҡ PlayerPrefs, ������������ default 1
        volumeSlider.value = currentVolume; // ��駤�� Slider �繤�ҷ����Ŵ��
        SetVolume(currentVolume); // ��駤�� Volume � Audio Mixer

        // ���� Listener ��� Slider ����ͤ���ա������¹�ŧ
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        SetVolume(value);
    }

    public void SetVolume(float volumeValue)
    {
        // �ŧ��� Volume �ҡ 0-1 ����� Decibel (dB) - Audio Mixer �� dB
        // �ٵû���ҳ:  -80dB (��º) �֧ 0dB (�ѧ�ش)
        float volumeInDecibels = Mathf.Log10(Mathf.Max(volumeValue, 0.0001f)) * 20f; // ��ͧ�ѹ Log10(0) ��觨з�����Դ -Infinity

        // ��駤�� Volume � Audio Mixer ��ҹ Exposed Parameter
        masterMixer.SetFloat(volumeParameterName, volumeInDecibels);

        // �ѹ�֡��� Volume �Ѩ�غѹ
        currentVolume = volumeValue;
        PlayerPrefs.SetFloat("MasterVolume", currentVolume); // �ѹ�֡���ŧ PlayerPrefs ������餧��������չ��Т��� session
    }

    // �ѧ��ѹ���ж١���¡������� Scene �١ Unload (�� ����¹ Scene)
    private void OnDisable()
    {
        // �ѹ�֡��� Volume �ա��������� Script �١�Դ��ҹ (���͡ó� Scene Unload)
        PlayerPrefs.SetFloat("MasterVolume", currentVolume);
    }
}
