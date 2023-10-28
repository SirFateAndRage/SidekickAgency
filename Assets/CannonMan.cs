using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMan : MonoBehaviour
{
    public ParticleSystem psShoot;
    public Animator animator;
    public AnimationEvent shootFunction;

    //Rotation
    Vector3 directionToMouse = Vector3.zero;
    float angle = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootAnimation();
        }
        UpdateAxis();
    }

    private void UpdateAxis()
    {
        Vector3 mousePos = GetMousePos();
        directionToMouse = (mousePos - transform.position).normalized; 
        angle = Mathf.Atan2(directionToMouse.x, directionToMouse.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        Debug.Log(mousePos);
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePositionScreen);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point; 
        }
        return Vector3.zero;
    }

    public void ShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void VFX()
    {
        psShoot.Play();
    }

}
