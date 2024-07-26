using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] Sfx;
    [SerializeField] private AudioSource[] Bgm;
    [SerializeField] private float sfxMinDistance;

    private bool canPlaySfx;

    public bool playBgm;
    private int BgmIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        Invoke("AllowSfx", 0.1f); //使游戏一开始不应出现的音效去除
    }
    private void Update()
    {
        if (!playBgm)
        { 
            StopAllBGM();
        }
        else
        {
            if (!Bgm[BgmIndex].isPlaying) //如果非指定的Bgm正在播放
            {
                PlayBGM(BgmIndex); //指定音乐并播放
            }
        }
    }

    public void PlaySfx(int _sfx, Transform _source)
    {
        //if (Sfx[_sfx].isPlaying) //如果该音效已在播放，则停止其他物体上的重复播放
        //{ 
        //    return;
        //}

        if (canPlaySfx == false)
        { 
            return;
        }

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinDistance) //如果角色距离物体过远则停止播放该物体上的声音
        { 
            return ;
        }

        if (_sfx < Sfx.Length)  //若指定音效存在
        {
            Sfx[_sfx].pitch = Random.Range(0.85f, 1.5f);

            if (_sfx == 40)
                Sfx[_sfx].pitch = Random.Range(0.9f, 1.1f); //缩小弹反的变调范围

            Sfx[_sfx].Play();



        }
    }

    public void StopSfx(int _index)
    {
        Sfx[_index].Stop();
    }

    public void StopSfxWithTime(int _index)
    {
        StartCoroutine(DecreaseVloume(Sfx[_index]));
    }

    private IEnumerator DecreaseVloume(AudioSource _audio) //音效淡出效果
    { 
        float defaultVolume = _audio.volume;

        while (_audio.volume > 0.1f)
        { 
            _audio.volume -= _audio.volume * 0.2f;
            yield return new WaitForSeconds(0.25f);

            if (_audio.volume <= 0.1f)
            { 
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }

    public void PlayBGM(int _BGMindex)//先停止播放所有BGM，再根据索引选择播放BGM
    {
        BgmIndex = _BGMindex;

        StopAllBGM();

        Bgm[BgmIndex].Play();
    }

    public void RandomPlay()//随机播放音乐
    {
        BgmIndex = Random.Range(0, Bgm.Length);
        PlayBGM(BgmIndex);
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < Bgm.Length; i++) //先停止播放所有BGM，再根据索引选择播放BGM
        {
            Bgm[i].Stop();
        }

    }

    private void AllowSfx()
    { 
        canPlaySfx = true;
    }

}
