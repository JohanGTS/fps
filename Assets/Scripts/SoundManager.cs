using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static SoundManager Instance {get;set;}

    public AudioSource shootingSound;
    public AudioSource reloadingSound;
    public AudioSource emptySound;

    void Awake()
    {
        if (Instance!=null && Instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance=this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
