using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieSpawnController : MonoBehaviour
{
public GameObject zombiePrefab;
public int initialZombiePerWave=4;
public int currentZombiesPerWave;
public float spawnDelay=0.5f;
public int currentWave=0;
public float waveCooldown=10.0f;
public bool inCoolDown;
public float cooldownCounter=0;

public TextMeshProUGUI waverOverUI,cooldonCounterUI;

public List<ZombieScript> currentZombiesAlive;
    public int maxWave;
    private string escena;

    // Start is called before the first frame update
    void Start()
    {
        currentZombiesPerWave=initialZombiePerWave;
        escena = SceneManager.GetActiveScene().name;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;

        StartCoroutine(spawnWave());
    }

    private IEnumerator spawnWave()
    {
         for(int i=0;i<currentZombiesPerWave;i++)
         {
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(1f,1f),0f,UnityEngine.Random.Range(1f,1f));
         Vector3 spawnPoint = transform.position+spawnOffset;
         var zombie = Instantiate(zombiePrefab,spawnPoint, Quaternion.identity);
         ZombieScript enemyScript =  zombie.GetComponent<ZombieScript>();
         currentZombiesAlive.Add(enemyScript);
         yield return new WaitForSeconds(spawnDelay);

         }
    }

    // Update is called once per frame
    void Update()
    {
        List<ZombieScript> zombiesToRemove= new List<ZombieScript>();

        foreach (ZombieScript zombie in currentZombiesAlive)
        {
            if (zombie.HP<=0)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        foreach (ZombieScript zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();
        if (currentZombiesAlive.Count==0 && !inCoolDown)
        {
            if(currentWave>=maxWave && currentZombiesAlive.Count==0){
                if(escena=="Nivel1")
                {
                    Invoke("CargarNivel2", 3f);
                }
                else if(escena=="Nivel2")
                {
                    Invoke("CargarGanar", 3f);
                }
            }
            else
            {
                StartCoroutine(WaveCooldown());
            }
        }

        if (inCoolDown)
        {
            cooldownCounter-=Time.deltaTime;
        }
        else
        {
            cooldownCounter=waveCooldown;
        }

        cooldonCounterUI.text= cooldownCounter.ToString("F0");
    }

    void CargarNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    void CargarGanar()
    {
        SceneManager.LoadScene("Ganar");
    }
    private IEnumerator WaveCooldown()
    {
        inCoolDown=true;
        waverOverUI.gameObject.SetActive(true);
        cooldonCounterUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(waveCooldown);
        inCoolDown=false;
        cooldonCounterUI.gameObject.SetActive(false);
        waverOverUI.gameObject.SetActive(false);
        currentZombiesPerWave*=2;
        StartNextWave();
    }
}
