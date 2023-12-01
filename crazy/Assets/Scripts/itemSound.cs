using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bossBoomSound;
    public GameObject[] atkcheck;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        BoomCheck();
    }

    void BoomCheck()
    {
        // 이전에 재생 중이던 소리가 끝나지 않았으면 새로운 소리를 재생하지 않음
        if (!audioSource.isPlaying)
        {
            for (int i = 0; i < atkcheck.Length; i++)
            {
                if (atkcheck[i].activeSelf == true)
                {
                    audioSource.clip = bossBoomSound;
                    audioSource.Play();
                    Debug.Log(atkcheck[i].name + " BossAttackSound");
                    // 여기에서 소리가 한 번만 재생되도록 하기 위해 break; 추가
                    break;
                }
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.name("PlayerA") || other.gameObject.name("PlayerB"))
    //     {
    //         audioSource.clip = itemAddSound;
    //         audioSource.Play();
    //         Debug.Log(other.gameObject.name);
    //     }
    // }
}
