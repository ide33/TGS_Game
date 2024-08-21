using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioClip bgmClip;  // 再生するBGMのAudioClip
    public float startTime = 30.0f;  // BGMの再生開始位置（秒単位）
    private AudioSource audioSource;  // AudioSourceコンポーネント

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // BGMを設定し、ループを有効にする
        audioSource.clip = bgmClip;
        audioSource.loop = true;


        // 指定した位置から再生を開始
        audioSource.time = startTime;
        audioSource.Play();
    }
}
