using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonFacePosition : MonoBehaviour
{
    [SerializeField] private Transform _headTransform;


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Si el rayo golpea algo, obtenemos la información del punto de impacto
            Vector3 hitPoint = hit.point;
            Debug.Log("Hit point: " + hitPoint);
            _headTransform.LookAt(hitPoint);


        }

    }
}
