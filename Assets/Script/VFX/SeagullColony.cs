using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullColony : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPositions;
    [SerializeField] float _speed;
    [SerializeField] float _timeToSpawn;
    float _currentTime = 0;

    void Start() 
    {
        Animator[] seagullAnimators = GetComponentsInChildren<Animator>();

        foreach (var seagull in seagullAnimators)
        {
            float randomOffset = Random.Range(0f, 1f);   
            seagull.Play("Idle", 0, randomOffset); 
        }
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _timeToSpawn)
        {
            _currentTime = 0;
            Transform randomSpawn = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
            transform.position = randomSpawn.position;
            transform.forward = -randomSpawn.forward;
        }

        transform.position -= transform.forward * _speed * Time.deltaTime;
    }
}
