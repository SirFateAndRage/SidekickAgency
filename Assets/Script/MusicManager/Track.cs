using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Track
{
    public string name;
    public AudioClip clip;
    public bool loop;
    public int loopLength;

    public Track(string name)
    {
        this.name = name;
    }

    public Track Clone()
    {
        Track cloneTrack = new Track(this.name);
        cloneTrack.clip = this.clip;
        cloneTrack.loop = this.loop;
        cloneTrack.loopLength = this.loopLength;

        return cloneTrack;
    }
}

