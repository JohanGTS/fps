using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public TextMeshProUGUI puntuacionTexto=null;
    private string escena;
    private int puntos;
    public int damage=25; 
    // Start is called before the first frame update

        private void Start()
    {
        
        escena = SceneManager.GetActiveScene().name;
        // Cargar la puntuación guardada
        puntos = PlayerPrefs.GetInt("PuntuacionGuardada", 0);
        ActualizarPuntuacion();
    }
    private void ActualizarPuntuacion()
    {
        if (puntuacionTexto != null)
        {
            puntuacionTexto.text = "Puntuación: " + puntos.ToString();
        }
    }

    private void OnCollisionEnter (Collision collision)
    { 

        puntos = PlayerPrefs.GetInt("PuntuacionGuardada", 0);
        if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieScript>().TakeDamage(damage); 
            Destroy(gameObject);
        }
         
         if(escena=="Tiro")
         {
            puntuacionTexto = GameObject.FindGameObjectWithTag("Puntuacion").GetComponent<TextMeshProUGUI>();
         }
        

        if (puntuacionTexto !=null)
        {
            if (collision.gameObject.CompareTag("Dummy"))
                {
                    puntos+=20;
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }

            if (collision.gameObject.CompareTag("Diana"))
                {
                    puntos+=30;
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }

            if (collision.gameObject.CompareTag("Calabaza"))
                {
                    puntos+=40;
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }

            ActualizarPuntuacion();
            // Guardar la puntuación actualizada
            PlayerPrefs.SetInt("PuntuacionGuardada", puntos);
            PlayerPrefs.Save();
        }

    }
    
}
