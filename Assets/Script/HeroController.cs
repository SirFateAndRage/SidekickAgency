using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;

public class HeroController : MonoBehaviour
{
    [SerializeField] GameObject _hero;
    [SerializeField] Transform _heroSpawnPoint;
    [SerializeField] Transform _bezierPoint1;
    [SerializeField] Transform _bezierPoint2;
    [SerializeField] Transform _bezierPoint3;
    [SerializeField] Animator _cannonAnimator;
    [SerializeField] CapsuleController _capsule;
    [SerializeField] Transform _cannonPoint;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] AudioSource _audioSource;
    public float height = 20f;

    bool _heroFollowsMouse = false;

    public void SpawnHero()
    {
        //_hero.SetActive(true);
        //_hero.transform.position = _heroSpawnPoint.position;
        //_hero.GetComponent<Animator>().Play("Flying");

        //StopAllCoroutines();
        //StartCoroutine(HeroAppearSequence());
    }

    public void SendHero()
    {
        _heroFollowsMouse = false;
        _cannonAnimator.SetTrigger("Shoot");
    }

    public void VFX()
    {
       CapsuleController current = Instantiate(_capsule);
        current.InitiCapsule();
    }

    public void TurnOffHero()
    {
        _heroFollowsMouse = false;
    }



    IEnumerator HeroAppearSequence()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward - _hero.transform.up * .8f;
        float timeToLerp = 0;

        while(Vector3.Distance(_hero.transform.position, clickPoint) > 0)
        {
            clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward - _hero.transform.up * .8f;
            _hero.transform.position = Vector3.Lerp(_heroSpawnPoint.position, clickPoint, timeToLerp);
            yield return null;
            timeToLerp += Time.deltaTime * 1.5f;
        }

        _heroFollowsMouse = true;
        _hero.GetComponent<Animator>().Play("Idle");
    }

    //IEnumerator HeroLeaveSequence()
    //{
    //    Vector3 heroLastPos = _hero.transform.position;
    //    float timeToLerp = 0;

    //    while(Vector3.Distance(_hero.transform.position, _heroSpawnPoint.position) > .5f)
    //    {
    //        _hero.transform.position = Vector3.Lerp(heroLastPos, _heroSpawnPoint.position, timeToLerp);
    //        yield return null;
    //        timeToLerp += Time.deltaTime;
    //    }
    //}

}
