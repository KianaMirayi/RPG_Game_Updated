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

        Invoke("AllowSfx", 0.1f); //ʹ��Ϸһ��ʼ��Ӧ���ֵ���Чȥ��
    }
    private void Update()
    {
        if (!playBgm)
        { 
            StopAllBGM();
        }
        else
        {
            if (!Bgm[BgmIndex].isPlaying) //�����ָ����Bgm���ڲ���
            {
                PlayBGM(BgmIndex); //ָ�����ֲ�����
            }
        }
    }

    public void PlaySfx(int _sfx, Transform _source)
    {
        //if (Sfx[_sfx].isPlaying) //�������Ч���ڲ��ţ���ֹͣ���������ϵ��ظ�����
        //{ 
        //    return;
        //}

        if (canPlaySfx == false)
        { 
            return;
        }

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinDistance) //�����ɫ���������Զ��ֹͣ���Ÿ������ϵ�����
        { 
            return ;
        }

        if (_sfx < Sfx.Length)  //��ָ����Ч����
        {
            Sfx[_sfx].pitch = Random.Range(0.85f, 1.5f);

            if (_sfx == 40)
                Sfx[_sfx].pitch = Random.Range(0.9f, 1.1f); //��С�����ı����Χ

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

    private IEnumerator DecreaseVloume(AudioSource _audio) //��Ч����Ч��
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

    public void PlayBGM(int _BGMindex)//��ֹͣ��������BGM���ٸ�������ѡ�񲥷�BGM
    {
        BgmIndex = _BGMindex;

        StopAllBGM();

        Bgm[BgmIndex].Play();
    }

    public void RandomPlay()//�����������
    {
        BgmIndex = Random.Range(0, Bgm.Length);
        PlayBGM(BgmIndex);
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < Bgm.Length; i++) //��ֹͣ��������BGM���ٸ�������ѡ�񲥷�BGM
        {
            Bgm[i].Stop();
        }

    }

    private void AllowSfx()
    { 
        canPlaySfx = true;
    }

}
