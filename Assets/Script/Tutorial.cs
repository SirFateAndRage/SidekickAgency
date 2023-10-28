using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _gameMotor;
    [SerializeField] GameObject _tutorialMessage;
    [SerializeField] GameObject _flavorText;

    int clicks = 0;

    public void BeginGame()
    {
        if (clicks == 1)
        {
            _gameMotor.SetActive(true);
            _flavorText.SetActive(false);
            Destroy(this.gameObject);

        }
        else
        {
            _tutorialMessage.SetActive(false);
            _flavorText.SetActive(true);
        }
        clicks++;
    }
}
