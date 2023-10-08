using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] GameObject _hero;
    [SerializeField] Transform _heroSpawnPoint;
    [SerializeField] Transform _bezierPoint1;
    [SerializeField] Transform _bezierPoint2;
    [SerializeField] Transform _bezierPoint3;

    bool _heroFollowsMouse = false;

    public void SpawnHero()
    {
        _hero.SetActive(true);
        _hero.transform.position = _heroSpawnPoint.position;
        _hero.GetComponent<Animator>().Play("Flying");

        StartCoroutine(HeroAppearSequence());
    }

    public void SendHero()
    {
        _heroFollowsMouse = false;
        _hero.GetComponent<Animator>().Play("Flying");

        StartCoroutine(HeroTravelSequence());
    }

    public void TurnOffHero()
    {
        _heroFollowsMouse = false;
        
        StartCoroutine(HeroLeaveSequence());
    }

    void Update() 
    {
        if(!_heroFollowsMouse)
            return;   

        _hero.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward * .8f;
        _bezierPoint1 = _hero.transform;
    }

    IEnumerator HeroAppearSequence()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward * .8f;
        float timeToLerp = 0;

        while(Vector3.Distance(_hero.transform.position, clickPoint) > 0)
        {
            clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward * .8f;
            _hero.transform.position = Vector3.Lerp(_heroSpawnPoint.position, clickPoint, timeToLerp);
            yield return null;
            timeToLerp += Time.deltaTime * 1.5f;
        }

        _heroFollowsMouse = true;
        _hero.GetComponent<Animator>().Play("Idle");
    }

    IEnumerator HeroLeaveSequence()
    {
        Vector3 heroLastPos = _hero.transform.position;
        float timeToLerp = 0;


        while(Vector3.Distance(_hero.transform.position, _heroSpawnPoint.position) > 0)
        {
            _hero.transform.position = Vector3.Lerp(heroLastPos, _heroSpawnPoint.position, timeToLerp);
            yield return null;
            timeToLerp += Time.deltaTime;
        }

        _hero.SetActive(false);
    }

    IEnumerator HeroTravelSequence()
    {
        Camera.main.transform.DOShakePosition(.5f, 1, 5, 45, false, true, ShakeRandomnessMode.Harmonic);
        float timeToLerp = 0;

        _hero.GetComponent<AudioSource>().Play();

        while(Vector3.Distance(_hero.transform.position, _bezierPoint3.position) > 0)
        {
            _hero.transform.position = Vector3.Lerp(Vector3.Lerp(_bezierPoint1.position, _bezierPoint2.position, timeToLerp), Vector3.Lerp(_bezierPoint2.position, _bezierPoint3.position, timeToLerp), timeToLerp);
            yield return null;
            timeToLerp += Time.deltaTime * .4f;
        }

        _hero.SetActive(false);
    }
}
