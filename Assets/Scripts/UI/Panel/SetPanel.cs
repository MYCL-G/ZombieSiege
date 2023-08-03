using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Slider sliMusic;
    public Toggle togSound;
    public Slider sliSound;

    public override void Init()
    {
        MusicData musicData=mGameData.Instance.musicData;
        togMusic.isOn=musicData.muiscOpen;
        sliMusic.value=musicData.musicValue;
        togSound.isOn=musicData.soundOpen;
        sliSound.value=musicData.soundValue;
        btnClose.onClick.AddListener(()=>{
            mGameData.Instance.SaveMusicData();
            mUI.Instance.HidePanel<SetPanel>();
        });
        togMusic.onValueChanged.AddListener((musicOpen)=>{
            mGameData.Instance.musicData.muiscOpen=musicOpen;
            mMusic.Instance.SetMusicOpen(musicOpen);
        });
        sliMusic.onValueChanged.AddListener((musicValue)=>{
            mGameData.Instance.musicData.musicValue=musicValue;
            mMusic.Instance.SetMusicValue(musicValue);
        });
        togSound.onValueChanged.AddListener((soundOpen)=>{
            mGameData.Instance.musicData.soundOpen=soundOpen;
            mMusic.Instance.SetSoundOpen(soundOpen);
        });
        sliSound.onValueChanged.AddListener((soundValue)=>{
            mGameData.Instance.musicData.soundValue=soundValue;
            mMusic.Instance.SetSoundValue(soundValue);
        });
    }
}
