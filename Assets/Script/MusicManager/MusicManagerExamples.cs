using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerExamples : MonoBehaviour
{
    MusicManager _musicManager;

    // Start is called before the first frame update
    void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _musicManager.Transition("Fadeout");
        if (Input.GetKeyDown(KeyCode.S))
            _musicManager.Transition("Sudden");
        if (Input.GetKeyDown(KeyCode.M))
            _musicManager.Transition("Mix");
    }
}
