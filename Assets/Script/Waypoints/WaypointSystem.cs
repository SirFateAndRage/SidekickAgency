using UnityEngine;
using System.Collections;

public class WaypointSystem : MonoBehaviour {

    public Transform[] allNodes;

	// Use this for initialization
	void Awake () {
        allNodes = new Transform[this.transform.childCount];
        for (int i = 0; i < allNodes.Length; i++)
        {
            //Obtengo y guardo el child que está en el índice "i".
            allNodes[i] = this.transform.GetChild(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        var transformChilds = transform.GetComponentsInChildren<Transform>();

        for (int i = 1; i < transformChilds.Length; i++)
        {
            Gizmos.DrawWireSphere(transformChilds[i].position, .3f);

            if(i == transformChilds.Length - 1)
                Gizmos.DrawLine(transformChilds[i].position, transformChilds[1].position);
            else
                Gizmos.DrawLine(transformChilds[i].position, transformChilds[i + 1].position);
        }
    }
}
