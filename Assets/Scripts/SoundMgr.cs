using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    LevelUp,
    Energy,
    Money,
    Consumer,
    Chicken,
    Cow,
    Hello1,
    Hello2,
    Pa,
    Bought,
    Sex
}


public class SoundMgr : MonoBehaviour
{
    public AudioSource auLevel;
    public AudioSource auEnergy;
    public AudioSource auMoney;
    public AudioSource auConsumer;
    public AudioSource auChicken;
    public AudioSource auCow;
    public AudioSource auHello1;
    public AudioSource auHello2;
    public AudioSource auPa;
    public AudioSource auBought;
    public AudioSource auSex;


    public Dictionary<SoundType, AudioSource> dicSoundAudio = new Dictionary<SoundType, AudioSource>();
    public Dictionary<SoundType, float> dicSoundTime = new Dictionary<SoundType, float>();

    [Header("Test")]
    public SoundType testSoundType;

    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("PlaySound", PlaySound);
        EventCenter.Instance.AddEventListener("StopSound", StopSound);

    }

    public void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlaySound", PlaySound);
        EventCenter.Instance.RemoveEventListener("StopSound", StopSound);

    }

    public void Init()
    {
        dicSoundAudio.Clear();
        dicSoundAudio.Add(SoundType.LevelUp, auLevel);
        dicSoundAudio.Add(SoundType.Energy, auEnergy);
        dicSoundAudio.Add(SoundType.Money, auMoney);
        dicSoundAudio.Add(SoundType.Consumer, auConsumer);

        dicSoundAudio.Add(SoundType.Chicken, auChicken);
        dicSoundAudio.Add(SoundType.Cow, auCow);
        dicSoundAudio.Add(SoundType.Hello1, auHello1);
        dicSoundAudio.Add(SoundType.Hello2, auHello2);

        dicSoundAudio.Add(SoundType.Pa, auPa);
        dicSoundAudio.Add(SoundType.Bought, auBought);
        dicSoundAudio.Add(SoundType.Sex, auSex);


        dicSoundTime.Clear();

        dicSoundTime.Add(SoundType.LevelUp, 0.4f);
        dicSoundTime.Add(SoundType.Money, 0.4f);

        dicSoundTime.Add(SoundType.Pa, 1f);
        dicSoundTime.Add(SoundType.Bought, 1.3f);
        dicSoundTime.Add(SoundType.Sex, 0.8f);


    }

    public void PlaySound(object arg0)
    {
        SoundType soundType = (SoundType)arg0;

        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = 0.6f;
            if (dicSoundTime.ContainsKey(soundType))
            {
                playTime = dicSoundTime[soundType];
            }
            targetSound.time = playTime;
            targetSound.Play();
        }
    }

    public void StopSound(object arg0)
    {
        SoundType soundType = (SoundType)arg0;

        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            targetSound.Stop();
        }
    }


    public void PlaySoundTime(SoundType soundType, float playtime)
    {
        if (dicSoundAudio.ContainsKey(soundType))
        {
            AudioSource targetSound = dicSoundAudio[soundType];

            float playTime = playtime;
            targetSound.time = playTime;
            targetSound.Play();
        }
    }
}

