using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mMusic : MonoBehaviour
{
    static mMusic instance;
    public static mMusic Instance=>instance;
    public AudioSource musicSource;
    public AudioSource soundSource;
    private void Awake() {
        GameObject.DontDestroyOnLoad(gameObject);
        instance=this;
        MusicData musicData=mGameData.Instance.musicData;
        SetMusicOpen(musicData.muiscOpen);
        SetSoundOpen(musicData.soundOpen);
        SetMusicValue(musicData.musicValue);
        SetSoundValue(musicData.soundValue);
    }
    public void SetMusicOpen(bool musicOpen){
        musicSource.mute=!musicOpen;
    }
    public void SetSoundOpen(bool soundOpen){
        soundSource.mute=!soundOpen;
    }
    public void SetMusicValue(float musicValue){
        musicSource.volume=musicValue;
    }
    public void SetSoundValue(float soundValue){
        soundSource.volume=soundValue;
    }
    public void PlaySound(string res){
        GameObject musicObj=new GameObject();
        AudioSource a=musicObj.AddComponent<AudioSource>();
        a.mute=soundSource.mute;
        a.volume=soundSource.volume;
        a.clip=Resources.Load<AudioClip>(res);
        a.Play();
        GameObject.Destroy(musicObj,1);
    }
}
