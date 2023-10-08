using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public bool playOnStart;
    public Theme themeToPlay;

    Track _currentTrack;
    int _trackCount = 0;
    int _loopCount = 0;
    float _currentTrackTime;

    [SerializeField] public List<Transition> transitions = new List<Transition>();

    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (playOnStart)
            PlayTheme();
    }

    void Update()
    {
        _currentTrackTime += Time.deltaTime;

        if (_currentTrackTime >= _audioSource.clip.length)
        {
            _currentTrackTime = 0;
            if (_currentTrack.loop && _currentTrack.loopLength == 0)
                return;

            if (_loopCount >= _currentTrack.loopLength)
            {
                _loopCount = 0;
                _trackCount++;
                NextTrack();
            }
            else
                _loopCount++;
        }
    }

    public void PlayTheme()
    {
        _currentTrack = themeToPlay.tracks[0];
        _audioSource.loop = _currentTrack.loop;
        _audioSource.clip = _currentTrack.clip;
        _currentTrackTime = 0;
        _loopCount = 0;

        _audioSource.Play();
        if (themeToPlay.tracks.Length > 1)
            themeToPlay.tracks[1].clip.LoadAudioData();
    }

    public void PlayTheme(Theme themeToPlay)
    {
        this.themeToPlay = themeToPlay;
        PlayTheme();
    }

    void NextTrack()
    {
        _currentTrack = themeToPlay.tracks[_trackCount];
        _audioSource.Stop();

        _audioSource.loop = _currentTrack.loop;
        _audioSource.clip = _currentTrack.clip;

        _audioSource.Play();
        if (_trackCount < themeToPlay.tracks.Length - 1)
            themeToPlay.tracks[_trackCount + 1].clip.LoadAudioData();
    }

    public void Transition(string transitionName)
    {
        var transition = transitions.Where(x => x.name == transitionName).FirstOrDefault();

        switch (transition.transitionMode)
        {
            case TransitionModes.Sudden:
                SuddenTransition(transition.themeToTransitionTo);
                break;
            case TransitionModes.FadeOut:
                StartCoroutine(FadeoutTransition(transition.themeToTransitionTo, transition.floatParameters[0], transition.boolParameters[0]));
                break;
            case TransitionModes.Mix:
                StartCoroutine(MixTransition(transition.themeToTransitionTo, transition.floatParameters[0]));
                break;
            default:
                break;
        }
    }

    void SuddenTransition(Theme themeToTransitionTo)
    {
        _audioSource.Stop();
        PlayTheme(themeToTransitionTo);
    }

    IEnumerator FadeoutTransition(Theme themeToTransitiontTo, float transitionSpeed, bool waitTillEndOfTrack)
    {
        if (waitTillEndOfTrack)
            //Need a little threshold in order to transition smoothly
            yield return new WaitUntil(() => _currentTrackTime >= _audioSource.clip.length - .25f);

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime * transitionSpeed;
            yield return null;
        }

        PlayTheme(themeToTransitiontTo);

        while (_audioSource.volume < 1)
        {
            _audioSource.volume += Time.deltaTime * transitionSpeed;
            yield return null;
        }
    }

    IEnumerator MixTransition(Theme themeToTransitionTo, float transitionSpeed)
    {
        AudioSource transitionAudioSource = GetComponents<AudioSource>().Where(x => x != _audioSource).FirstOrDefault();
        transitionAudioSource.clip = _currentTrack.clip;
        transitionAudioSource.volume = _audioSource.volume;
        transitionAudioSource.time = _currentTrackTime;
        transitionAudioSource.Play();

        PlayTheme(themeToTransitionTo);
        _audioSource.volume = 0;

        while(_audioSource.volume < 1 && transitionAudioSource.volume > 0)
        {
            _audioSource.volume += Time.deltaTime * transitionSpeed;
            transitionAudioSource.volume -= Time.deltaTime * transitionSpeed;
            yield return null;
        }
        transitionAudioSource.Stop();
    }
}

[System.Serializable]
public class Transition
{
    public string name;
    public Theme themeToTransitionTo;
    public TransitionModes transitionMode;
    public float[] floatParameters = new float[1];
    public bool[] boolParameters = new bool[1];

    public Transition(string name)
    {
        this.name = name;
    }
}

public enum TransitionModes
{
    Sudden,
    FadeOut,
    Mix,
}
