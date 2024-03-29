using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoNuevo : MonoBehaviour
{
    private int puntos;
    public TextMeshProUGUI puntuacionTexto=null;

    // Start is called before the first frame update

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        puntuacionTexto = GameObject.FindGameObjectWithTag("Puntuacion").GetComponent<TextMeshProUGUI>();
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

    public void NewGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void GaleriaTiro()
    {
        SceneManager.LoadScene("Tiro");
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reintentar()
    {
        SceneManager.LoadScene("Nivel1");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
