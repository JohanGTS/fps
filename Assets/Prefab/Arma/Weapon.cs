using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera playerCamera;
    public bool isShooting,readyToShoot;
    public float shootingDelay=2f;
    bool allowReset =true;

    public int bulletsPerBurst =3;
    public int currentBurst;
    private string escena;
    public float spreadIntesity;

    public GameObject bulletPrefab;
    public Transform bulletSpwan;
    public float bulletVelocuty=30;
    public float bulletPrefabLifeTime=3f;

    public enum ShootingMode {Single, Burst, Auto}

    public ShootingMode currentShootingMode;

    public GameObject muzzleEffect;

    public bool isReloading;
    public float reloadTime;
    public int magazineSize,bulletsLeft,magazineLefts;

    public TextMeshProUGUI ammoDisplay,bulletDisplay;

    void Awake()
    {
        readyToShoot=true ;
        currentBurst=bulletsPerBurst;
        bulletsLeft= magazineSize;
        escena = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        isShooting =Input.GetKeyDown(KeyCode.Mouse0);

        if ( isShooting && !isReloading && bulletsLeft>0)
        {
            currentBurst=bulletsPerBurst;
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R)&& bulletsLeft<magazineSize && !isReloading && magazineLefts>0)
        {
            Reload();
        }

        if (ammoDisplay!=null)
        {
            if(escena=="Tiro")
            {
                ammoDisplay.text="0";
            }
            else
            {
                ammoDisplay.text=magazineLefts.ToString();
            } 
            
        }

        if (bulletDisplay!= null)
        {
            bulletDisplay.text= bulletsLeft.ToString();
        }

        if (bulletsLeft==0 && isShooting)
        {
            SoundManager.Instance.emptySound.Play();
        }

        if(bulletsLeft==0 &&magazineLefts==0)
        {
            if(escena=="Tiro")
            {
                Invoke("CargarPuntuacion",3f);
            }
            else
            {
                SceneManager.LoadScene("Dead");
            }    
        }
        

    }

    void CargarPuntuacion()
    {
        SceneManager.LoadScene("Puntuacion");
    }

    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.shootingSound.Play();
        readyToShoot=false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab,bulletSpwan.position, Quaternion.identity);
        
        bullet.transform.forward= shootingDirection;
        
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpwan.forward.normalized*bulletVelocuty,ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));

        if (allowReset)
        {
            Invoke("ResetShot",shootingDelay);
            allowReset=false;
        }
    }

    private void Reload()
    {
        SoundManager.Instance.emptySound.Play();
        isReloading=true;
        Invoke("ReloadCompleted",reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft=magazineSize;
        magazineLefts--;
        isReloading=false;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray,out hit))
        {
            targetPoint=hit.point;
        }

        else 
        {
            targetPoint= ray.GetPoint(100);
        }

        Vector3 direction = targetPoint -bulletSpwan.position;

        float x= UnityEngine.Random.Range(-spreadIntesity,spreadIntesity);
        float y= UnityEngine.Random.Range(-spreadIntesity,spreadIntesity);
        
        return direction+ new Vector3(x,y,0);
        



    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);


    }
}
