using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{

    public AudioClip bgPangSound;
    public AudioClip bgStartSound;
    AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgStartSound;
        audioSource.Play();
    }

    void Start()
    {
        Invoke("Pang", 2); // Pang 함수를 바로 호출합니다.
    }

    void Pang()
    {
        audioSource.loop = true; // loop를 활성화합니다.
        audioSource.clip = bgPangSound;
        audioSource.Play();
    }


}
