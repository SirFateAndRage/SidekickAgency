using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject canvas; // Referencia al Canvas que quieres activar

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvas.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvas.SetActive(false);
    }

    void OnMouseEnter()
    {
        // Activa el Canvas cuando el rat�n entra en el �rea de colisi�n
    }

    void OnMouseExit()
    {
         // Desactiva el Canvas cuando el rat�n sale del �rea de colisi�n
    }
}
