using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _gameMotor;
    [SerializeField] GameObject _tutorialMessage;

    public void BeginGame()
    {
        _gameMotor.SetActive(true);
        _tutorialMessage.SetActive (false);
        Destroy(this.gameObject);
    }
}
