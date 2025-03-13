using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // ลาก Slider UI มาใส่ใน Inspector
    public AudioMixer masterMixer; // ลาก MasterMixer มาใส่ใน Inspector
    public string volumeParameterName = "MasterVolume"; // ชื่อ Exposed Parameter ใน Mixer (default: "MasterVolume")

    private float currentVolume = 1f; // ค่า Volume ปัจจุบัน (เริ่มต้นที่ 1 หรือ 100%)

    void Start()
    {
        // โหลดค่า Volume ที่บันทึกไว้ (ถ้ามี)
        currentVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // โหลดจาก PlayerPrefs, ถ้าไม่มีใช้ค่า default 1
        volumeSlider.value = currentVolume; // ตั้งค่า Slider เป็นค่าที่โหลดมา
        SetVolume(currentVolume); // ตั้งค่า Volume ใน Audio Mixer

        // เพิ่ม Listener ให้ Slider เมื่อค่ามีการเปลี่ยนแปลง
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        SetVolume(value);
    }

    public void SetVolume(float volumeValue)
    {
        // แปลงค่า Volume จาก 0-1 ให้เป็น Decibel (dB) - Audio Mixer ใช้ dB
        // สูตรประมาณ:  -80dB (เงียบ) ถึง 0dB (ดังสุด)
        float volumeInDecibels = Mathf.Log10(Mathf.Max(volumeValue, 0.0001f)) * 20f; // ป้องกัน Log10(0) ซึ่งจะทำให้เกิด -Infinity

        // ตั้งค่า Volume ใน Audio Mixer ผ่าน Exposed Parameter
        masterMixer.SetFloat(volumeParameterName, volumeInDecibels);

        // บันทึกค่า Volume ปัจจุบัน
        currentVolume = volumeValue;
        PlayerPrefs.SetFloat("MasterVolume", currentVolume); // บันทึกค่าลง PlayerPrefs เพื่อให้คงอยู่ข้ามซีนและข้าม session
    }

    // ฟังก์ชันนี้จะถูกเรียกใช้เมื่อ Scene ถูก Unload (เช่น เปลี่ยน Scene)
    private void OnDisable()
    {
        // บันทึกค่า Volume อีกครั้งเมื่อ Script ถูกปิดใช้งาน (เผื่อกรณี Scene Unload)
        PlayerPrefs.SetFloat("MasterVolume", currentVolume);
    }
}
