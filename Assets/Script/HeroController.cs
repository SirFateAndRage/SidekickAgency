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
    [SerializeField] Animator _cannonAnimator;
    [SerializeField] CapsuleController _capsule;
    [SerializeField] Transform _cannonPoint;
    [SerializeField] Transform _heroSpawn;
    [SerializeField] Transform _initialHeroSpawn;

    private float _speed = 1f;

    public void SpawnHero()
    {
        _cannonAnimator.SetTrigger("Fly");
        StopAllCoroutines();
        StartCoroutine(GoToPosition());
    }

    private IEnumerator GoToPosition()
    {
        float distance = Vector3.Distance(_hero.transform.position, _heroSpawn.position);

        float duration = distance / _speed;

        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            _hero.transform.position = Vector3.Lerp(_hero.transform.position, _heroSpawn.transform.position, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _hero.transform.position = _heroSpawn.transform.position;
    }

    private IEnumerator GoToReturnTO()
    {
        float distance = Vector3.Distance(_initialHeroSpawn.position, _hero.transform.position);

        float duration = distance / _speed;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            _hero.transform.position = Vector3.Lerp(_hero.transform.position, _initialHeroSpawn.position, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _hero.transform.position = _initialHeroSpawn.position;
    }


    public void SendHero()
    {
        _cannonAnimator.SetTrigger("Shoot");
    }

    public void VFX()
    {
       CapsuleController current = Instantiate(_capsule);
        current.InitiCapsule();
    }

    public void Return()
    {
       
        _cannonAnimator.SetTrigger("Fly");
        StartCoroutine(GoToReturnTO());
    }



    //IEnumerator HeroAppearSequence()
    //{
    //    Vector3 clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward - _hero.transform.up * .8f;
    //    float timeToLerp = 0;

    //    while(Vector3.Distance(_hero.transform.position, clickPoint) > 0)
    //    {
    //        clickPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, .5f)) + _hero.transform.forward - _hero.transform.up * .8f;
    //        _hero.transform.position = Vector3.Lerp(_heroSpawnPoint.position, clickPoint, timeToLerp);
    //        yield return null;
    //        timeToLerp += Time.deltaTime * 1.5f;
    //    }

    //    _heroFollowsMouse = true;
    //    _hero.GetComponent<Animator>().Play("Idle");
    //}
}
