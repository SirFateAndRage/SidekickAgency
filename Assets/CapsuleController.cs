using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;
    [SerializeField] Transform _cannonPosition;
    [SerializeField] Transform _endCurve;
    [SerializeField] AudioSource _audioSource;

    public float height = 20f;

    private Vector3 _currentEndCurve;

    private float timeToLerp;
    Vector3 _controlPoint;

    private void Start()
    {
        InitiCapsule();
    }

    public void InitiCapsule()
    {
        Camera.main.transform.DOShakePosition(.5f, 0.33f, 5, 45, false, true, ShakeRandomnessMode.Harmonic);
        gameObject.transform.localPosition = _cannonPosition.position;
        this.gameObject.SetActive(true);
        _particle.Play();
        _audioSource.Play();
        timeToLerp = 0f;
        _currentEndCurve = _endCurve.position;

        _controlPoint = (_cannonPosition.position + _endCurve.position) / 2 + Vector3.up * height;
    }

    private void Update()
    {
        if(Vector3.Distance(gameObject.transform.position,_endCurve.position) < 0.5f)
        {
            Destroy(this.gameObject);
        }
        timeToLerp += Time.deltaTime * 0.8f;
        Vector3 m1 = Vector3.Lerp(_cannonPosition.position, _controlPoint, timeToLerp);
        Vector3 m2 = Vector3.Lerp(_controlPoint, _currentEndCurve, timeToLerp);
        gameObject.transform.LookAt(Vector3.Lerp(m1, m2, timeToLerp));
        gameObject.transform.position = Vector3.Lerp(m1, m2, timeToLerp);

    }

}
