using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> lettersSounds;
    public static SoundManager inst;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public int latestUsedIndex;
    public void PlayAudioToLetter(int index)
    {
        latestUsedIndex = index;
        //lettersSounds[index].Play();
    }

    public void PlayAudioLetterLatest()
    {
        lettersSounds[latestUsedIndex].Play();
    }
}
