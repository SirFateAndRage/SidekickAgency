using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme : ScriptableObject
{
    public Track[] tracks;

    public Theme Clone()
    {
        var themeClone = CreateInstance<Theme>();
        themeClone.tracks = new Track[tracks.Length];
        for (int i = 0; i < tracks.Length; i++)
        {
            themeClone.tracks[i] = tracks[i].Clone();
        }

        return themeClone;
    }
}
