using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP=100;
    public float tiempoEntreAtkzombie =0.5f;
    private bool puedeAtacar = true;

    public bool EsJefe=false; 

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PuntuacionGuardada", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void TakeDamage (int damageAmount)
    {
        HP-=damageAmount;

        if (HP<=0)
        {
            //animator.SetTrigger("DIE");
            Debug.Log("Muerto");
        }

        else
        {
            Debug.Log("Dañado");
            //animator.SetTrigger("DAMAGE");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && puedeAtacar)
        {
            if (EsJefe)
            {
                TakeDamage(25);
            }
            else
            {
                TakeDamage(15);
            }
            
            puedeAtacar = false; // Desactiva la capacidad de atacar hasta que pase el tiempo de espera.
        }
    }

    private void AtacarJefe()
    {
        TakeDamage(25);
        ReiniciarAtaque(); // Vuelve a permitir el ataque después de que haya pasado el tiempo de espera.
    }

    private void AtacarZombie()
    {
        TakeDamage(10);
        ReiniciarAtaque(); // Vuelve a permitir el ataque después de que haya pasado el tiempo de espera.
    }

    private void ReiniciarAtaque()
    {
        puedeAtacar = true; // Vuelve a habilitar la capacidad de atacar.
    }
    
}
