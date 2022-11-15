using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    public Transform observed;
    private bool isObservedInRange;

    public GameEnding gameEnding;

    Vector3 direction;
    Ray ray;
    RaycastHit raycastHit;

    private void Update()
    {
        if (isObservedInRange && CheckIfHaveDirectVision())
        {
            Debug.Log("GAME OVER");
            gameEnding.CatchPlayer();
        }
    }

    private bool CheckIfHaveDirectVision()
    {
        direction = observed.position - transform.position + Vector3.up; //Direccion: Destino - inicio (+ up porque Jhon Lemon tiene el origen en los pies, asi se suma 1 metro de altura).
        ray = new Ray(transform.position, direction);

        Debug.DrawRay(transform.position, direction, Color.green, Time.deltaTime, true); //Dibujar un gizmo personalizado

        if (Physics.Raycast(ray, out raycastHit)) //true si choca contra algo
        {
            return raycastHit.collider.transform == observed;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == observed) //Es fisico, nunca habra dos en la misma posicion
        {
            isObservedInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == observed) //Es fisico, nunca habra dos en la misma posicion
        {
            isObservedInRange = false;
        }
    }

    // Override del metodo, solo se ejecuta en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, observed.position);
    }
}
