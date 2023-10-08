using UnityEngine;
using System.Collections;

public class Bezier3 : MonoBehaviour {

    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform target;
    public float t;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        target.position = Vector3.Lerp( Vector3.Lerp(p0.position, p1.position, t), Vector3.Lerp(p1.position, p2.position, t), t );

        t += Time.deltaTime;
        if(t > 1)
        {
            t = 0;
        }
	}
}
