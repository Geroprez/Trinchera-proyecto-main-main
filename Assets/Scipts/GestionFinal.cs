using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager

public class GestionFinal : MonoBehaviour
{
    public SpriteRenderer spriteRendererFinal; // Referencia al SpriteRenderer en el mundo del juego
    public Sprite finalA; // Sprite para el Final A
    public Sprite finalB; // Sprite para el Final B
    private int npcContador = 0; // Contador de NPCs interactuados
    public int npcMinimoParaFinalA = 3; // Número mínimo de NPCs para activar el Final A

    public BoxCollider2D areaFinal; // Área de interacción con el final (BoxCollider2D)
    private MovimientoJugador movimientoJugador; // Referencia al script del jugador

    public float distanciaAlFinal; // Distancia entre el jugador y el final
    public float tiempoParaCargarCreditos = 2f; // Tiempo para esperar antes de cargar los créditos

    private bool finalActivado = false; // Bandera para evitar activar el final más de una vez

    private void Start()
    {
        movimientoJugador = FindObjectOfType<MovimientoJugador>(); // Obtener el script del jugador
    }

    void Update()
    {
        if (movimientoJugador != null && !finalActivado) // Verifica que el final no esté activado ya
        {
            distanciaAlFinal = Vector2.Distance(movimientoJugador.transform.position, transform.position);
            Debug.Log("Distancia al final: " + distanciaAlFinal);

            // Activar el final si el jugador está dentro de una distancia de 2 unidades
            if (distanciaAlFinal <= 2f)
            {
                ActivarFinal(); // Llamar a la función que activa el final
            }
        }
    }

    // Método para incrementar el contador de NPCs
    public void IncrementarNPCs()
    {
        npcContador++;
        Debug.Log("NPCs interactuados: " + npcContador);
        // No llamar a ActivarFinal aquí, para evitar activar el final prematuramente
    }

    // Método para determinar qué final activar
    private void ActivarFinal()
    {
        if (finalActivado) return; // Si el final ya está activado, no hacer nada

        // Verificar si el jugador ha interactuado con suficientes NPCs
        if (npcContador >= npcMinimoParaFinalA)
        {
            MostrarFinal(finalA); // Mostrar el Final A
        }
        else
        {
            MostrarFinal(finalB); // Mostrar el Final B
        }

        finalActivado = true; // Marcar que el final ha sido activado
    }

    // Método para mostrar el sprite del final
    private void MostrarFinal(Sprite final)
    {
        if (spriteRendererFinal != null)
        {
            spriteRendererFinal.sprite = final;
            spriteRendererFinal.enabled = true; // Asegurarse de que el sprite esté visible
        }

        // Deshabilitar el movimiento del jugador al activar el final
        if (movimientoJugador != null)
        {
            movimientoJugador.DeshabilitarMovimientoFinal(); // Desactivar movimiento del jugador
        }

        // Iniciar el tiempo de espera para cargar los créditos
        StartCoroutine(CargarCreditos());
    }

    // Coroutine para esperar un tiempo y luego cargar la escena de créditos
    private IEnumerator CargarCreditos()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoParaCargarCreditos);

        // Carga la escena de los créditos
        SceneManager.LoadScene("Creditos");
    }

    // Detección de cuando el jugador entra en el área del final
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player") && !finalActivado) 
        {
            ActivarFinal(); // Activar el final al entrar en el área
        }
    }

    // Detección de cuando el jugador sale del área del final
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que sale es el jugador
        if (other.CompareTag("Player")) 
        {
            // Aquí podrías reactivar el movimiento del jugador si es necesario
        }
    }
}






