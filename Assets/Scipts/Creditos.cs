using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    private Animator animator;

    [Header("Cuadro para detener")]
    public int cuadroDetencion = 9; // Cuadro donde se detendrá la animación

    private void Start()
    {
        // Obtiene el componente Animator del objeto
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Obtiene la información de estado actual del Animator
        AnimatorStateInfo estadoActual = animator.GetCurrentAnimatorStateInfo(0);

        // Obtiene el tiempo normalizado de la animación
        float tiempoNormalizado = estadoActual.normalizedTime % 1; // % 1 asegura que el valor esté entre 0 y 1

        // Calcula el cuadro de la animación basado en el tiempo normalizado
        float duracionAnimacion = estadoActual.length;
        float tiempoEnSegundos = tiempoNormalizado * duracionAnimacion;

        // Define el número de cuadros (ajusta si es necesario)
        int cuadrosTotales = 10; // Suponiendo que hay 10 cuadros en la animación

        // Calcula el cuadro actual
        int cuadroActual = Mathf.FloorToInt(tiempoEnSegundos * cuadrosTotales / duracionAnimacion);

        // Detiene la animación si se alcanza el cuadro de detención
        if (cuadroActual >= cuadroDetencion)
        {
            animator.speed = 0; // Detiene la animación
        }
    }
}



