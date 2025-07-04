using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name; // 곡의 이름
    public AudioClip clip; // 곡
}


public class SoundManager : MonoBehaviour
{
    // singleton  instance

    static public SoundManager instance;

    #region singleton
    void Awake() // 객체 생성시 최초 실행.
    {
        if (instance == null)
        {
            instance = this; // 자기 자신을 instance로 지정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourcesBGM;

    public string[] playSoundNames;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Start()
    {
        playSoundNames = new string[audioSourceEffects.Length];
    }


    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (effectSounds[i].name == _name)
            {
                for(int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying) // 현재 재생중이지 않은 AudioSource를 찾는다.
                    {
                        playSoundNames[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
            Debug.Log(_name + "사운드가 SoundManager에 없습니다.");
        }
    }

    public void StopAllSE()
    {
       for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }   
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if(playSoundNames[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log(_name + "사운드가 없습니다.");
    }
}
